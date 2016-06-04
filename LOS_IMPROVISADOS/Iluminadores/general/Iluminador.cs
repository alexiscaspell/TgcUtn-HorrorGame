using System;
using TgcViewer.Utils.Sound;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.general
{
    class Iluminador
    {
        public ALuz luz { get; set; }
        public ABateria bateria { get; set; }
        public AManoPantalla mano { get; set; }

        public ALuz oscuridad { get; set; }
        
        private TgcStaticSound sonidoPrenderOApagar;

        public bool iluminadorActivado;

        public Iluminador(ALuz luz, AManoPantalla mano, ABateria bateria, TgcStaticSound sonido)
        {
            this.luz = luz;
            this.mano = mano;
            this.bateria = bateria;
            this.sonidoPrenderOApagar = sonido;
				
            oscuridad = new LuzOscura(luz.mapa, luz.camaraFPS);

            init();
        }

        public void init()
        {
            luz.init();
            mano.init();
            bateria.init();
            iluminadorActivado = true;
        }
        public void render()
        {
            if (!iluminadorActivado)
            {
                oscuridad.render();
            }
            else
            {
                if (bateria.tenesBateria())
                {
                    luz.render();
                    mano.render();
                }
                else
                {
                    oscuridad.render();
                }

                bateria.render();
            }            
        }

        public void dispose()
        {
            //no se si hay que hacer el dispose de los sprites y demas
        }

        internal void prender()
        {
            iluminadorActivado = true;
            bateria.prender();
        }

        internal void apagarOPrender()
        {
            iluminadorActivado = !iluminadorActivado;
            bateria.apagarOPrender();
            sonidoPrenderOApagar.play(false);
        }
    }
}