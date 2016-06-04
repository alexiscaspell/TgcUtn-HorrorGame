using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using TgcViewer.Utils.TgcGeometry;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using System.Collections.Generic;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.fluors
{
    class LuzFluor : ALuz
    {
        public LuzFluor(List<TgcMesh> meshes, CamaraFPS camaraFPS) : base(meshes, camaraFPS)
        {
        }

        public override void init()
        {
            GuiController.Instance.Modifiers.addColor("fluorColor", Color.LightGreen);
            GuiController.Instance.Modifiers.addFloat("fluorIntensidad", 0f, 150f, 70f);
            GuiController.Instance.Modifiers.addFloat("fluorAtenuacion", 0.1f, 2f, 0.2f);
            GuiController.Instance.Modifiers.addFloat("fluorEspecularEx", 0, 40, 6f);

            //Modifiers de material
            GuiController.Instance.Modifiers.addColor("fluorEmissive", Color.Black);
            GuiController.Instance.Modifiers.addColor("fluorAmbient", Color.LightGreen);
            GuiController.Instance.Modifiers.addColor("fluorDiffuse", Color.LightGreen);
            GuiController.Instance.Modifiers.addColor("fluorSpecular", Color.LightGreen);
        }

        public override void render()
        {
            Effect currentShader = GuiController.Instance.Shaders.TgcMeshPointLightShader;

            foreach (TgcMesh mesh in meshes)
            {
                mesh.Effect = currentShader;
                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
            }
            
            foreach (TgcMesh mesh in meshes)
            {
                //Cargar variables shader de la luz
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["fluorColor"]));
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(camaraFPS.posicion));
                mesh.Effect.SetValue("lightIntensity", (float)GuiController.Instance.Modifiers["fluorIntensidad"]);
                mesh.Effect.SetValue("lightAttenuation", (float)GuiController.Instance.Modifiers["fluorAtenuacion"]);

                //Cargar variables de shader de Material. El Material en realidad deberia ser propio de cada mesh. Pero en este ejemplo se simplifica con uno comun para todos
                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["fluorEmissive"]));
                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["fluorAmbient"]));
                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["fluorDiffuse"]));
                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["fluorSpecular"]));
                mesh.Effect.SetValue("materialSpecularExp", (float)GuiController.Instance.Modifiers["fluorEspecularEx"]);

                mesh.render();
            }          
        }
    }
}
