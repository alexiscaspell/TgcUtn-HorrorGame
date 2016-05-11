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

        public BateriaLinterna() : base()
        {
            cantidadDesgaste = 1;
            cantidadBaterias = 5; //lo pongo en 5 para probar
            cantidadRecarga = 50;
        }

        public override void init()
        {
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

            //Ubicarlo centrado en la pantalla
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
        }

        private void actualizarSprite()
        {
            if (cargaActual >= 80)
            {
                spriteActual = listaSprites.ElementAt(5);
            }
            else if (cargaActual >= 60)
            {
                spriteActual = listaSprites.ElementAt(4);
            }
            else if (cargaActual >= 40)
            {
                spriteActual = listaSprites.ElementAt(3);
            }
            else if (cargaActual >= 20)
            {
                spriteActual = listaSprites.ElementAt(2);
            }
            else if (cargaActual > 0)
            {
                spriteActual = listaSprites.ElementAt(1);
            }
            else if (cargaActual == 0)
            {
                spriteActual = listaSprites.ElementAt(0);
            }
        }

        public override void recargar()
        {
            if (cantidadBaterias == 0 || cargaActual == 100) return;

            cantidadBaterias--;
            cargarBateria(cantidadRecarga);

            actualizarSprite();
        }

        private void cargarBateria(int cantidadACargar)
        {
            if (cargaActual + cantidadACargar >= 100)
            {
                cargaActual = 100;
            }
            else
            {
                cargaActual = cargaActual + cantidadACargar;
            }
        }
    }
}
