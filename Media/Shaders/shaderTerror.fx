/*
* Shader genérico para TgcMesh con iluminación dinámica por pixel (Phong Shading)
* utilizando un tipo de luz Point-Light con atenuación por distancia
* Hay 3 Techniques, una para cada MeshRenderType:
*	- VERTEX_COLOR
*	- DIFFUSE_MAP
*	- DIFFUSE_MAP_AND_LIGHTMAP
*/

/**************************************************************************************/
/* Variables comunes */
/**************************************************************************************/

float time;

//Matrices de transformacion
float4x4 matWorld; //Matriz de transformacion World
float4x4 matWorldView; //Matriz World * View
float4x4 matWorldViewProj; //Matriz World * View * Projection
float4x4 matInverseTransposeWorld; //Matriz Transpose(Invert(World))

//Textura para DiffuseMap
texture texDiffuseMap;
sampler2D diffuseMap = sampler_state
{
	Texture = (texDiffuseMap);
	ADDRESSU = WRAP;
	ADDRESSV = WRAP;
	MINFILTER = LINEAR;
	MAGFILTER = LINEAR;
	MIPFILTER = LINEAR;
};

//Textura para Lightmap
texture texLightMap;
sampler2D lightMap = sampler_state
{
   Texture = (texLightMap);
};

//Material del mesh
float3 materialEmissiveColor; //Color RGB
float3 materialAmbientColor; //Color RGB
float4 materialDiffuseColor; //Color ARGB (tiene canal Alpha)
float3 materialSpecularColor; //Color RGB
float materialSpecularExp; //Exponente de specular

//Parametros de la Luz
float3 lightColor; //Color RGB de la luz
float4 lightPosition; //Posicion de la luz
float4 eyePosition; //Posicion de la camara
float lightIntensity; //Intensidad de la luz
float lightAttenuation; //Factor de atenuacion de la luz



/**************************************************************************************/
/* VERTEX_COLOR */
/**************************************************************************************/

//Input del Vertex Shader
struct VS_INPUT_VERTEX_COLOR 
{
	float4 Position : POSITION0;
	float3 Normal : NORMAL0;
	float4 Color : COLOR;
};

//Output del Vertex Shader
struct VS_OUTPUT_VERTEX_COLOR
{
	float4 Position : POSITION0;
	float4 Color : COLOR;
	float3 WorldPosition : TEXCOORD0;
	float3 WorldNormal : TEXCOORD1;
	float3 LightVec	: TEXCOORD2;
	float3 HalfAngleVec	: TEXCOORD3;
};


//Vertex Shader
VS_OUTPUT_VERTEX_COLOR vs_VertexColor(VS_INPUT_VERTEX_COLOR input)
{
	VS_OUTPUT_VERTEX_COLOR output;

	//Proyectar posicion
	output.Position = mul(input.Position, matWorldViewProj);

	//Enviar color directamente
	output.Color = input.Color;

	//Posicion pasada a World-Space (necesaria para atenuación por distancia)
	output.WorldPosition = mul(input.Position, matWorld);

	/* Pasar normal a World-Space 
	Solo queremos rotarla, no trasladarla ni escalarla.
	Por eso usamos matInverseTransposeWorld en vez de matWorld */
	output.WorldNormal = mul(input.Normal, matInverseTransposeWorld).xyz;

	//LightVec (L): vector que va desde el vertice hacia la luz. Usado en Diffuse y Specular
	output.LightVec = lightPosition.xyz - output.WorldPosition;
	
	//ViewVec (V): vector que va desde el vertice hacia la camara.
	float3 viewVector = eyePosition.xyz - output.WorldPosition;
	
	//HalfAngleVec (H): vector de reflexion simplificado de Phong-Blinn (H = |V + L|). Usado en Specular
	output.HalfAngleVec = viewVector + output.LightVec;
	
	return output;
}

//Input del Pixel Shader
struct PS_INPUT_VERTEX_COLOR 
{
	float4 Color : COLOR0; 
	float3 WorldPosition : TEXCOORD0;
	float3 WorldNormal : TEXCOORD1;
	float3 LightVec	: TEXCOORD2;
	float3 HalfAngleVec	: TEXCOORD3;
};

