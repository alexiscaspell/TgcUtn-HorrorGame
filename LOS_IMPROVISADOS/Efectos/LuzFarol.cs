using Microsoft.DirectX;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using System.Drawing;
using Microsoft.DirectX.Direct3D;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Efectos
{
    class LuzFarol : IEfecto
    {
        public TgcScene tgcEscena { set; get; }

        public CamaraFPS camaraFPS { get; set; }

        public LuzFarol (TgcScene tgcEscena, CamaraFPS camaraFPS)
        {
            this.tgcEscena = tgcEscena;
            this.camaraFPS = camaraFPS;
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
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["velaColor"]));
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(lightPos));
                mesh.Effect.SetValue("eyePosition", TgcParserUtils.vector3ToFloat4Array(GuiController.Instance.FpsCamera.getPosition()));
                mesh.Effect.SetValue("lightIntensity", (float)GuiController.Instance.Modifiers["velaIntensidad"]);
                mesh.Effect.SetValue("lightAttenuation", (float)GuiController.Instance.Modifiers["velaAtenuacion"]);

                //Cargar variables de shader de Material. El Material en realidad deberia ser propio de cada mesh. Pero en este ejemplo se simplifica con uno comun para todos
                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["VelaEmissive"]));
                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["VelaAmbient"]));
                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["VelaDiffuse"]));
                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["VelaSpecular"]));
                mesh.Effect.SetValue("materialSpecularExp", (float)GuiController.Instance.Modifiers["velaEspecularEx"]);

                mesh.render();
            }
        }

        public void crearModificadores()
        {
            GuiController.Instance.Modifiers.addColor("velaColor", Color.LightYellow);
            GuiController.Instance.Modifiers.addFloat("velaIntensidad", 0f, 150f, 3f);
            GuiController.Instance.Modifiers.addFloat("velaAtenuacion", 0.1f, 2f, 0.3f);
            GuiController.Instance.Modifiers.addFloat("velaEspecularEx", 0, 20, 0.8f);

            //Modifiers de material
            GuiController.Instance.Modifiers.addColor("VelaEmissive", Color.Black);
            GuiController.Instance.Modifiers.addColor("VelaAmbient", Color.White);
            GuiController.Instance.Modifiers.addColor("VelaDiffuse", Color.White);
            GuiController.Instance.Modifiers.addColor("VelaSpecular", Color.White);
        }

        public void crearVariablesDeUsuario()
        {
        }


        public void usarModificadoresIniciales()
        {
        }

        public void usarVariablesDeUsuarioIniciales()
        {
        }
    }
}
