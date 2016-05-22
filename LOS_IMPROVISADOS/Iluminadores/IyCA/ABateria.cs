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
        /*duracionTotal = cargaTotal * tiempoDesgaste / cantidadDesgaste (esta es la formula para saber cada cuanto tiempo bajar bateria 
                                                                   y cuanto segun la cantidad que queres que dure la bateria)*/

        public int cantidadDesgaste { get; set; }//Esta es la cantidad de carga que se baja pasado el tiempo de desgaste

        public int tiempoDesgaste { get; set; }//Son los segundos que tienen que pasar para que se baje la carga

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

        public void gastarBateria()//int cantidad)
        {
            if (cargaActual == 0) return;

            TimeSpan tiempoTranscurrido = DateTime.Now.Subtract(tiempoAnterior);

            int tiempoTranscurridoEnSegundos = tiempoTranscurrido.Minutes * 60 + tiempoTranscurrido.Seconds;

            if (/*tiempoTranscurrido.Seconds*/tiempoTranscurridoEnSegundos >= tiempoDesgaste && tenesBateria())
            {
                cargaActual -= cantidadDesgaste;//cantidad;
                tiempoAnterior = DateTime.Now;
            }
        }

        public abstract void recargar();        
        public abstract void init();
        public abstract void render();
    }
}
