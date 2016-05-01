﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Efectos
{
    interface IEfecto
    {
        void init();
        void render();

        void crearVariablesDeUsuario();
        void crearModificadores();

        void usarVariablesDeUsuarioIniciales();
        void usarModificadoresIniciales();
    }
}
