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

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas
{
    class ManoLinterna : AManoPantalla
    {
        public ManoLinterna()
        {
            sprite = new TgcSprite();
            sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\manoLinterna.png");
        }

        public override void init()
        {
<<<<<<< HEAD
            Size screenSize = ScreenSizeClass.ScreenSize;//GuiController.Instance.Panel3d.Size;
=======
            Size screenSize = ScreenSizeClass.ScreenSize;
>>>>>>> master
            sprite.Position = new Vector2(screenSize.Width / 2, 0.75f * screenSize.Height);
            sprite.Scaling = new Vector2((float)0.0002 * screenSize.Width, (float)0.0003 * screenSize.Height);
        }

        public override void render()
        {
            GuiController.Instance.Drawer2D.beginDrawSprite();
            sprite.render();
            GuiController.Instance.Drawer2D.endDrawSprite();
        }
    }
}
