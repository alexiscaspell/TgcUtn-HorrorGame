﻿using System;
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

        private const int CANTIDAD_DE_CUARTOS = 79;

        public Mapa()
        {
            TgcSceneLoader loader = new TgcSceneLoader();
            escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\mapasm-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\");

                instancia = this;

            /* if (!leerDatosDeArchivo())
             {
                 cargarDatosAArchivo();
             }*/

            mapearMapaALista();

            cargarDatosAArchivo();

            ColinaAzul.Instance.calcularBoundingBoxes(cuartos, CANTIDAD_DE_CUARTOS);
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

            for (int i = 0; i < CANTIDAD_DE_CUARTOS; i++)
            {
                cuartos.Add("r_"+(i + 1).ToString(), new List<TgcMesh>());
            }
            cuartos.Add("otros", new List<TgcMesh>());

            foreach (TgcMesh mesh in escena.Meshes)
            {
                string index = mesh.Name;
                index = index.Split('[')[0];

                List<TgcMesh> auxList = new List<TgcMesh>();

                if (index[0]=='r')
                {
                    auxList = cuartos[index];
                }
                else
                {
                    auxList = cuartos["otros"];
                }

                auxList.Add(mesh);
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