using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.general
{
    class LuzOscura : ALuz
    {
        public LuzOscura(Mapa mapa, CamaraFPS camaraFPS)
        {
        	this.mapa = mapa;
        	this.camaraFPS = camaraFPS;
        }

        public override void configInicial()
        {
            currentShader = GuiController.Instance.Shaders.TgcMeshPointLightShader;
        }
        
        public override void configurarEfecto(TgcMesh mesh)
        {
        	    //Cargar variables shader de la luz
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor(Color.Gray));
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(camaraFPS.posicion));
                mesh.Effect.SetValue("lightIntensity", 10f);
                mesh.Effect.SetValue("lightAttenuation", 0.17f);
                mesh.Effect.SetValue("materialSpecularExp", 1f);

                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor(Color.Black));
                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor(Color.DarkGray));
                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor(Color.White));
                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor(Color.White));
        }

//        public override void render()
//        {
//            Effect currentShader = GuiController.Instance.Shaders.TgcMeshPointLightShader;
//
//            foreach (TgcMesh mesh in mapa.escenaFiltrada)
//            {
//                mesh.Effect = currentShader;
//                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
//            }
//            
//            foreach (TgcMesh mesh in mapa.escenaFiltrada)
//            {
//                //Cargar variables shader de la luz
//                mesh.Effect.SetValue("lightColor", ColorValue.FromColor(Color.Gray));
//                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(camaraFPS.posicion));
//                mesh.Effect.SetValue("lightIntensity", 30f);
//                mesh.Effect.SetValue("lightAttenuation", 0.17f);
//                mesh.Effect.SetValue("materialSpecularExp", 1f);
//
//                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor(Color.Black));
//                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor(Color.DarkGray));
//                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor(Color.White));
//                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor(Color.White));
//
//                mesh.render();
//            }
//        }
    }
}
