using TgcViewer.Utils._2D;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA
{
    abstract class AManoPantalla
    {
        public TgcSprite sprite { get; set; }
        public float posX;
        public float posY;
        public float escX;
        public float escY;

        abstract public void init();
        abstract public void render();
    }
}
