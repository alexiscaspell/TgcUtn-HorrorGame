using AlumnoEjemplos.LOS_IMPROVISADOS.Menu;
using System;

namespace AlumnoEjemplos.MiGrupo
{
    internal class FactoryMenu
    {
        private GameMenu menuAnterior;
        private EjemploAlumno application;

        #region Singleton
        private static volatile FactoryMenu instancia = null;

        public static FactoryMenu Instance
        {
            get
            { return newInstance(); }
        }

        internal static FactoryMenu newInstance()
        {
            if (instancia != null) { }
            else
            {
                instancia = new FactoryMenu();
            }
            return instancia;
        }

        #endregion
        //NUNCA USAR NEW, USAR Instance

        internal GameMenu menuPrincipal()
        {
            GameMenu menu = new GameMenu("menuFondo");
            menu.init(application);

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
            GameMenu menu = new GameMenu("menuObjetivo");
            menu.init(application);

            ButtonReturn volver = new ButtonReturn();
            volver.posicionBoton.Y = 0.8f;
            volver.init();
            volver.setMenuAnterior(menuAnterior);

            menu.agregarBoton(volver);

            return menu;
        }

        internal GameMenu menuBotones()
        {
            GameMenu menu = new GameMenu("botonesMenu");
            menu.init(application);

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

        internal void setApplication(EjemploAlumno ejemploAlumno)
        {
            application = ejemploAlumno;
        }

        internal GameMenu menuPausa()
        {
            GameMenu menu = new GameMenu("menuFondo");
            menu.init(application);

            ButtonBackToPlay volver = new ButtonBackToPlay();
            volver.init();

            menu.agregarBoton(volver);

            return menu;
        }
    }
}