//Pixel Shader
float4 ps_VertexColor(PS_INPUT_VERTEX_COLOR input) : COLOR0
{      
	//Normalizar vectores
	float3 Nn = normalize(input.WorldNormal);
	float3 Ln = normalize(input.LightVec);
	float3 Hn = normalize(input.HalfAngleVec);
	
	//Calcular intensidad de luz, con atenuacion por distancia
	float distAtten = length(lightPosition.xyz - input.WorldPosition) * lightAttenuation;
	float intensity = lightIntensity / distAtten; //Dividimos intensidad sobre distancia (lo hacemos lineal pero tambien podria ser i/d^2)
	
	//Componente Ambient
	float3 ambientLight = intensity * lightColor * materialAmbientColor;
	
	//Componente Diffuse: N dot L
	float3 n_dot_l = dot(Nn, Ln);
	float3 diffuseLight = intensity * lightColor * materialDiffuseColor.rgb * max(0.0, n_dot_l); //Controlamos que no de negativo
	
	//Componente Specular: (N dot H)^exp
	float3 n_dot_h = dot(Nn, Hn);
	float3 specularLight = n_dot_l <= 0.0
			? float3(0.0, 0.0, 0.0)
			: (intensity * lightColor * materialSpecularColor * pow(max( 0.0, n_dot_h), materialSpecularExp));
	
	
	/* Color final: modular (Emissive + Ambient + Diffuse) por el color del mesh, y luego sumar Specular.
	   El color Alpha sale del diffuse material */
	float4 finalColor = float4(saturate(materialEmissiveColor + ambientLight + diffuseLight) * input.Color + specularLight , materialDiffuseColor.a);
	
	
	return finalColor;
}

/*
* Technique VERTEX_COLOR
*/
technique VERTEX_COLOR
{
   pass Pass_0
   {
	  VertexShader = compile vs_2_0 vs_VertexColor();
	  PixelShader = compile ps_2_0 ps_VertexColor();
   }
}


/**************************************************************************************/
/* DIFFUSE_MAP */
/**************************************************************************************/

//Input del Vertex Shader
struct VS_INPUT_DIFFUSE_MAP
{
   float4 Position : POSITION0;
   float3 Normal : NORMAL0;
   float4 Color : COLOR;
   float2 Texcoord : TEXCOORD0;
};

//Output del Vertex Shader
struct VS_OUTPUT_DIFFUSE_MAP
{
	float4 Position : POSITION0;
	float2 Texcoord : TEXCOORD0;
	float3 WorldPosition : TEXCOORD1;
	float3 WorldNormal : TEXCOORD2;
	float3 LightVec	: TEXCOORD3;
	float3 HalfAngleVec	: TEXCOORD4;
};


//Vertex Shader
VS_OUTPUT_DIFFUSE_MAP vs_modificado1(VS_INPUT_DIFFUSE_MAP input)
{ 
	VS_OUTPUT_DIFFUSE_MAP output;
	
	//Animar Posicion
	//input.Position.x *= abs (clamp(time,1,100) * cos(time) );
	//input.Position.y *= abs (clamp(time,1,100) * sin(time) );
	//input.Position.z *= abs (clamp(time,1,100) * cos(time) );

	float constante = 7;
	
	input.Position.x += constante*(cos(time) + sin(time)*cos(3*time));
	input.Position.y -= constante*(sin(time) + cos(time)*sin(4*time));
	input.Position.z += constante*(sin(time) + cos(time)*cos(2*time));
	
	//Proyectar posicion
	output.Position = mul(input.Position, matWorldViewProj);

	//Enviar Texcoord directamente
	output.Texcoord = input.Texcoord;

	//Posicion pasada a World-Space (necesaria para atenuación por distancia)
	output.WorldPosition = mul(input.Position, matWorld);

	/* Pasar normal a World-Space 
	Solo queremos rotarla, no trasladarla ni escalarla.
	Por eso usamos matInverseTransposeWorld en vez de matWorld */
	output.WorldNormal = mul(input.Normal, matInverseTransposeWorld).xyz;

	//LightVec (L): vector que va desde el vertice hacia la luz. Usado en Diffuse y Specular
	output.LightVec = lightPosition.xyz - output.WorldPosition;

	//ViewVec (V): vector que va desde el vertice hacia la camara.
	float3 viewVector = eyePosition.xyz - output.WorldPosition;

	//HalfAngleVec (H): vector de reflexion simplificado de Phong-Blinn (H = |V + L|). Usado en Specular
	output.HalfAngleVec = viewVector + output.LightVec;

	return output;
}


