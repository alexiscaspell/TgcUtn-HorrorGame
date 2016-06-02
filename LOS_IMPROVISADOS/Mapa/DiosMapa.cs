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

        public DiosMapa()
        {
            instancia = this;
        }

        #endregion

        public Mapa mapa;

        private float factorAvance;//Es el porcentaje del mapa q se quiere ir avanzando

        private Vector3 vectorAvance;

        private List<List<Punto>> vias;


        public void init(float factorAvance)
        {
            mapa = Mapa.Instance;
            this.factorAvance = factorAvance;

            vias = new List<List<Punto>>();
        }

        internal List<List<Punto>> getMatrix()
        {
            return vias;
        }

        internal bool estaCerca(Vector3 posicion, Vector3 otraPosicion)
        {
            return ((otraPosicion - posicion).Length() < 100);
        }

        public void generarMatriz()
        {
            Vector3 pMax = mapa.escena.BoundingBox.PMax;
            Vector3 pMin = mapa.escena.BoundingBox.PMin;

            Vector3 medidasMapa = pMax - pMin;

            vectorAvance = factorAvance * pMax;//(new Vector3(medidasMapa.X, 0, medidasMapa.Z));

            for (float i = pMin.X; i <pMax.X; i += vectorAvance.X)
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

                    bool collide = mapa.colisionaEsfera(puntoActual.getSphere());
                    //punto.setActivo(!collide);//Si no colisiona entonces esta en true
                    if (collide)
                    {
                        filaActual.Remove(puntoActual);
                    }
                }
            }
            /*
            foreach (List<Punto> filaActual in vias)
            {
                foreach (Punto punto in filaActual)
                {
                    bool collide = mapa.colisionaEsfera(punto.getSphere());
                    //punto.setActivo(!collide);//Si no colisiona entonces esta en true
                    if (collide)
                    {
                        filaActual.Remove(punto);
                    }
                }
            }*/
        }
    }
}
