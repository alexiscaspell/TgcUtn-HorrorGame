using Microsoft.DirectX;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;
using System;
using AlumnoEjemplos.LOS_IMPROVISADOS;

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
            Size screenSize = ScreenSizeClass.ScreenSize;
            Size textureSize = spriteBoton.Texture.Size;

            spriteBoton.Position = new Vector2(position.X * screenSize.Width, position.Y * screenSize.Height);
            spriteBoton.Scaling = new Vector2(0.00025f*screenSize.Width, 0.0006f*screenSize.Height);
            normalScale = spriteBoton.Scaling;
        }

        public abstract void execute(EjemploAlumno app,GameMenu menu);

        internal void render()
        {
            update();

            spriteBoton.Scaling = normalScale;

            if (selected)
            {
                spriteBoton.Scaling = 1.1f * spriteBoton.Scaling;
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