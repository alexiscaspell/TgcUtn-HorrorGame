using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas
{
    class BateriaLinterna : ABateria
    {
        public int cantidadBaterias { get; set; }
        public int cantidadRecarga { get; set; }
        
        private TgcSprite spriteActual;
        private List<TgcSprite> listaSprites;
        private DateTime tiempoAnterior;

        private TgcText2d textoCantidadBaterias;

        public BateriaLinterna() : base()
        {
            cantidadDesgaste = 5; //gasta una linea cada 5 segundos
            cantidadBaterias = 4;
            cantidadRecarga = 2;

            cargaActual = 6;
        }

        public override void init()
        {
            //bateria
            TgcSprite sprite0 = new TgcSprite();
            sprite0.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\bateria0.png");
            TgcSprite sprite1 = new TgcSprite();
            sprite1.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\bateria1.png");
            TgcSprite sprite2 = new TgcSprite();
            sprite2.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\bateria2.png");
            TgcSprite sprite3 = new TgcSprite();
            sprite3.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\bateria3.png");
            TgcSprite sprite4 = new TgcSprite();
            sprite4.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\bateria4.png");
            TgcSprite sprite5 = new TgcSprite();
            sprite5.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\bateria5.png");

            Size screenSize = GuiController.Instance.Panel3d.Size;

            sprite0.Scaling = new Vector2((float)0.0002 * screenSize.Width, (float)0.0005 * screenSize.Height);
            sprite1.Scaling = new Vector2((float)0.0002 * screenSize.Width, (float)0.0005 * screenSize.Height);
            sprite2.Scaling = new Vector2((float)0.0002 * screenSize.Width, (float)0.0005 * screenSize.Height);
            sprite3.Scaling = new Vector2((float)0.0002 * screenSize.Width, (float)0.0005 * screenSize.Height);
            sprite4.Scaling = new Vector2((float)0.0002 * screenSize.Width, (float)0.0005 * screenSize.Height);
            sprite5.Scaling = new Vector2((float)0.0002 * screenSize.Width, (float)0.0005 * screenSize.Height);

            sprite0.Position = new Vector2(0, 20);
            sprite1.Position = new Vector2(0, 20);
            sprite2.Position = new Vector2(0, 20);
            sprite3.Position = new Vector2(0, 20);
            sprite4.Position = new Vector2(0, 20);
            sprite5.Position = new Vector2(0, 20);

            listaSprites = new List<TgcSprite>() {sprite0, sprite1, sprite2, sprite3, sprite4, sprite5};

            //texto
            textoCantidadBaterias = new TgcText2d();
            textoCantidadBaterias.Color = Color.White;
            textoCantidadBaterias.Align = TgcText2d.TextAlign.LEFT;
            textoCantidadBaterias.Position = new Point(screenSize.Width/9, screenSize.Height/32);
            textoCantidadBaterias.Size = new Size(300, 100);
            textoCantidadBaterias.changeFont(new System.Drawing.Font("TimesNewRoman", 25, FontStyle.Bold));

        }

        public override void render()
        {
            TimeSpan tiempoTranscurrido = DateTime.Now.Subtract(tiempoAnterior);

            if (tiempoTranscurrido.Seconds >= cantidadDesgaste && tenesBateria())
            {
                cargaActual--;
                tiempoAnterior = DateTime.Now;

                actualizarSprite();
            }
            
            GuiController.Instance.Drawer2D.beginDrawSprite();
            spriteActual.render();
            GuiController.Instance.Drawer2D.endDrawSprite();


            //texto
            textoCantidadBaterias.Text = "x" + cantidadBaterias.ToString();
            textoCantidadBaterias.render();
        }

        private void actualizarSprite()
        {
            spriteActual = listaSprites.ElementAt(cargaActual);
        }

        public override void recargar()
        {
            if (cantidadBaterias == 0 || cargaActual == 6) return;

            cantidadBaterias--;
            cargarBateria(cantidadRecarga);

            actualizarSprite();
        }

        private void cargarBateria(int cantidadACargar)
        {
            if (cargaActual + cantidadACargar >= 5)
            {
                cargaActual = 5;
            }
            else
            {
                cargaActual = cargaActual + cantidadACargar;
            }
        }
    }
}
