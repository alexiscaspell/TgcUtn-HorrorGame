using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Bateria
    {
        //private Vector2 posicion;

        private TgcSprite bateriaActual;
        //private TgcAnimatedSprite bateriaActual;

        private List<TgcSprite> listaBaterias;

        private int cantBateria;

        private System.DateTime tiempoAnterior;

        private int velocidadDePerdida;

        public void init(int velocidadPerdida)
        {
            cantBateria = 5;

            velocidadDePerdida = velocidadPerdida;

            inicializarBaterias();

            bateriaActual = listaBaterias.ElementAt(cantBateria);
            /*string pathSprite = GuiController.Instance.AlumnoEjemplosDir + "Media\\Texturas\\baterias.png";
            Size frameSize = new Size(257,110);
            bateriaActual = new TgcAnimatedSprite(pathSprite, frameSize, 4, 200f);
            Size screenSize = GuiController.Instance.Panel3d.Size;
            bateriaActual.Scaling = new Vector2((float)0.0002 * screenSize.Width, (float)0.0005 * screenSize.Height);
            bateriaActual.Position = new Vector2(0, 20);*///ESTONO SE XQ NO FUNCA!!!!

            tiempoAnterior = System.DateTime.Now;
            
        }

        private void inicializarBaterias()
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

            listaBaterias = new List<TgcSprite>();

            listaBaterias.Add(sprite0);
            listaBaterias.Add(sprite1);
            listaBaterias.Add(sprite2);
            listaBaterias.Add(sprite3);
            listaBaterias.Add(sprite4);
            listaBaterias.Add(sprite5);
            
        }

        public void render()
        {
            TimeSpan tiempoTranscurrido = DateTime.Now.Subtract(tiempoAnterior);

            if (tiempoTranscurrido.Seconds.Equals(velocidadDePerdida)&&cantBateria>0)
            {
                cantBateria--;
                tiempoAnterior = DateTime.Now;
                bateriaActual = listaBaterias.ElementAt(cantBateria);
            }

            GuiController.Instance.Drawer2D.beginDrawSprite();
            bateriaActual.render();//updateAndRender();
            GuiController.Instance.Drawer2D.endDrawSprite();
        }
    }
}
