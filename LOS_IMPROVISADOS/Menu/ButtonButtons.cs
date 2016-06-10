using AlumnoEjemplos.MiGrupo;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Menu
{
    class ButtonButtons:GameButton
    {
        public override void execute(EjemploAlumno app, GameMenu menu)
        {
            FactoryMenu factory = FactoryMenu.Instance;

            factory.setMenuAnterior(menu);

            GameMenu menuObjetivo = factory.menuBotones();

            app.menuActual = menuObjetivo;
        }

        public void init()
        {
            base.init("botonTeclas", new Vector2(0.72f, 0.44f));
        }
    }
}
