using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA
{
    abstract class ABateria
    {
        public int cantidadDesgaste { get; set; }

        public int cargaActual { get; set; }

        public ABateria()
        {
            cargaActual = 100;
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
