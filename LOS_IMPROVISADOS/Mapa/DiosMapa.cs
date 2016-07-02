using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class DiosMapa
    {
        #region Singleton
        private static volatile DiosMapa instancia = null;

        public static DiosMapa Instance
        {
            get
            { return newInstance(); }
        }

        internal static DiosMapa newInstance()
        {
            if (instancia != null) { }
            else
            {
                instancia = new DiosMapa();
            }
            return instancia;
        }

        internal Vector3 puntoMasCercano(Vector3 point)
        {
            float d;

            List<Punto> filaActual;

            List<Vector3> puntosMasCercanos = new List<Vector3>();

            for (int i = 0; i < vias.Count; i++)
            {
                filaActual = vias[i];

                List<Vector3> vectoresDePtos = new List<Vector3>();

                foreach (Punto ptoActual in filaActual)
                {
                    if (ptoActual.activo)
                    {
                        vectoresDePtos.Add(ptoActual.getPosition());
                    }
                }

                if (vectoresDePtos.Count>0)
                {
                    Vector3 closestPoint;

                    if (vectoresDePtos.Count==1)
                    {
                        closestPoint = vectoresDePtos[0];
                    }
                    else
                    {
                        closestPoint = TgcCollisionUtils.closestPoint(point, vectoresDePtos.ToArray(), out d);
                    }
                    
                    puntosMasCercanos.Add(closestPoint);

                    vectoresDePtos.Clear();
                }

            }

            return TgcCollisionUtils.closestPoint(point, puntosMasCercanos.ToArray(), out d);
        }

        internal List<Punto> getCaminos(Vector3 closestPoint)
        {
            foreach (Punto pto in caminos.Keys)
            {
                if (pto.getPosition().Equals(closestPoint))
                {
                    return caminos[pto];
                }
            }

            return new List<Punto>();
        }

        internal List<TgcBoundingSphere> boundingSpheres()
        {
            List<TgcBoundingSphere> esferas = new List<TgcBoundingSphere>();

            for (int i = 0; i < vias.Count; i++)
            {
                for (int j = 0; j < vias[i].Count; j++)
                {
                    if (vias[i][j].activo)
                    {
                        vias[i][j].getSphere().setRenderColor(System.Drawing.Color.Yellow);
                    }
                    else
                    {
                        vias[i][j].getSphere().setRenderColor(System.Drawing.Color.Red);
                    }
                    esferas.Add(vias[i][j].getSphere());
                }
            }
            return esferas;
        }

        internal Dictionary<Punto, List<Punto>> getCaminos()
        {
            return caminos;
        }

        internal void puntoMasCercano(ref int x, ref int z, Vector3 posicion)
        {
            Vector3 closestPoint = puntoMasCercano(posicion);

            for (int i = 0; i < vias.Count; i++)
            {
                for (int j = 0; j < vias[i].Count; j++)
                {
                    if (closestPoint.Equals(vias[i][j].getPosition()))
                    {
                        x = i;
                        z = j;
                        return;
                    }
                }
            }
        }

        internal void deleteMatriz()
        {
            //Por ahora no libero nada
        }

        private DiosMapa()
        {
        }

        #endregion

        public Mapa mapa;

        private float factorAvance;//Es el porcentaje del mapa q se quiere ir avanzando

        private Vector3 vectorAvance;

        private List<List<Punto>> vias;

        //lobo
        private List<Punto> listaDePuntosPersecucion;
        private int contadorDePuntosQueElPersonajeVaPasando;
        private int CANTIDAD_DE_PUNTOS_PARA_QUE_EL_BOSS_SE_TELETRANSPORTE = 40;
        private int PORCION_DE_PUNTOS_QUE_ELIMINO_CUANDO_EL_BOSS_SE_TELETRANSPORTA = 3;

        public void init(float factorAvance)
        {
            mapa = Mapa.Instance;
            this.factorAvance = factorAvance;

            vias = new List<List<Punto>>();


            //lobo
            listaDePuntosPersecucion = new List<Punto> { };
            contadorDePuntosQueElPersonajeVaPasando = 0;
        }

        internal List<List<Punto>> getMatrix()
        {
            return vias;
        }

        internal bool estaCerca(Vector3 posicion, Vector3 otraPosicion)
        {
            return ((otraPosicion - posicion).Length() < 100);
        }

        internal void activarODesactivarPunto(Vector3 position)
        {
            Punto punto = obtenerPuntoPorPosicion(position);
            punto.activo = !punto.activo;
        }


        public Punto obtenerPuntoPorPosicion(Vector3 puntoABuscar)
        {
            int x = 0;
            int z = 0;

            puntoMasCercano(ref x, ref z, puntoABuscar);

            return vias[x][z];
        }

        public void generarMatriz()
        {
            Vector3 pMax = mapa.escena.BoundingBox.PMax;
            Vector3 pMin = mapa.escena.BoundingBox.PMin;

            Vector3 medidasMapa = pMax - pMin;

            vectorAvance = factorAvance * pMax;//(new Vector3(medidasMapa.X, 0, medidasMapa.Z));

            for (float i = pMin.X; i < pMax.X; i += vectorAvance.X)
            {
                List<Punto> fila = new List<Punto>();

                for (float j = pMin.Z; j < pMax.Z; j += vectorAvance.Z)
                {
                    fila.Add(new Punto(i, j));
                }
                vias.Add(fila);
            }

            detectarColisionesVias();
        }

        /*private void generarCaminos()
        {
            int maximo = 0;

            for (int i = 0; i < vias.Count; i++)
            {
                encontrarSigCamino(i,0,maximo);
            }
            for (int i = 0; i < vias.Count; i++)
            {

            }
        }*/

        private void encontrarSigCamino(int x, int z, int maximo)
        {
            throw new NotImplementedException();
        }

        int contadorFalse = 0;
        private void detectarColisionesVias()
        {
            List<Punto> filaActual;
            Punto puntoActual;

            for (int i = 0; i < vias.Count; i++)
            {
                filaActual = vias[i];

                for (int j = 0; j < filaActual.Count; j++)
                {
                    puntoActual = filaActual[j];

                    bool collideObjects = mapa.colisionaEsfera(puntoActual.getSphere());
                    bool collideRoom = ColinaAzul.Instance.colisionaConAlgunCuarto(puntoActual.getSphere());
                    //punto.setActivo(!collide);//Si no colisiona entonces esta en true
                    if (!collideRoom || collideObjects)
                    {
                        puntoActual.activo = false;
                        contadorFalse++;
                    }
                }
            }
        }

        internal Punto obtenerPuntoInteligente(Vector3 vector3)
        {
            if (puntoAnteriorPersonaje==null)
            {
                puntoAnteriorPersonaje = obtenerPuntoPorPosicion(vector3);
                return puntoAnteriorPersonaje;
            }

            Punto ptoMasCercano = caminos[puntoAnteriorPersonaje][0];

            foreach (Punto item in caminos[puntoAnteriorPersonaje])
            {
                if ((vector3 - item.getPosition()).Length()<(vector3-ptoMasCercano.getPosition()).Length())
                {
                    ptoMasCercano = item;
                }
            }

            puntoAnteriorPersonaje = ptoMasCercano;

            return ptoMasCercano;
        }

        Dictionary<Punto, List<Punto>> caminos = new Dictionary<Punto, List<Punto>>();
        private Punto puntoAnteriorPersonaje = null;

        public void generarCaminos()
        {
            for (int i = 0; i < vias.Count; i++)
            {
                for (int j = 0; j < vias.Count; j++)
                {
                    caminos.Add(vias[i][j], obtenerPuntosCercanos(i, j));
                }
            }

        }

        private List<Punto> obtenerPuntosCercanos(int i, int j)
        {
            List<Punto> puntos = new List<Punto>();

            int cantViasX = vias.Count - 1;

            int cantViasJ = vias[i].Count - 1;

            if (i == 0 && j == 0)
            {
                puntos.Add(vias[0][1]);
                puntos.Add(vias[1][0]);
            }
            else if (i == cantViasX && j == cantViasJ)
            {
                puntos.Add(vias[cantViasX - 1][cantViasJ]);
                puntos.Add(vias[cantViasX][cantViasJ - 1]);
            }
            else if (i == cantViasX && j == 0)
            {
                puntos.Add(vias[cantViasX][1]);
                puntos.Add(vias[cantViasX - 1][0]);
            }
            else if (i == 0 && j == cantViasJ)
            {
                puntos.Add(vias[0][cantViasJ - 1]);
                puntos.Add(vias[1][cantViasJ]);
            }

            else if (((i > 0 && i < cantViasX) && (j == 0 || j == cantViasJ)))
            {
                puntos.Add(vias[i - 1][j]);
                puntos.Add(vias[i + 1][j]);

                if (j == cantViasJ)
                {
                    puntos.Add(vias[i][j - 1]);
                }
                else
                {
                    puntos.Add(vias[i][j + 1]);
                }
            }

            else if (((j > 0 && j < cantViasJ) && (i == 0 || i == cantViasX)))
            {
                puntos.Add(vias[i][j - 1]);
                puntos.Add(vias[i][j + 1]);

                if (i == cantViasX)
                {
                    puntos.Add(vias[i - 1][j]);
                }
                else
                {
                    puntos.Add(vias[i + 1][j]);
                }
            }

            else
            {
                puntos.Add(vias[i + 1][j]);
                puntos.Add(vias[i][j + 1]);
                puntos.Add(vias[i - 1][j]);
                puntos.Add(vias[i][j - 1]);
            }

            return puntos;
        }

        /******************************************************************/
        /***************************INTELIGENCIA***************************/
        /******************************************************************/

        public void agregarPuntoAListaPersecucion(Punto puntoAAgregar)
        {
            if (!listaDePuntosPersecucion.Contains(puntoAAgregar))
            {
                listaDePuntosPersecucion.Add(puntoAAgregar);
            }
        }

        public int contadorSiguienteABuscar()
        {
            return contadorDePuntosQueElPersonajeVaPasando++;
        }

        public void eliminarPuntoDeListaPersecucion(Punto puntoAEliminar)
        {
            listaDePuntosPersecucion.Remove(puntoAEliminar);
        }

        public Punto puntoASeguirPorElBoss()
        {
            /*if (cantidadDeElementosDeListaPersecucion() > CANTIDAD_DE_PUNTOS_PARA_QUE_EL_BOSS_SE_TELETRANSPORTE)
            {
                int cantidad = (cantidadDeElementosDeListaPersecucion() / PORCION_DE_PUNTOS_QUE_ELIMINO_CUANDO_EL_BOSS_SE_TELETRANSPORTA);
                listaDePuntosPersecucion.RemoveRange(0, cantidad); 


                //la logica esta asi porque lo teletransporto en una de las posiciones donde paso el pj
                 //si se quiere que se teletransporte a un punto random, habria que vaciar la lista y volverla a cargar desde la posicion nueva del pj
                 
                AnimatedBoss.Instance.cambiarPosicionDelBoss(DiosMapa.instancia.puntoASeguirPorElBoss().getPosition());
            }*/
        
            return listaDePuntosPersecucion[0];
        }

        public void eliminarPuntosConPosicionesMenores(int numeroMaximo)
        {
            List<Punto> puntosAEliminar = new List<Punto> { };

            //primero consigo los puntos que quiero eliminar
            foreach (Punto punto in listaDePuntosPersecucion)
            {
                punto.listaDeOrdenEnQueElPersonajePasoPorEstePunto.RemoveAll(x => x < numeroMaximo);

                if (punto.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Count == 0)
                {
                    puntosAEliminar.Add(punto);
                }
            }

            //ahora los elimino
            listaDePuntosPersecucion.RemoveAll(x => puntosAEliminar.Contains(x));
            /*no elimino el punto dentro del if porque no confio en borrar elementos de la misma lista que estoy recorriendo*/
        }

        public bool listaPersecucionEstaVacia()
        {
            return (listaDePuntosPersecucion.Count == 0);
        }

        public int cantidadDeElementosDeListaPersecucion()
        {
            return listaDePuntosPersecucion.Count;
        }

        public void eliminarPrimerPuntoDeListaPersecucion()
        {
            listaDePuntosPersecucion.RemoveAt(0);
        }

        public void reiniciarPersecucion()
        {
            if (listaDePuntosPersecucion.Count>0)
            {
                foreach (Punto punto in listaDePuntosPersecucion)
                {
                    punto.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Clear();
                }
                listaDePuntosPersecucion.Clear();

                contadorDePuntosQueElPersonajeVaPasando = 0;
            }
        }

        public void elminarPrimerosPuntosDePersecucion(int cantidadPuntos)
        {
            listaDePuntosPersecucion.RemoveRange(0, cantidadPuntos);
        }
    }
}
