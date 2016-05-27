using System;

namespace AlumnoEjemplos.MiGrupo
{
    internal class FactoryMenu
    {
        public FactoryMenu()
        {
        }

        internal GameMenu menuPrincipal()
        {
            return new GameMenu("menuFondo");
        }
    }
}