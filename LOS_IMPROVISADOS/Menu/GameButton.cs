using Microsoft.DirectX;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;
using System;

namespace AlumnoEjemplos.MiGrupo
{
     public abstract class GameButton
    {
        public TgcSprite spriteBoton;
        private bool selected = false;
        private Vector2 normalScale;

        public void init(string image,Vector2 position)
        {
            spriteBoton = new TgcSprite();
            spriteBoton.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir
                                                                 + "Media\\Menu\\"+image+".png");

            Vector2 scaling = new Vector2(27, 25);

            Size screenSize = GuiController.Instance.Panel3d.Size;
            Size textureSize = spriteBoton.Texture.Size;

            spriteBoton.Position = 0.1f * (new Vector2(position.X * screenSize.Width,position.Y*screenSize.Height));
            spriteBoton.Scaling = 0.01f * (new Vector2(scaling.X*(screenSize.Width/textureSize.Width), scaling.Y * (screenSize.Height/textureSize.Height)));
            normalScale = spriteBoton.Scaling;
        }

        public abstract void execute(EjemploAlumno app,GameMenu menu);

        internal void render()
        {
            update();

            spriteBoton.Scaling = normalScale;

            if (selected)
            {
                spriteBoton.Scaling = 1.02f * spriteBoton.Scaling;
            }

            spriteBoton.render();
        }

        private void update()
        {

        }

        public void select()
        {
            selected = true;
        }
        public void unSelect()
        {
            selected = false;
        }
    }
}