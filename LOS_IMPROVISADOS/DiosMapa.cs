using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class DiosMapa
    {
        public Mapa mapa;

        private float factorAvance;//Es el porcentaje del mapa q se quiere ir avanzando

        private Vector3 vectorAvance;

        private List<List<Punto>> vias;

        public void init(float factorAvance,Mapa mapa)
        {
            this.mapa = mapa;
            this.factorAvance = factorAvance;

            vias = new List<List<Punto>>();
        }

        public void generarMatriz()
        {
            Vector3 pMax = mapa.escena.BoundingBox.PMax;
            Vector3 pMin = mapa.escena.BoundingBox.PMin;

            Vector3 medidasMapa = pMax - pMin; 

            vectorAvance = factorAvance*(new Vector3(medidasMapa.X, 0, medidasMapa.Z));

            for (float i = pMin.X; i <pMax.X; i += vectorAvance.X)
            {
                List<Punto> fila = new List<Punto>();

                for (float j = pMin.Z; j < pMax.Z; j += vectorAvance.Z)
                {
                    fila.Add(new Punto(i, j, true));
                }
                vias.Add(fila);
            }

            detectarColisionesVias();
        }

        private void detectarColisionesVias()
        {
            foreach (List<Punto> filaActual in vias)
            {
                foreach (Punto punto in filaActual)
                {
                    bool collide = mapa.colisionaEsfera(punto.getSphere());
                    punto.setActivo(!collide);//Si no colisiona entonces esta en true
                }
            }
        }
    }
}
