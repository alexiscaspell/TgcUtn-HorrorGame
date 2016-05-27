using AlumnoEjemplos.LOS_IMPROVISADOS.Menu;
using System;

namespace AlumnoEjemplos.MiGrupo
{
    internal class FactoryMenu
    {
        private GameMenu menuAnterior;

        public FactoryMenu()
        {
        }

        internal GameMenu menuPrincipal()
        {
            GameMenu menu = new GameMenu("menuFondo");

            ButtonStart start = new ButtonStart();
            start.init();

            ButtonObjective objetivos = new ButtonObjective();
            objetivos.init();

            ButtonButtons botones = new ButtonButtons();
            botones.init();

            ButtonExit exit = new ButtonExit();
            exit.init();

            menu.agregarBoton(start);
            menu.agregarBoton(objetivos);
            menu.agregarBoton(botones);
            menu.agregarBoton(exit);

            return menu;
        }

        internal GameMenu menuObjetivos()
        {
            GameMenu menu = new GameMenu("menuFondo");

            ButtonReturn volver = new ButtonReturn();
            volver.init();
            volver.setMenuAnterior(menuAnterior);

            menu.agregarBoton(volver);

            return menu;
        }

        internal void setMenuAnterior(GameMenu menu)
        {
            menuAnterior = menu;
        }
    }
}