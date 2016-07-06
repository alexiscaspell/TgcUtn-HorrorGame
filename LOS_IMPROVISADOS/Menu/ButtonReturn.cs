using Microsoft.DirectX;
using System;

namespace AlumnoEjemplos.MiGrupo
{
    internal class ButtonReturn:GameButton
    {
        private GameMenu menuAnterior;
        public Vector2 posicionBoton = new Vector2(0.77f, 0.6f);

        internal void setMenuAnterior(GameMenu menuAnterior)
        {
            this.menuAnterior = menuAnterior;
        }

        internal void init()
        {
            base.init("botonVolver", posicionBoton);
        }

        public override void execute(EjemploAlumno app, GameMenu menu)
        {
            app.menuActual = menuAnterior;

            //habria que destruir menu
        }

    }
}