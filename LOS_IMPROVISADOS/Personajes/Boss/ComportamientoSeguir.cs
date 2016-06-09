using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Boss
{
    class ComportamientoSeguir : Comportamiento
    {
        public Punto pFinal;

        public ComportamientoSeguir(Vector3 posicionInicial)
        {
            DiosMapa.Instance.reiniciarPersecucion();
            pFinal = DiosMapa.Instance.obtenerPuntoPorPosicion(posicionInicial);
        }

        public Vector3 proximoPunto(Vector3 posicionActual)
        {
            Punto puntoActual = DiosMapa.Instance.obtenerPuntoPorPosicion(posicionActual);

            if (puntoActual == pFinal && !DiosMapa.Instance.listaPersecucionEstaVacia())
            {
                DiosMapa.Instance.eliminarPuntoDeListaPersecucion(puntoActual);

                pFinal = DiosMapa.Instance.puntoASeguirPorElBoss();

                actualizarListaPersecucionSiElPersonajePaso2Veces();
            }

            return pFinal.getPosition();
        }

        private void actualizarListaPersecucionSiElPersonajePaso2Veces()
        {
            if (pFinal.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Count > 1)
            {
                int numeroMaximo = pFinal.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Max();

                DiosMapa.Instance.eliminarPuntosConPosicionesMenores(numeroMaximo);
            }
        }
    }
}
