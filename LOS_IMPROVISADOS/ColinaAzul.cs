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
            cuerpoFicticioPersonaje = new TgcBoundingSphere(CamaraFPS.Instance.posicion, 10f);
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

            cuartoAnterior = bloquesCuartos.Keys.ElementAt(0);//Inicializo cuarto
        }

        public TgcBoundingBox calcularBoundingBox(List<TgcMesh> cuarto)
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
            //TgcBoundingBox boxMesh = mesh.BoundingBox;
            TgcBoundingSphere esferaMesh = new TgcBoundingSphere(mesh.BoundingBox.calculateBoxCenter() , 10f);

            foreach (string nombreCuarto in bloquesCuartos.Keys)
            {
                /*if (colisionEntreCajas(boxMesh,bloquesCuartos[nombreCuarto]))
                {
                    return nombreCuarto;
                }*/
                if (colisionaEsferaCaja(esferaMesh,bloquesCuartos[nombreCuarto]))
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

        private string cuartoAnterior;

        public string dondeEstaPesonaje()
        {
            cuerpoFicticioPersonaje.setCenter(CamaraFPS.Instance.camaraFramework.Position);

            if (colisionaEsferaCaja(cuerpoFicticioPersonaje,bloquesCuartos[cuartoAnterior]))
            {
                return cuartoAnterior;
            }

            foreach (string cuarto in Mapa.Instance.obtenerContiguos(cuartoAnterior))
            {
                if (bloquesCuartos.ContainsKey(cuarto))
                {
                    if(colisionaEsferaCaja(cuerpoFicticioPersonaje, bloquesCuartos[cuarto]))
                    {
                        cuartoAnterior = cuarto;
                        return cuarto;
                    }
                }
            }

            foreach (string nombreCuarto in bloquesCuartos.Keys)
            {
                if (colisionaEsferaCaja(cuerpoFicticioPersonaje,bloquesCuartos[nombreCuarto]))
                {
                    cuartoAnterior = nombreCuarto;
                    return nombreCuarto;
                }
            }
            return cuartoAnterior;
        }

        internal bool colisionaConAlgunCuarto(TgcBoundingSphere tgcBoundingSphere)
        {
            foreach (TgcBoundingBox cuarto in bloquesCuartos.Values)
            {
                if (colisionaEsferaCaja(tgcBoundingSphere,cuarto))
                {
                    return true;
                }
            }
            return false;
        }

        internal string getCuartoIn(Vector3 posicion)
        {
            TgcBoundingSphere esfera = new TgcBoundingSphere(posicion, 1f);

            foreach (string cuarto in bloquesCuartos.Keys)
            {
                if (colisionaEsferaCaja(esfera,bloquesCuartos[cuarto]))
                {
                    return cuarto;
                }
            }

            return "";
        }

        internal bool estoyEn(string cuarto,Vector3 posicion)
        {
            TgcBoundingSphere esfera = new TgcBoundingSphere(posicion, 10f);

            return colisionaEsferaCaja(esfera, bloquesCuartos[cuarto]);
        }
    }
}
