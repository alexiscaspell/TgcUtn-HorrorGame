using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;

using Microsoft.DirectX.Direct3D;
using System.Drawing;
using TgcViewer.Utils.TgcGeometry;
using AlumnoEjemplos.LOS_IMPROVISADOS.Efectos;
using TgcViewer.Utils._2D;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class LuzLinterna : IEfecto
    {
        public TgcScene tgcEscena { set; get; }

        public CamaraFramework camaraFramework { get; set; }

        public  TgcSprite spriteMano;

        public LuzLinterna(TgcScene tgcEscena, CamaraFramework camaraFramework)
        {
            this.tgcEscena = tgcEscena;
            this.camaraFramework = camaraFramework;
///////////////////////////////////ACA EMPIEZA FERNILANDIA////////////////////////////////////////////////
            spriteMano = new TgcSprite();
            spriteMano.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\manoLinterna.png");

        }

        public void init()
        {
            crearModificadores();
            crearVariablesDeUsuario();
///////////////////////////////////ACA EMPIEZA FERNILANDIA////////////////////////////////////////////////
            Size screenSize = GuiController.Instance.Panel3d.Size;

            spriteMano.Position = new Vector2(screenSize.Width/2, 0.75f*screenSize.Height);

            spriteMano.Scaling = new Vector2((float)0.0002 * screenSize.Width, (float)0.0003 * screenSize.Height);
        } 

        public void render()
        {
            Effect currentShader = GuiController.Instance.Shaders.TgcMeshSpotLightShader;
            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                mesh.Effect = currentShader;
                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
            }

            Vector3 lightPos = camaraFramework.getPosition();
            Vector3 lightDir = camaraFramework.getLookAt() - camaraFramework.getPosition();
            lightDir.Normalize();

            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                //Cargar variables shader de la luz
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(lightPos));
                mesh.Effect.SetValue("spotLightDir", TgcParserUtils.vector3ToFloat3Array(lightDir));
                mesh.Effect.SetValue("eyePosition", TgcParserUtils.vector3ToFloat4Array(GuiController.Instance.FpsCamera.getPosition()));
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

                mesh.render();
            }
            ///////////////////////////////////ACA EMPIEZA FERNILANDIA////////////////////////////////////////////////
            GuiController.Instance.Drawer2D.beginDrawSprite();
            spriteMano.render();
            GuiController.Instance.Drawer2D.endDrawSprite();
        }

        public void crearVariablesDeUsuario()
        {
        }

        public void crearModificadores()
        {
            //luz
            GuiController.Instance.Modifiers.addColor("linternaColor", Color.White);
            GuiController.Instance.Modifiers.addFloat("linternaIntensidad", 0, 150, 12f);
            GuiController.Instance.Modifiers.addFloat("linternaAtenuacion", 0.1f, 2, 0.34f);
            GuiController.Instance.Modifiers.addFloat("linternaSpecularEx", 0, 20, 15f);
            GuiController.Instance.Modifiers.addFloat("linternaAngulo", 0, 180, 45f);
            GuiController.Instance.Modifiers.addFloat("linternaSpotExponent", 0, 20, 17f);

            //material
            GuiController.Instance.Modifiers.addColor("linternaEmissive", Color.Black);
            GuiController.Instance.Modifiers.addColor("linternaAmbient", Color.White);
            GuiController.Instance.Modifiers.addColor("linternaDiffuse", Color.White);
            GuiController.Instance.Modifiers.addColor("linternaSpecular", Color.White);
        }

        public void usarVariablesDeUsuarioIniciales()
        {
            
        }


        public void usarModificadoresIniciales()
        {
            
        }
        
    }
}
