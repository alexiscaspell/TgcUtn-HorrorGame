using AlumnoEjemplos.LOS_IMPROVISADOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class SeguirPersonaje : Comportamiento
    {
        private DiosMapa diosMapa;

        public SeguirPersonaje()
        {
            diosMapa = DiosMapa.Instance;
        }
        public Vector3 proximoPunto(Vector3 posicionActual)
        {
            Vector3 ptoMasCercano = diosMapa.puntoMasCercano(CamaraFPS.Instance.camaraFramework.Position);

            return ptoMasCercano;
        }
    }
}
