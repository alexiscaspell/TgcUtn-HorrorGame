using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA
{
    class Iluminador
    {
        public AEfecto efecto { get; set; }
        public Logica logica { get; set; }
        public APantalla pantalla { get; set; }

        public Iluminador(AEfecto efecto, APantalla pantalla, Logica logica)
        {
            this.efecto = efecto;
            this.pantalla = pantalla;
            this.logica = logica;
        }

        public void init()
        {
            efecto.init();
            pantalla.init();
            logica.init();
        }
        public void render()
        {
            efecto.render();
            pantalla.render();
            logica.render();
        }
    }
}