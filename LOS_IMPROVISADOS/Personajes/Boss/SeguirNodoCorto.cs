using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Boss
{
    class SeguirNodoCorto : Comportamiento
    {
        private DiosMapa gps;

        private Vector3 proximoPto = new Vector3(0, 0, 0);

        private bool primeraIteracion = true;

        public SeguirNodoCorto()
        {
            gps = DiosMapa.Instance;
        }

        public Vector3 proximoPunto(Vector3 posicionActual)
        {
            Vector3 closestPoint = gps.puntoMasCercano(posicionActual);

            Vector3 proxPto;

            if (closestPoint == proximoPto || primeraIteracion)
            {
                List<Punto> puntos = gps.getCaminos(closestPoint);

                proxPto = primerActivo(puntos);

                proximoPto = proxPto;

                if (primeraIteracion)
                {
                    primeraIteracion = false;
                }
            }

            else
            {
                proxPto = proximoPto;
            }

            return proxPto;
        }

        private Vector3 primerActivo(List<Punto> puntos)
        {
            foreach (Punto item in puntos)
            {
                if (item.activo)
                {
                    return item.getPosition();
                }
            }

            return new Vector3(0, 0, 0);
        }

    }
}
