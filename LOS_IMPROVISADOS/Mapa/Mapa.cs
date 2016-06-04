using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TgcViewer;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    public class Mapa
    {
        #region Singleton
        private static volatile Mapa instancia = null;

        public static Mapa Instance
        {
            get
            { return newInstance(); }
        }

        internal static Mapa newInstance()
        {
            if (instancia != null) { }
            else
            {
               new Mapa();
            }
            return instancia;
        }

        #endregion

        public TgcScene escena { get; set; }

        private Dictionary<string, List<TgcMesh>> cuartos;

        private Dictionary<string, string[]> relacionesCuartos = new Dictionary<string, string[]>();

        public List<TgcMesh> escenaFiltrada
        {
            get
            {
                return updateEscenaFiltrada();//Actualiza la escena a renderizar
            }
        }

        private const int CANTIDAD_DE_CUARTOS = 79;
        private const int CANTIDAD_DE_PUERTAS = 15;
        private const int CANTIDAD_DE_PUERTAS_PZ = 11;

        public Mapa()
        {
            TgcSceneLoader loader = new TgcSceneLoader();
            escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\mapa-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\");

                instancia = this;

            /* if (!leerDatosDeArchivo())
             {
                 cargarDatosAArchivo();
             }*/

            cargarDatosAArchivo();

            mapearMapaALista();

            ColinaAzul.Instance.calcularBoundingBoxes(cuartos, CANTIDAD_DE_CUARTOS);

            agregarObjetosMapa();
        }

        private void agregarObjetosMapa()
        {
            foreach (TgcMesh mesh in cuartos["otros"])
            {
                string cuarto = ColinaAzul.Instance.aQueCuartoPertenece(mesh);

                if (cuarto!="")
                {
                    cuartos[cuarto].Add(mesh);
                }
            }

            cuartos["otros"].Clear();//Me falta cargar las puertas
        }

        private void cargarDatosAArchivo()
        {
            string directorio = GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\archivoMapa.txt";

            List<string> writer = new List<string>();

            mapearMapaATesto(writer);

            //Aca mapeo las cosas y las guardo en un archivo de tesssssto

            File.WriteAllLines(directorio, writer);
        }

        private void mapearMapaATesto(List<string> writer)
        {
            foreach (TgcMesh item in escena.Meshes)
            {
                writer.Add(item.Name);
            }
        }


        private void mapearMapaALista()
        {
            cuartos = new Dictionary<string, List<TgcMesh>>();

            initDiccionario();

            foreach (TgcMesh mesh in escena.Meshes)
            {
                string index = parsearMesh(mesh);

                if(cuartos.ContainsKey(index))
                {
                    cuartos[index].Add(mesh);
                }
                else
                {
                    cuartos["otros"].Add(mesh);
                }
            }
        }

        private string parsearMesh(TgcMesh mesh)
        {
            string index = mesh.Name.Split('_')[0];

            if (!relacionesCuartos.ContainsKey(index))
            {
                string[] lista = null;

                string iterador;

                iterador = mesh.Name.Split('[')[1];

                iterador = iterador.Split(']')[0];

                lista = iterador.Split(';');

                relacionesCuartos.Add(index, lista);
            }

            return index;
        }

        private void initDiccionario()
        {
            for (int i = 1; i <= CANTIDAD_DE_CUARTOS; i++)
            {
                cuartos.Add("r" + i.ToString(), new List<TgcMesh>());
                if (i <= CANTIDAD_DE_PUERTAS)
                {
                    cuartos.Add("p" + i.ToString(), new List<TgcMesh>());
                }
                if (i <= CANTIDAD_DE_PUERTAS_PZ)
                {
                    cuartos.Add("pz" + i.ToString(), new List<TgcMesh>());
                }
            }
            cuartos.Add("otros", new List<TgcMesh>());
        }

        internal void dispose()
        {
            escena.disposeAll();
        }

        /*ACA DEBERIA DE HABER LOGICA ENTRE RELACIONES DE LOS CUARTOS*/
        internal bool colisionaEsfera(TgcBoundingSphere esfera,ref TgcBoundingBox obstaculo)
        {
            foreach (TgcMesh mesh in escena.Meshes)
            {
                if (ColinaAzul.Instance.colisionaEsferaCaja(esfera,mesh.BoundingBox))
                {
                    obstaculo = mesh.BoundingBox;
                    return true;
                }
            }
            return false;
        }

        internal bool colisionaEsfera(TgcBoundingSphere esfera)
        {
            foreach (TgcMesh mesh in escena.Meshes)
            {
                if (ColinaAzul.Instance.colisionaEsferaCaja(esfera, mesh.BoundingBox))
                {
                    return true;
                }
            }
            return false;
        }

        private List<TgcMesh> updateEscenaFiltrada()
        {
            string nombreCuarto = ColinaAzul.Instance.dondeEstaPesonaje();

            return cuartos[nombreCuarto];
        }

    }
}
