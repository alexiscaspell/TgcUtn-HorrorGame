using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        
        public ABateria()
        {
            cargaActual = 100;
            texto = new TgcText2d();
            sprite = new TgcSprite();
        }

        public bool tenesBateria()
        {
            return cargaActual > 0;
        }

        public abstract void recargar();
        
        public abstract void init();
        public abstract void render();
    }
}
