using AlumnoEjemplos.MiGrupo;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Menu
{
    class ButtonObjective:GameButton
    {
        public override void execute(EjemploAlumno app, GameMenu menu)
        {
            FactoryMenu factory = FactoryMenu.Instance;

            factory.setMenuAnterior(menu);

            GameMenu menuObjetivo = factory.menuObjetivos();

            app.menuActual = menuObjetivo;
        }

        public void init()
        {
            base.init("botonObjetivo", new Vector2(0.72f, 0.3f));
        }
    }
}
