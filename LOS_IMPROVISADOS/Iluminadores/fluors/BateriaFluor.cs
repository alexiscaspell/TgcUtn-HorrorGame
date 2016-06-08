using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.fluors
{
    class BateriaFluor : ABateria
    {

        public BateriaFluor() : base()
        {
            tiempoDesgaste = 1;
            cantidadDesgaste = 10;

            cantidadFluors = 5;
            sprite = new TgcSprite();
        }

        public override void init()
        {
            //barra verde
            sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\fluorIndicadorBarra.png");

            //texto
            texto.Color = Color.White;
            texto.Align = TgcText2d.TextAlign.LEFT;
            texto.Position = new Point(screenSize.Width / 40, screenSize.Height / 16);
            texto.Size = new Size(300, 100);
            texto.changeFont(new System.Drawing.Font("TimesNewRoman", 22, FontStyle.Bold));
        }

        public override void recargar()
        {
            if (cantidadFluors <= 0 || cargaActual != 0) return;

            cantidadFluors--;
            cargaActual = 100;
            
        }

        public override void render()
        {
            gastarBateria();

            //barra
            sprite.Scaling = new Vector2(cargaActual * 0.000002f * screenSize.Width, 0.0002f * screenSize.Height);
            sprite.Position = new Vector2(0.005f*screenSize.Width, 0.03f*screenSize.Height);

            GuiController.Instance.Drawer2D.beginDrawSprite();
            sprite.render();
            GuiController.Instance.Drawer2D.endDrawSprite();

            //texto
            texto.Text = "X" + cantidadFluors.ToString();
            texto.render();
        }
    }
}
