using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;

using Microsoft.DirectX.Direct3D;
using System.Drawing;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class LuzLinterna
    {
        public TgcScene tgcEscena { set; get; }

        public CamaraFramework camaraFramework { get; set; }

        public LuzLinterna(TgcScene tgcEscena, CamaraFramework camaraFramework)
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
            bool lightEnable = (bool)GuiController.Instance.Modifiers["lightEnable"];
            Effect currentShader;

            if (lightEnable)
            {
                currentShader = GuiController.Instance.Shaders.TgcMeshSpotLightShader;
            }
            else
            {
                currentShader = GuiController.Instance.Shaders.TgcMeshShader;
            }

            //Aplicar a cada mesh el shader actual
            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                mesh.Effect = currentShader;
                //El Technique depende del tipo RenderType del mesh
                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
            }

            /*
            Vector3 lightPos = GuiController.Instance.FpsCamera.getPosition();
            Vector3 lightDir = GuiController.Instance.FpsCamera.getLookAt() - GuiController.Instance.FpsCamera.getPosition();
                lightDir.Normalize();
            */

            Vector3 lightPos = camaraFramework.getPosition();
            Vector3 lightDir = camaraFramework.getLookAt() - camaraFramework.getPosition();
            lightDir.Normalize();

            //Renderizar meshes
            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                if (lightEnable)
                {
                    //Cargar variables shader de la luz
                    mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(lightPos));
                    mesh.Effect.SetValue("spotLightDir", TgcParserUtils.vector3ToFloat3Array(lightDir));
                    mesh.Effect.SetValue("eyePosition", TgcParserUtils.vector3ToFloat4Array(GuiController.Instance.FpsCamera.getPosition()));
                    mesh.Effect.SetValue("lightColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["lightColor"]));
                    mesh.Effect.SetValue("lightIntensity", (float)GuiController.Instance.Modifiers["lightIntensity"]);
                    mesh.Effect.SetValue("lightAttenuation", (float)GuiController.Instance.Modifiers["lightAttenuation"]);
                    mesh.Effect.SetValue("spotLightExponent", (float)GuiController.Instance.Modifiers["spotExponent"]);
                    mesh.Effect.SetValue("spotLightAngleCos", FastMath.ToRad((float)GuiController.Instance.Modifiers["spotAngle"]));

                    //Cargar variables de shader de Material. El Material en realidad deberia ser propio de cada mesh. Pero en este ejemplo se simplifica con uno comun para todos
                    mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["mEmissive"]));
                    mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["mAmbient"]));
                    mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["mDiffuse"]));
                    mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["mSpecular"]));
                    mesh.Effect.SetValue("materialSpecularExp", (float)GuiController.Instance.Modifiers["specularEx"]);
                }

                //Renderizar modelo
                mesh.render();
            }
        }

        public void crearVariablesDeUsuario()
        {

        }

        public void crearModificadores()
        {
            //luz
            GuiController.Instance.Modifiers.addBoolean("lightEnable", "lightEnable", true);
            GuiController.Instance.Modifiers.addColor("lightColor", Color.White);
            GuiController.Instance.Modifiers.addFloat("lightIntensity", 0, 150, 9f);
            GuiController.Instance.Modifiers.addFloat("lightAttenuation", 0.1f, 2, 0.34f);
            GuiController.Instance.Modifiers.addFloat("specularEx", 0, 20, 15f);
            GuiController.Instance.Modifiers.addFloat("spotAngle", 0, 180, 45f);
            GuiController.Instance.Modifiers.addFloat("spotExponent", 0, 20, 12f);

            //material
            GuiController.Instance.Modifiers.addColor("mEmissive", Color.Black);
            GuiController.Instance.Modifiers.addColor("mAmbient", Color.White);
            GuiController.Instance.Modifiers.addColor("mDiffuse", Color.White);
            GuiController.Instance.Modifiers.addColor("mSpecular", Color.White);
        }

        public void usarVariablesDeUsuarioIniciales()
        {
            
        }


        public void usarModificadoresIniciales()
        {
            
        }
        
    }
}
