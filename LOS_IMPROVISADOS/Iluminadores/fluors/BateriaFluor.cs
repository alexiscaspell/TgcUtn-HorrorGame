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
        public int cantidadFluors { get; set; }

        public TgcSprite spriteBarra { get; set; }

        public BateriaFluor() : base()
        {
            tiempoDesgaste = 2;//Gasta bateria cada 6seg
            cantidadDesgaste = 10;//Gasta 10 barras por vez

            //cantidadDesgaste = 2;

            cantidadFluors = 5;
            spriteBarra = new TgcSprite();
        }

        public override void init()
        {   /*
            //fluor
            sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\fluorIndicador.png");
            sprite.Scaling = new Vector2((float)0.0004 * screenSize.Width, (float)0.0004 * screenSize.Height);
            sprite.Position = new Vector2(20, 20);
            //posicion del texto : texto.Position = new Point(screenSize.Width / 25, screenSize.Height / 20);
            */

            //barra verde

            spriteBarra.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\fluorIndicadorBarra.png");

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

            /*
            //fluor
            GuiController.Instance.Drawer2D.beginDrawSprite();
            sprite.render();
            GuiController.Instance.Drawer2D.endDrawSprite();
            */

            //barra
            spriteBarra.Scaling = new Vector2(cargaActual * 0.000002f * screenSize.Width, 0.0002f * screenSize.Height);
            spriteBarra.Position = new Vector2(0.005f*screenSize.Width, 0.03f*screenSize.Height);//new Vector2(10, 20);

            GuiController.Instance.Drawer2D.beginDrawSprite();
            spriteBarra.render();
            GuiController.Instance.Drawer2D.endDrawSprite();

            //texto
            texto.Text = "X" + cantidadFluors.ToString();
            texto.render();
        }
    }
}
