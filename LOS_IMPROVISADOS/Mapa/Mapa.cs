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
    class Mapa
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

        public Mapa()
        {
            TgcSceneLoader loader = new TgcSceneLoader();
            escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\mapa-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\");

                instancia = this;

            if (!leerDatosDeArchivo())
            {
                MessageBox.Show("asdasdasd");
                cargarDatosAArchivo();
            }
            else
                MessageBox.Show("gggggg");
        }

        private bool leerDatosDeArchivo()
        {
            string directorio = GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\archivoMapa.txt";

            if (!File.Exists(directorio)) return false;

            System.IO.StreamReader archivoDatos = new System.IO.StreamReader(directorio);
            bool tieneAlgo = File.ReadAllLines(directorio).Length > 0;

            if (tieneAlgo)
            {
                //Aca leo el archivo
            }

            return tieneAlgo;
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
                writer.Add(item.UserProperties["Name"]);
            }
        }

        internal void dispose()
        {
            escena.disposeAll();
        }

        public bool colisionaEsfera(TgcBoundingSphere esfera,ref TgcBoundingBox obstaculo)
        {

            foreach (TgcMesh mesh in escena.Meshes)
            {
                if (TgcCollisionUtils.testSphereAABB(esfera,mesh.BoundingBox))
                {
                    obstaculo = mesh.BoundingBox;

                    return true;
                }
            }
            return false;
        }
        public bool colisionaEsfera(TgcBoundingSphere esfera)
        {
            TgcBoundingBox b = new TgcBoundingBox();
            return colisionaEsfera(esfera, ref b);
        }

    }
}