//Input del Pixel Shader
struct PS_DIFFUSE_MAP
{
	float2 Texcoord : TEXCOORD0;
	float3 WorldPosition : TEXCOORD1;
	float3 WorldNormal : TEXCOORD2;
	float3 LightVec	: TEXCOORD3;
	float3 HalfAngleVec	: TEXCOORD4;
};

//Pixel Shader
float4 ps_modificado1(PS_DIFFUSE_MAP input) : COLOR0
{
	//Normalizar vectores
	float3 Nn = normalize(input.WorldNormal);
	float3 Ln = normalize(input.LightVec);
	float3 Hn = normalize(input.HalfAngleVec);
	
	//Calcular intensidad de luz, con atenuacion por distancia
	float distAtten = length(lightPosition.xyz - input.WorldPosition) * lightAttenuation;
	float intensity = lightIntensity / distAtten; //Dividimos intensidad sobre distancia (lo hacemos lineal pero tambien podria ser i/d^2)
	
	//Obtener texel de la textura
	float4 texelColor = tex2D(diffuseMap, input.Texcoord);
	
	//Componente Ambient
	float3 ambientLight = intensity * lightColor * materialAmbientColor;
	
	//Componente Diffuse: N dot L
	float3 n_dot_l = dot(Nn, Ln);
	float3 diffuseLight = intensity * lightColor * materialDiffuseColor.rgb * max(0.0, n_dot_l); //Controlamos que no de negativo
	
	//Componente Specular: (N dot H)^exp
	float3 n_dot_h = dot(Nn, Hn);
	float3 specularLight = n_dot_l <= 0.0
			? float3(0.0, 0.0, 0.0)
			: (intensity * lightColor * materialSpecularColor * pow(max( 0.0, n_dot_h), materialSpecularExp));
	
	/* Color final: modular (Emissive + Ambient + Diffuse) por el color de la textura, y luego sumar Specular.
	   El color Alpha sale del diffuse material */
	float4 finalColor = float4(saturate(materialEmissiveColor + ambientLight + diffuseLight) * texelColor + specularLight, materialDiffuseColor.a);
	
	//Vision nocturna
	//finalColor.x = finalColor.x *0.222 + 0.08*cos(time);
	//finalColor.y = finalColor.y *0.707 + 0.08*cos(time);
	//finalColor.z = finalColor.z *0.071 + 0.08*cos(time);
	
	float scaleFactor = 0.5 * abs( sin(time) );
	
	float value = ((finalColor.r + finalColor.g + finalColor.b) / 3) * scaleFactor;
	finalColor.rgb = finalColor.rgb * (1 - scaleFactor) + value * scaleFactor;
	
	return finalColor;
}









/**************************************************************************************/
/* ShaderModificado */
/**************************************************************************************/

//Input del Vertex Shader
struct VS_INPUT_DIFFUSE_MAP_AND_LIGHTMAP
{
   float4 Position : POSITION0;
   float3 Normal : NORMAL0;
   float4 Color : COLOR;
   float2 Texcoord : TEXCOORD0;
   float2 TexcoordLightmap : TEXCOORD1;
};

//Output del Vertex Shader
struct VS_OUTPUT_DIFFUSE_MAP_AND_LIGHTMAP
{
	float4 Position : POSITION0;
	float2 Texcoord : TEXCOORD0;
	float2 TexcoordLightmap : TEXCOORD1;
	float3 WorldPosition : TEXCOORD2;
	float3 WorldNormal : TEXCOORD3;
	float3 LightVec	: TEXCOORD4;
	float3 HalfAngleVec	: TEXCOORD5;
};

