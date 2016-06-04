using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class ColinaAzul
    {
        #region Singleton
        private static volatile ColinaAzul instancia = null;

        public static ColinaAzul Instance
        {
            get
            { return newInstance(); }
        }

        internal static ColinaAzul newInstance()
        {
            if (instancia != null) { }
            else
            {
                instancia = new ColinaAzul();
            }
            return instancia;
        }

        #endregion

        Dictionary<string,TgcBoundingBox> bloquesCuartos;

        private TgcBoundingSphere cuerpoFicticioPersonaje;

        private ColinaAzul()
        {
            bloquesCuartos = new Dictionary<string, TgcBoundingBox>();
            cuerpoFicticioPersonaje = new TgcBoundingSphere(CamaraFPS.Instance.posicion, 10);
        }

        public void calcularBoundingBoxes(Dictionary<string,List<TgcMesh>> cuartos,int cantidadCuartos)
        {
            List<TgcMesh> cuarto;
            for (int i = 1; i <= cantidadCuartos; i++)
            {
                string index = "r" + i.ToString();

                cuarto = cuartos[index];
                //cuartos.TryGetValue("r_"+i, out cuarto);

                if (cuarto.Count>0)
                {
                    bloquesCuartos.Add("r"+i.ToString(),calcularBoundingBox(cuarto));
                }
            }
        }

        private TgcBoundingBox calcularBoundingBox(List<TgcMesh> cuarto)
        {
            Vector3 pMin = cuarto[0].BoundingBox.PMin;
            Vector3 pMax = cuarto[0].BoundingBox.PMax;

            foreach (TgcMesh mesh in cuarto)
            {
                pMin = menoresCoordenadasDe(pMin, mesh.BoundingBox.PMin);

                pMax = mayoresCoordenadasDe(pMax, mesh.BoundingBox.PMax);
            }

            return new TgcBoundingBox(pMin, pMax);
        }

        internal string aQueCuartoPertenece(TgcMesh mesh)
        {
            TgcBoundingBox boxMesh = mesh.BoundingBox;

            foreach (string nombreCuarto in bloquesCuartos.Keys)
            {
                if (colisionEntreCajas(boxMesh,bloquesCuartos[nombreCuarto]))
                {
                    return nombreCuarto;
                }
            }
            return "";
        }

        private bool colisionEntreCajas(TgcBoundingBox box,TgcBoundingBox otherBox)
        {
            TgcCollisionUtils.BoxBoxResult result = TgcCollisionUtils.classifyBoxBox(box, otherBox);

            return (result==TgcCollisionUtils.BoxBoxResult.Adentro||result==TgcCollisionUtils.BoxBoxResult.Atravesando);
        }

        public bool colisionaEsferaCaja(TgcBoundingSphere esfera,TgcBoundingBox box)
        {
            return TgcCollisionUtils.testSphereAABB(esfera, box);
        }

        private Vector3 menoresCoordenadasDe(Vector3 point, Vector3 otherPoint)
        {
            return new Vector3(FastMath.Min(point.X, otherPoint.X),
            FastMath.Min(point.Y, otherPoint.Y),
            FastMath.Min(point.Z, otherPoint.Z));
        }

        private Vector3 mayoresCoordenadasDe(Vector3 point, Vector3 otherPoint)
        {
            return new Vector3(FastMath.Max(point.X, otherPoint.X),
            FastMath.Max(point.Y, otherPoint.Y),
            FastMath.Max(point.Z, otherPoint.Z));
        }

        public string dondeEstaPesonaje()
        {
            cuerpoFicticioPersonaje.setCenter(CamaraFPS.Instance.camaraFramework.Position);

            foreach (string nombreCuarto in bloquesCuartos.Keys)
            {
                if (colisionaEsferaCaja(cuerpoFicticioPersonaje,bloquesCuartos[nombreCuarto]))
                {
                    return nombreCuarto;
                }
            }
            return "";
        }
    }
}
