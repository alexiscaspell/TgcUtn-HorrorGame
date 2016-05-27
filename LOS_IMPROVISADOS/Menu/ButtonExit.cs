using Microsoft.DirectX;
using System;

namespace AlumnoEjemplos.MiGrupo
{
    internal class ButtonExit : GameButton
    {
        public void init()
        {
            base.init("botonExit2", new Vector2(0.72f, 0.58f));
        }

        public override void execute(EjemploAlumno app, GameMenu menu)
        {
            //Por ahora nada
        }
    }
}