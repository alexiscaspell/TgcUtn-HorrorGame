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
                new DiosMapa();
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
                    vectoresDePtos.Add(ptoActual.getPosition());
                }

                Vector3 closestPoint = TgcCollisionUtils.closestPoint(point, vectoresDePtos.ToArray(), out d);
                puntosMasCercanos.Add(closestPoint);

                vectoresDePtos.Clear();
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
            instancia = this;
        }

        #endregion

        public Mapa mapa;

        private float factorAvance;//Es el porcentaje del mapa q se quiere ir avanzando

        private Vector3 vectorAvance;

        private List<List<Punto>> vias;

        //lobo
        private List<Punto> listaDePuntosPersecucion;
        private int contadorDePuntosQueElPersonajeVaPasando;

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
                    }
                }
            }
        }

        Dictionary<Punto, List<Punto>> caminos = new Dictionary<Punto, List<Punto>>();

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

            int cantVias = vias.Count - 1;

            if (i == 0 && j == 0)
            {
                puntos.Add(vias[0][1]);
                puntos.Add(vias[1][0]);
            }
            else if (i == cantVias && j == cantVias)
            {
                puntos.Add(vias[cantVias - 1][cantVias]);
                puntos.Add(vias[cantVias][cantVias - 1]);
            }
            else if (i == cantVias && j == 0)
            {
                puntos.Add(vias[cantVias][1]);
                puntos.Add(vias[cantVias - 1][0]);
            }
            else if (i == 0 && j == cantVias)
            {
                puntos.Add(vias[0][cantVias - 1]);
                puntos.Add(vias[1][cantVias]);
            }

            else if (((i > 0 && i < cantVias) && (j == 0 || j == cantVias)))
            {
                puntos.Add(vias[i - 1][j]);
                puntos.Add(vias[i + 1][j]);

                if (j == cantVias)
                {
                    puntos.Add(vias[i][j - 1]);
                }
                else
                {
                    puntos.Add(vias[i][j + 1]);
                }
            }

            else if (((j > 0 && j < cantVias) && (i == 0 || i == cantVias)))
            {
                puntos.Add(vias[i][j - 1]);
                puntos.Add(vias[i][j + 1]);

                if (i == cantVias)
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

        //lobo
        public void agregarPuntoAListaPersecucion(Punto puntoAAgregar)
        {
            if (!listaDePuntosPersecucion.Contains(puntoAAgregar))
            {
                listaDePuntosPersecucion.Add(puntoAAgregar);
            }
        }

        public int contadorSiguienteABuscar()
        {
            contadorDePuntosQueElPersonajeVaPasando++;
            /*
            if (contadorDePuntosQueElPersonajeVaPasando > 5000) //esto es solo para que el numero no tienda a infinito
            {
                contadorDePuntosQueElPersonajeVaPasando = 1;
            }*/

            return contadorDePuntosQueElPersonajeVaPasando;
        }

        public void eliminarPuntoDeListaPersecucion(Punto puntoAEliminar)
        {
            listaDePuntosPersecucion.Remove(puntoAEliminar);
        }

        public Punto puntoASeguirPorElBoss()
        {
            return listaDePuntosPersecucion.First();
        }

        public void eliminarPuntosConPosicionesMenores(int posicion)
        {
            foreach (Punto punto in listaDePuntosPersecucion)
            {
                punto.listaDeOrdenEnQueElPersonajePasoPorEstePunto.RemoveAll(x => x < posicion);

                if (punto.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Count == 0)
                {
                    eliminarPuntoDeListaPersecucion(punto);
                }
            }
        }
    }
}