//Vertex Shader
VS_OUTPUT_DIFFUSE_MAP_AND_LIGHTMAP vs_modificado(VS_INPUT_DIFFUSE_MAP_AND_LIGHTMAP input)
{
	VS_OUTPUT_DIFFUSE_MAP_AND_LIGHTMAP output;
	
	input.Position.x += 50*cos(time) + 10*sin(time)*cos(3*time);
	input.Position.y -= 30*sin(time) + 30*cos(time)*sin(4*time);
	input.Position.z += 35*sin(time) + 50*cos(time)*cos(2*time);
	
	//input.Position.x += abs( 50 * cos(time) );
	//input.Position.y += abs( 40 * cos(time) );
	//input.Position.z += abs( 30 * sin(time) );
	
	//Proyecto pos
	output.Position = mul ( input.Position, matWorldViewProj);

	//Enviar Texcoord directamente
	output.Texcoord = input.Texcoord;
	output.TexcoordLightmap = input.TexcoordLightmap;

	//Posicion pasada a World-Space (necesaria para atenuación por distancia)
	output.WorldPosition = mul(input.Position, matWorld);

	/* Pasar normal a World-Space 
	Solo queremos rotarla, no trasladarla ni escalarla.
	Por eso usamos matInverseTransposeWorld en vez de matWorld */
	output.WorldNormal = mul(input.Normal, matInverseTransposeWorld).xyz;

	//LightVec (L): vector que va desde el vertice hacia la luz. Usado en Diffuse y Specular
	output.LightVec = lightPosition.xyz - output.WorldPosition;

	//ViewVec (V): vector que va desde el vertice hacia la camara.
	float3 viewVector = eyePosition.xyz - output.WorldPosition;

	//HalfAngleVec (H): vector de reflexion simplificado de Phong-Blinn (H = |V + L|). Usado en Specular
	output.HalfAngleVec = viewVector + output.LightVec;

	return output;
}



//Input del Pixel Shader
struct PS_INPUT_DIFFUSE_MAP_AND_LIGHTMAP
{
	float2 Texcoord : TEXCOORD0;
	float2 TexcoordLightmap : TEXCOORD1;
	float3 WorldPosition : TEXCOORD2;
	float3 WorldNormal : TEXCOORD3;
	float3 LightVec	: TEXCOORD4;
	float3 HalfAngleVec	: TEXCOORD5;
};

//Pixel Shader
float4 ps_modificado(PS_INPUT_DIFFUSE_MAP_AND_LIGHTMAP input) : COLOR0
{		

	//Normalizar vectores
	float3 Nn = normalize(input.WorldNormal);
	float3 Ln = normalize(input.LightVec);
	float3 Hn = normalize(input.HalfAngleVec);
	
	//Calcular intensidad de luz, con atenuacion por distancia
	float distAtten = length(lightPosition.xyz - input.WorldPosition) * lightAttenuation;
	float intensity = lightIntensity / distAtten; //Dividimos intensidad sobre distancia (lo hacemos lineal pero tambien podria ser i/d^2)
	
	//Obtener color de diffuseMap y de Lightmap
	float4 texelColor = tex2D(diffuseMap, input.Texcoord);
	float4 lightmapColor = tex2D(lightMap, input.TexcoordLightmap);
	
	//Componente Ambient
	float3 ambientLight = intensity * lightColor * materialAmbientColor;
	
	//Componente Diffuse: N dot L
	float3 n_dot_l = dot(Nn, Ln);
	float3 diffuseLight = intensity * lightColor * materialDiffuseColor.rgb * max(0.0, n_dot_l); //Controlamos que no de negativo
	
	//Componente Specular: (N dot H)^exp
	float3 n_dot_h = dot(Nn, Hn);
	float3 specularLight = n_dot_l <= 0.0
			? float3(0.0, 0.0, 0.0)
			: (intensity * lightColor * materialSpecularColor * pow(max( 0.0, n_dot_h), materialSpecularExp));
	
	/* Color final: modular (Emissive + Ambient + Diffuse) por el color de la textura, y luego sumar Specular.
	   El color Alpha sale del diffuse material */
	float4 finalColor = float4(saturate(materialEmissiveColor + ambientLight + diffuseLight) * (texelColor * lightmapColor) + specularLight, materialDiffuseColor.a);
		
	//finalColor.x = finalColor.x *0.222 + 0.08*cos(time);
	//finalColor.y = finalColor.y *0.707 + 0.08*cos(time);
	//finalColor.z = finalColor.z *0.071 + 0.08*cos(time);
	
	float scaleFactor = abs( sin(time) );
	
	float value = ((finalColor.r + finalColor.g + finalColor.b) / 3) * scaleFactor;
	//finalColor.rgb = finalColor.rgb * (1 - scaleFactor) + value * scaleFactor;
	
	//float promedio = (finalColor.r + finalColor.g + finalColor.b) / 3;
	
	//finalColor.rgb = promedio + 0.05*cos(time);
	
	return finalColor;
}


//technique DIFFUSE_MAP_AND_LIGHTMAP
technique ShaderTerror
{
   pass Pass_0
   {
	  VertexShader = compile vs_2_0 vs_modificado1();
	  PixelShader = compile ps_2_0 ps_modificado1();
   }
}


