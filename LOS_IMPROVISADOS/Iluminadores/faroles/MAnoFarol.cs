using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
            sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\farol.png");
        }

        public override void init()
        {
            Size screenSize = ScreenSizeClass.ScreenSize;
            sprite.Position = new Vector2(screenSize.Width - (screenSize.Width / 4), 0.50f * screenSize.Height);
            sprite.Scaling = new Vector2((float)0.0003 * screenSize.Width, (float)0.0005 * screenSize.Height);
        }

        public override void render()
        {
            GuiController.Instance.Drawer2D.beginDrawSprite();
            sprite.render();
            GuiController.Instance.Drawer2D.endDrawSprite();
        }
    }
}
