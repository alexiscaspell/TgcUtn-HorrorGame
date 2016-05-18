using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils._2D;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA
{
    abstract class ABateria
    {
        public int cantidadDesgaste { get; set; }

        public int cargaActual { get; set; }

        public TgcText2d texto { get; set; }

        public TgcSprite sprite { get; set; }

        public DateTime tiempoAnterior { get; set; }

        public Size screenSize { get; set; }


        public ABateria()
        {
            cargaActual = 100;
            texto = new TgcText2d();
            sprite = new TgcSprite();
            screenSize = GuiController.Instance.Panel3d.Size;
        }

        public bool tenesBateria()
        {
            return cargaActual > 0;
        }

        public void gastarBateria(int cantidad)
        {
            if (cargaActual == 0) return;

            TimeSpan tiempoTranscurrido = DateTime.Now.Subtract(tiempoAnterior);

            if (tiempoTranscurrido.Seconds >= cantidadDesgaste && tenesBateria())
            {
                cargaActual-= cantidad;
                tiempoAnterior = DateTime.Now;
            }
        }

        public abstract void recargar();        
        public abstract void init();
        public abstract void render();
    }
}
