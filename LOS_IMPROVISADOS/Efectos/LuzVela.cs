using Microsoft.DirectX;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using System.Drawing;
using Microsoft.DirectX.Direct3D;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Efectos
{
    class LuzVela : IEfecto
    {
        public TgcScene tgcEscena { set; get; }

        public CamaraFramework camaraFramework { get; set; }

        public LuzVela (TgcScene tgcEscena, CamaraFramework camaraFramework)
        {
            this.tgcEscena = tgcEscena;
            this.camaraFramework = camaraFramework;
        }

        public void init()
        {
            crearModificadores();
            crearVariablesDeUsuario();
        }

        public void render()
        {
            Device device = GuiController.Instance.D3dDevice;
           
            Effect currentShader = GuiController.Instance.Shaders.TgcMeshPointLightShader;

            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                mesh.Effect = currentShader;
                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
            }

            Vector3 lightPos = camaraFramework.getPosition();

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
            GuiController.Instance.Modifiers.addFloat("velaIntensidad", 0f, 150f, 10f);
            GuiController.Instance.Modifiers.addFloat("velaAtenuacion", 0.1f, 2f, 0.9f);
            GuiController.Instance.Modifiers.addFloat("velaEspecularEx", 0, 20, 1f);

            //Modifiers de material
            GuiController.Instance.Modifiers.addColor("VelaEmissive", Color.Black);
            GuiController.Instance.Modifiers.addColor("VelaAmbient", Color.Gray);
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
