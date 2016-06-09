using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Boss
{
    class ComportamientoSeguir : Comportamiento
    {

        public ComportamientoSeguir()
        {
            DiosMapa.Instance.initPersecucion();
        }
        public Vector3 proximoPunto(Vector3 posicionActual)
        {
            Punto puntoDondeTengoQueIr = DiosMapa.Instance.puntoASeguirPorElBoss();

            return vectorDelPuntoDondeTengoQueIr(puntoDondeTengoQueIr);
        }

        private Vector3 vectorDelPuntoDondeTengoQueIr(Punto punto)
        {
            Vector3 posicionDondeTengoQueIr = punto.getPosition();

            if (punto.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Count > 1)
            {
                int numeroMaximo = punto.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Max();

                DiosMapa.Instance.eliminarPuntosConPosicionesMenores(numeroMaximo);
            }
            else
            {
                DiosMapa.Instance.eliminarPuntoDeListaPersecucion(punto);
            }

            return posicionDondeTengoQueIr;
        }
        
    }
}
