using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA
{
    class Iluminador
    {
        public ALuz efecto { get; set; }
        public ABateria bateria { get; set; }
        public AManoPantalla pantalla { get; set; }

        public Iluminador(ALuz efecto, AManoPantalla pantalla, ABateria bateria)
        {
            this.efecto = efecto;
            this.pantalla = pantalla;
            this.bateria = bateria;
        }

        public void init()
        {
            efecto.init();
            pantalla.init();
            bateria.init();
        }
        public void render()
        {
            efecto.render();
            pantalla.render();
            bateria.render();
        }
    }
}