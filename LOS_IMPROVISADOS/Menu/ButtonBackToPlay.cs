using Microsoft.DirectX;

namespace AlumnoEjemplos.MiGrupo
{
    internal class ButtonBackToPlay:GameButton
    {
        public void init()
        {
            base.init("botonVolver", new Vector2(0.77f, 0.8f));
        }

        public override void execute(EjemploAlumno app, GameMenu menu)
        {
            app.playing = true;
        }
    }
}