using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.general
{
    class LuzOscura : ALuz
    {
        public LuzOscura(TgcScene tgcEscena, CamaraFPS camaraFPS) : base(tgcEscena, camaraFPS)
        {
        }

        public override void init()
        {
        }

        public override void render()
        {
            Effect currentShader = GuiController.Instance.Shaders.TgcMeshPointLightShader;

            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                mesh.Effect = currentShader;
                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
            }
            
            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                //Cargar variables shader de la luz
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor(Color.Gray));
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(camaraFPS.posicion));
                mesh.Effect.SetValue("lightIntensity", 30f);
                mesh.Effect.SetValue("lightAttenuation", 0.17f);
                mesh.Effect.SetValue("materialSpecularExp", 1f);

                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor(Color.Black));
                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor(Color.DarkGray));
                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor(Color.White));
                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor(Color.White));

                mesh.render();
            }
        }
    }
}
