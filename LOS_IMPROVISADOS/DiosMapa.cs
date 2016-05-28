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

        List<Punto> vias;

        public void init(float factorAvance)
        {
            this.factorAvance = factorAvance;
            vias = new List<Punto>();
        }

        public void generarMatriz()
        {
            Vector3 pMax = mapa.escena.BoundingBox.PMax;
            Vector3 pMin = mapa.escena.BoundingBox.PMin;

            Vector3 medidasMapa = pMax - pMin; 

            vectorAvance = factorAvance*(new Vector3(medidasMapa.X, 0, medidasMapa.Z));

            //Punto[,] matrizPtos = new Punto[Convert.ToInt32(medidasMapa.X / factorAvance), Convert.ToInt32(medidasMapa.Z / factorAvance)];

            for (float i = pMin.X; i <pMax.X; i += vectorAvance.X)
            {
                for (float j = pMin.Z; j < pMax.Z; j += vectorAvance.Z)
                {
                    Punto punto = new Punto(i,j,true);
                    vias.Add(punto);
                }
            }

            foreach (Punto punto in vias)
            {
              bool collide = mapa.colisionaEsfera(punto.getSphere());//Por ahora lo hago cn el mapa pero despues va a ser cn mas cosas

                if (collide)
                {
                    //punto.setActivo(false);
                    vias.Remove(punto);
                }
            }

        }

    }
}
