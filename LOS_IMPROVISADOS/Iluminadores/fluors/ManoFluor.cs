using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.fluors
{
    class ManoFluor : AManoPantalla
    {
        public ManoFluor()
        {
            sprite = new TgcSprite();
            sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\manoFluor.png");

            posX = 0.205f;
            posY = 0.1f;
            escX = 0.0004f;
            escY = 0.0004f;
        }

        public override void init()
        {
            Size screenSize = ScreenSizeClass.ScreenSize;
            sprite.Position = new Vector2(screenSize.Width - (screenSize.Width * posX), posY * screenSize.Height);
            sprite.Scaling = new Vector2(escX * screenSize.Width, escY * screenSize.Height);
        }

        public override void render()
        {
            GuiController.Instance.Drawer2D.beginDrawSprite();
            sprite.render();
            GuiController.Instance.Drawer2D.endDrawSprite();
        }
    }
}
