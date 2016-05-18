using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using System;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.faroles
{
    class BateriaFarol : ABateria
    {
        public BateriaFarol(): base()
        {
            cantidadDesgaste = 1;
        }

        public override void init()
        {
            //farol
            sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\farolIndicador.png");

            sprite.Scaling = new Vector2((float)0.0002 * screenSize.Width, (float)0.0002 * screenSize.Height);

            sprite.Position = new Vector2(20, 20);

            //texto
            texto.Color = Color.White;
            texto.Align = TgcText2d.TextAlign.LEFT;
            texto.Position = new Point(screenSize.Width / 20, screenSize.Height / 8);
            texto.Size = new Size(300, 100);
            texto.changeFont(new System.Drawing.Font("TimesNewRoman", 22, FontStyle.Bold));
        }

        public override void recargar()
        {
            //esta objeto solo recarga cuando este cerca de un recipiente con aceite
        }

        public override void render()
        {
            gastarBateria(1);
            //farol
            GuiController.Instance.Drawer2D.beginDrawSprite();
            sprite.render();
            GuiController.Instance.Drawer2D.endDrawSprite();


            //texto
            texto.Text = cargaActual.ToString() + "%";
            texto.render();
        }
        
    }
}
