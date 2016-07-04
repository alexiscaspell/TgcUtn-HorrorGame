using AlumnoEjemplos.LOS_IMPROVISADOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Boss;
using TgcViewer;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class SeguirPersonaje : Comportamiento
    {
        private DiosMapa diosMapa;

        private Vector3 posAnterior = new Vector3(0, 0, 0);

        private Vector3 posActualAnterior;
        //private const float epsilon = 50;

        private bool primeraIteracion = true;

        private int cantVecesQueQuedoAtascado = 0;
        
        private const int cantMaxVecesAtascado = 1;//Pongo este para que el tipo no cambie tanto al algoritmo de brian

        public SeguirPersonaje()
        {
            diosMapa = DiosMapa.Instance;
        }
        public Vector3 proximoPunto(Vector3 posicionActual)
        {
            Vector3 posActualMapa = diosMapa.puntoMasCercano(posicionActual);
            Vector3 proximoPto = posAnterior;

            if (posActualMapa == posAnterior || primeraIteracion)
            {
                proximoPto = diosMapa.obtenerPuntoInteligente(CamaraFPS.Instance.camaraFramework.Position, posicionActual).getPosition();

                    if (proximoPto.Equals(posActualAnterior) || proximoPto.Equals(posActualMapa))
                    {
                        cantVecesQueQuedoAtascado++;
                    }

                posActualAnterior = posActualMapa;

                if (primeraIteracion)
                {
                    primeraIteracion = false;
                }
            }

            if (cantVecesQueQuedoAtascado>cantMaxVecesAtascado)
            {
                AnimatedBoss.Instance.comportamiento = new ComportamientoSeguir(posicionActual);

                proximoPto = AnimatedBoss.Instance.comportamiento.proximoPunto(posicionActual);
            }

            posAnterior = proximoPto;

            return proximoPto;
        }

        /*private bool sonIguales(Vector3 vec1, Vector3 vec2)
        {
            if ((vec1.X<=vec2.X+epsilon)&&(vec1.X>=vec2.X-epsilon))
            {
                if ((vec1.Z <= vec2.Z + epsilon) && (vec1.Z >= vec2.Z - epsilon))
                {
                    return true;
                }
            }
            return false;
        }*/
    }
}
