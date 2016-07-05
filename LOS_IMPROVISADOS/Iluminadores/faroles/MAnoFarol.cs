using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.faroles
{
    class ManoFarol : AManoPantalla
    {
        public ManoFarol()
        {
            sprite = new TgcSprite();
            sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\manoFarol.png");

            /*posX = 0.8f;
            posY = 0.6f;
            escX = 0.0004f;
            escY = 0.0009f;*/
            posX = 0.8f;
            posY = 0.53f;
            escX = 0.00048f;
            escY = 0.00095f;
        }

        public override void init()
        {
            Size screenSize = ScreenSizeClass.ScreenSize;
            sprite.Position = new Vector2(screenSize.Width * posX, posY * screenSize.Height);
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
