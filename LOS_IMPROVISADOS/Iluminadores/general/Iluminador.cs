using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.general
{
    class Iluminador
    {
        public ALuz luz { get; set; }
        public ABateria bateria { get; set; }
        public AManoPantalla mano { get; set; }

        public ALuz oscuridad { get; set; }

        public Iluminador(ALuz luz, AManoPantalla mano, ABateria bateria)
        {
            this.luz = luz;
            this.mano = mano;
            this.bateria = bateria;

            oscuridad = new LuzOscura(luz.tgcEscena, luz.camaraFPS);

            init();
        }

        public void init()
        {
            luz.init();
            mano.init();
            bateria.init();
        }
        public void render()
        {
            if (bateria.tenesBateria())
            {
                luz.render();
                mano.render();
                bateria.render();
            }
            else
            {
                bateria.render();
                oscuridad.render();
            }            
        }

        public void dispose()
        {
            //no se si hay que hacer el dispose de los sprites y demas
        }
    }
}