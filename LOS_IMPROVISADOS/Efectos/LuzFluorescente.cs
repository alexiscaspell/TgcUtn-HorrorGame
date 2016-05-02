
using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;

using Microsoft.DirectX.Direct3D;
using System.Drawing;
using TgcViewer.Utils.Shaders;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Efectos
{
    class LuzFluorescente : IEfecto
    {
        public TgcScene tgcEscena { set; get; }

        public CamaraFPS camaraFPS { get; set; }

        public LuzFluorescente(TgcScene tgcEscena, CamaraFPS camaraFPS)
        {
            this.tgcEscena = tgcEscena;
            this.camaraFPS = camaraFPS;
        }
        
        public void crearModificadores()
        {
            GuiController.Instance.Modifiers.addColor("fluorColor", Color.LightGreen);
            GuiController.Instance.Modifiers.addFloat("fluorIntensidad", 0f, 150f, 8f);
            GuiController.Instance.Modifiers.addFloat("fluorAtenuacion", 0.1f, 2f, 0.5f);
            GuiController.Instance.Modifiers.addFloat("fluorEspecularEx", 0, 20, 5f);

            //Modifiers de material
            GuiController.Instance.Modifiers.addColor("fluorEmissive", Color.Black);
            GuiController.Instance.Modifiers.addColor("fluorAmbient", Color.LawnGreen);
            GuiController.Instance.Modifiers.addColor("fluorDiffuse", Color.DarkGreen);
            GuiController.Instance.Modifiers.addColor("fluorSpecular", Color.White);
        }

        public void crearVariablesDeUsuario()
        {
        }

        public void init()
        {
            crearModificadores();
            crearVariablesDeUsuario();
        }

        public void render()
        {
            Effect currentShader = GuiController.Instance.Shaders.TgcMeshPointLightShader;

            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                mesh.Effect = currentShader;
                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
            }

            Vector3 lightPos = camaraFPS.posicion;

            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                //Cargar variables shader de la luz
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["fluorColor"]));
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(lightPos));
                mesh.Effect.SetValue("eyePosition", TgcParserUtils.vector3ToFloat4Array(GuiController.Instance.FpsCamera.getPosition()));
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

        public void usarModificadoresIniciales()
        {
        }

        public void usarVariablesDeUsuarioIniciales()
        {
        }
    }
}
