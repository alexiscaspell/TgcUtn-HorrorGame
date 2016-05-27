using Microsoft.DirectX;
using System;

namespace AlumnoEjemplos.MiGrupo
{
    internal class ButtonExit : GameButton
    {
        public void init()
        {
            base.init("botonExit2", new Vector2(7.8f, 7.3f));
        }

        public override void execute(EjemploAlumno app, GameMenu menu)
        {
            //Por ahora nada
        }
    }
}