using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using TgcViewer.Utils.TgcGeometry;
using System.Collections.Generic;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas
{
    class LuzLinterna : ALuz
    {
        public LuzLinterna(Mapa mapa, CamaraFPS camaraFPS) : base(mapa, camaraFPS)
        {
        }

        public override void configInicial()
        {
            ///luz
            GuiController.Instance.Modifiers.addColor("linternaColor", Color.White);
            GuiController.Instance.Modifiers.addFloat("linternaIntensidad", 0, 400f, 190f);
            GuiController.Instance.Modifiers.addFloat("linternaAtenuacion", 0.1f, 2, 0.4f);
            GuiController.Instance.Modifiers.addFloat("linternaSpecularEx", 0, 20, 15f);
            GuiController.Instance.Modifiers.addFloat("linternaAngulo", 0, 180, 45f);
            GuiController.Instance.Modifiers.addFloat("linternaSpotExponent", 0, 40, 25f);

            //material
            GuiController.Instance.Modifiers.addColor("linternaEmissive", Color.Black);
            GuiController.Instance.Modifiers.addColor("linternaAmbient", Color.White);
            GuiController.Instance.Modifiers.addColor("linternaDiffuse", Color.White);
            GuiController.Instance.Modifiers.addColor("linternaSpecular", Color.White);
            
        } 

        public override void configurarEfecto(TgcMesh mesh)
        {
        	//Cargar variables shader de la luz
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(camaraFPS.posicion));
                mesh.Effect.SetValue("eyePosition", TgcParserUtils.vector3ToFloat4Array(camaraFPS.posicion));
                mesh.Effect.SetValue("spotLightDir", TgcParserUtils.vector3ToFloat3Array(camaraFPS.camaraFramework.viewDir));
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["linternaColor"]));
                mesh.Effect.SetValue("lightIntensity", (float)GuiController.Instance.Modifiers["linternaIntensidad"]);
                mesh.Effect.SetValue("lightAttenuation", (float)GuiController.Instance.Modifiers["linternaAtenuacion"]);
                mesh.Effect.SetValue("spotLightExponent", (float)GuiController.Instance.Modifiers["linternaSpotExponent"]);
                mesh.Effect.SetValue("spotLightAngleCos", FastMath.ToRad((float)GuiController.Instance.Modifiers["linternaAngulo"]));

                //Cargar variables de shader de Material. El Material en realidad deberia ser propio de cada mesh. Pero en este ejemplo se simplifica con uno comun para todos
                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["linternaEmissive"]));
                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["linternaAmbient"]));
                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["linternaDiffuse"]));
                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["linternaSpecular"]));
                mesh.Effect.SetValue("materialSpecularExp", (float)GuiController.Instance.Modifiers["linternaSpecularEx"]);

        }
        
//        public override void render()
//        {
//            Effect currentShader = GuiController.Instance.Shaders.TgcMeshSpotLightShader;
//            foreach (TgcMesh mesh in mapa.escenaFiltrada)
//            {
//                mesh.Effect = currentShader;
//                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
//            }
//            
//            Vector3 lightDir = camaraFPS.direccionVista;
//            lightDir.Normalize();
//
//            foreach (TgcMesh mesh in mapa.escenaFiltrada)
//            {
//                //Cargar variables shader de la luz
//                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(camaraFPS.posicion));
//                mesh.Effect.SetValue("eyePosition", TgcParserUtils.vector3ToFloat4Array(camaraFPS.posicion));
//                mesh.Effect.SetValue("spotLightDir", TgcParserUtils.vector3ToFloat3Array(lightDir));
//                mesh.Effect.SetValue("lightColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["linternaColor"]));
//                mesh.Effect.SetValue("lightIntensity", (float)GuiController.Instance.Modifiers["linternaIntensidad"]);
//                mesh.Effect.SetValue("lightAttenuation", (float)GuiController.Instance.Modifiers["linternaAtenuacion"]);
//                mesh.Effect.SetValue("spotLightExponent", (float)GuiController.Instance.Modifiers["linternaSpotExponent"]);
//                mesh.Effect.SetValue("spotLightAngleCos", FastMath.ToRad((float)GuiController.Instance.Modifiers["linternaAngulo"]));
//
//                //Cargar variables de shader de Material. El Material en realidad deberia ser propio de cada mesh. Pero en este ejemplo se simplifica con uno comun para todos
//                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["linternaEmissive"]));
//                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["linternaAmbient"]));
//                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["linternaDiffuse"]));
//                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["linternaSpecular"]));
//                mesh.Effect.SetValue("materialSpecularExp", (float)GuiController.Instance.Modifiers["linternaSpecularEx"]);
//
//                mesh.render();
//            }
//        }
        
    }
}
