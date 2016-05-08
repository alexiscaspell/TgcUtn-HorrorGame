using Microsoft.DirectX;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using TgcViewer.Utils._2D;
using System;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores
{
    class LuzFarol : AEfecto
    {
        //pantalla
        public TgcSprite spriteFarol{ get; set; }

        public LuzFarol (TgcScene tgcEscena, CamaraFPS camaraFPS)
        {
            //efecto
            this.tgcEscena = tgcEscena;
            this.camaraFPS = camaraFPS;

            //pantalla
            spriteFarol = new TgcSprite();
            spriteFarol.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\farol.png");
        }
        

        public override void init()
        {
            GuiController.Instance.Modifiers.addColor("farolColor", Color.LightYellow);
            GuiController.Instance.Modifiers.addFloat("farolIntensidad", 0f, 150f, 3f);
            GuiController.Instance.Modifiers.addFloat("farolAtenuacion", 0.1f, 2f, 2f);
            GuiController.Instance.Modifiers.addFloat("farolEspecularEx", 0, 20, 1f);

            //Modifiers de material
            GuiController.Instance.Modifiers.addColor("farolEmissive", Color.Black);
            GuiController.Instance.Modifiers.addColor("farolAmbient", Color.LightYellow);
            GuiController.Instance.Modifiers.addColor("farolDiffuse", Color.White);
            GuiController.Instance.Modifiers.addColor("farolSpecular", Color.White);

            //pantalla
            Size screenSize = GuiController.Instance.Panel3d.Size;
            spriteFarol.Position = new Vector2(screenSize.Width - (screenSize.Width / 4), 0.50f * screenSize.Height);
            spriteFarol.Scaling = new Vector2((float)0.0003 * screenSize.Width, (float)0.0005 * screenSize.Height);
        }

        public override void render()
        {
            //efecto
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
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["farolColor"]));
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(lightPos));
                mesh.Effect.SetValue("eyePosition", TgcParserUtils.vector3ToFloat4Array(GuiController.Instance.FpsCamera.getPosition()));
                mesh.Effect.SetValue("lightIntensity", (float)GuiController.Instance.Modifiers["farolIntensidad"]);
                mesh.Effect.SetValue("lightAttenuation", (float)GuiController.Instance.Modifiers["farolAtenuacion"]);

                //Cargar variables de shader de Material. El Material en realidad deberia ser propio de cada mesh. Pero en este ejemplo se simplifica con uno comun para todos
                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["farolEmissive"]));
                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["farolAmbient"]));
                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["farolDiffuse"]));
                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["farolSpecular"]));
                mesh.Effect.SetValue("materialSpecularExp", (float)GuiController.Instance.Modifiers["farolEspecularEx"]);

                mesh.render();
            }


            //pantalla
            GuiController.Instance.Drawer2D.beginDrawSprite();
            spriteFarol.render();
            GuiController.Instance.Drawer2D.endDrawSprite();
        }
    }
}
