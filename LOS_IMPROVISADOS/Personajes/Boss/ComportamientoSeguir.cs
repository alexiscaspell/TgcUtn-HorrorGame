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
            pFinal = DiosMapa.Instance.obtenerPuntoPorPosicion(posicionInicial);

            //hardcodeo punto para probar
            Punto p1 = DiosMapa.Instance.obtenerPuntoPorPosicion(new Vector3(1262,0,3120));
            p1.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Add(DiosMapa.Instance.contadorSiguienteABuscar());
            Punto p2 = DiosMapa.Instance.obtenerPuntoPorPosicion(new Vector3(1479, 0, 3418));
            p2.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Add(DiosMapa.Instance.contadorSiguienteABuscar());
            Punto p3 = DiosMapa.Instance.obtenerPuntoPorPosicion(new Vector3(1262, 0, 4255));
            p3.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Add(DiosMapa.Instance.contadorSiguienteABuscar());
            Punto p4 = DiosMapa.Instance.obtenerPuntoPorPosicion(new Vector3(1262, 0, 5906));
            p4.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Add(DiosMapa.Instance.contadorSiguienteABuscar());

            DiosMapa.Instance.agregarPuntoAListaPersecucion(p1);
            DiosMapa.Instance.agregarPuntoAListaPersecucion(p2);
            DiosMapa.Instance.agregarPuntoAListaPersecucion(p3);
            DiosMapa.Instance.agregarPuntoAListaPersecucion(p4);
        }

        public Vector3 proximoPunto(Vector3 posicionActual)
        {
            Punto puntoActual = DiosMapa.Instance.obtenerPuntoPorPosicion(posicionActual);

            if (!DiosMapa.Instance.listaPersecucionEstaVacia())
            {
                if (puntoActual == pFinal)
                {
                    DiosMapa.Instance.eliminarPuntoDeListaPersecucion(puntoActual);
                    
                    pFinal = DiosMapa.Instance.puntoASeguirPorElBoss();

                    actualizarListaPersecucionSiElPersonajePaso2Veces();
                }
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
