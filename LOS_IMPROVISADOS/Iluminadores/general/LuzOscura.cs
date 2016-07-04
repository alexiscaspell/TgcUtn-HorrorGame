using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.Shaders;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.general
{
    class LuzOscura : ALuz
    {
    	private float time = 0;
    	
        public LuzOscura(Mapa mapa, CamaraFPS camaraFPS)
        {
        	this.mapa = mapa;
        	this.camaraFPS = camaraFPS;
        }

        public override void configInicial()
        {
        	currentShader = TgcShaders.loadEffect(GuiController.Instance.AlumnoEjemplosDir + 
        	                         "LOS_IMPROVISADOS\\Shaders\\shaderTerror.fx");
        	
        	//GuiController.Instance.Modifiers.addColor("luzOscuraColor", Color.LightYellow);
            //GuiController.Instance.Modifiers.addFloat("luzOscuraIntensidad", 0f, 150f, 19f);
            //GuiController.Instance.Modifiers.addFloat("luzOscuraAtenuacion", 0.1f, 2f, 0.1f);
            //GuiController.Instance.Modifiers.addFloat("luzOscuraEspecularEx", 0, 20, 4f);

            //Modifiers de material
            //GuiController.Instance.Modifiers.addColor("luzOscuraEmissive", Color.Black);
            //GuiController.Instance.Modifiers.addColor("luzOscuraAmbient", Color.LightYellow);
            //GuiController.Instance.Modifiers.addColor("luzOscuraDiffuse", Color.Gray);
            //GuiController.Instance.Modifiers.addColor("luzOscuraSpecular", Color.LightYellow);
        }
        
        public override void configurarEfecto(TgcMesh mesh)
        {
        	    //Cargar variables shader de la luz
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor(Color.Gray));
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(camaraFPS.posicion));
                mesh.Effect.SetValue("lightIntensity", 50f);
                //mesh.Effect.SetValue("lightAttenuation", 0.13f);
                mesh.Effect.SetValue("lightAttenuation", 0.075f);
                mesh.Effect.SetValue("materialSpecularExp", 0.1f);

                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor(Color.Black));
                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor(Color.DarkGray));
                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor(Color.White));
                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor(Color.White));
                
           }

        public override void render()
        {
        	inv.render();
            
            updateFondo();
            
            time += GuiController.Instance.ElapsedTime;
            
            foreach (TgcMesh mesh in mapa.escena.Meshes)
	        {
				if( TgcCollisionUtils.testAABBAABB(mesh.BoundingBox, cajaNegra.BoundingBox))
				{
					mesh.Effect = currentShader;
                	mesh.Technique = "ShaderTerror";

                	configurarEfecto(mesh);
                	
                	mesh.Effect.SetValue("time", time);
                	
				   	mesh.render();
                }
        	}
            
            foreach(Accionable a in mapa.objetos)
            {
            	if( TgcCollisionUtils.testAABBAABB(a.getMesh().BoundingBox, cajaNegra.BoundingBox))
				{
            		a.getMesh().Effect = currentShader;
            		a.getMesh().Technique = "ShaderTerror";

            		configurarEfecto(a.getMesh() );
            		
            		a.getMesh().Effect.SetValue("time", time);
                	
				   	a.render();
                }
            }
            
        }

        
    }
}
