using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas
{
    class ManoLinterna : AManoPantalla
    {
        public ManoLinterna()
        {
            sprite = new TgcSprite();
            sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\manoFarol.png");//manoLinterna.png");

            posX = 0.45f;
            posY = 0.7f;
            escX = 0.0004f;
            escY = 0.0007f;
        }

        public override void init()
        {
            Size screenSize = ScreenSizeClass.ScreenSize;
            sprite.Position = new Vector2(screenSize.Width * posX, posY * screenSize.Height);
            sprite.Scaling = new Vector2(escX * screenSize.Width, escY * screenSize.Height); ;
        }

        public override void render()
        {
            GuiController.Instance.Drawer2D.beginDrawSprite();
            sprite.render();
            GuiController.Instance.Drawer2D.endDrawSprite();
        }
    }
}
