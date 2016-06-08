using Microsoft.DirectX;
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

        public List<TgcMesh> escenaFiltrada { get; set; }
        
        public List<Accionable> objetos {get; set;}

        private Dictionary<string, Puerta> puertas = new Dictionary<string, Puerta>();

        private const int CANTIDAD_DE_CUARTOS = 79;
        private const int CANTIDAD_DE_PUERTAS = 15;
        private const int CANTIDAD_DE_PUERTAS_PZ = 11;

        public Mapa()
        {
            TgcSceneLoader loader = new TgcSceneLoader();
            escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\mapa-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\");

            objetos = HardCodeadorObjetos.HardCodearObjetos();
            
            instancia = this;

            /* if (!leerDatosDeArchivo())
             {
                 cargarDatosAArchivo();
             }*/

            cargarDatosAArchivo();

            mapearMapaALista();

            ColinaAzul.Instance.calcularBoundingBoxes(cuartos, CANTIDAD_DE_CUARTOS);

            mapearPuertas();

            agregarObjetosMapa();

            escenaFiltrada = new List<TgcMesh>();

            updateEscenaFiltrada();
        }

        private void mapearPuertas()
        {
            foreach (TgcMesh mesh in cuartos["otros"])
            {
                if (mesh.Name[0]=='q')//NINGUN MESH PUEDE EMPEZAR CON Q!!!
                {
                    mapearPuerta(mesh);
                }
            }
        }

        private void mapearPuerta(TgcMesh mesh)
        {
            int nroPuerta = 0;

            string nombrePuerta = mesh.Name;

            if (nombrePuerta[1] == 'z')
            {
                nroPuerta = Convert.ToInt32(nombrePuerta.Split('z')[1]);
            }

            Puerta nuevaPuerta = new Puerta(nroPuerta,mesh);

            puertas.Add(nombrePuerta, nuevaPuerta);
        }

        private void agregarObjetosMapa()
        {
            foreach (TgcMesh mesh in cuartos["otros"])
            {
                string cuarto = ColinaAzul.Instance.aQueCuartoPertenece(mesh);

                if (cuarto != "")
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
                if (item.Name[0]=='q')
                {
                    writer.Add(item.Name);
                };
            }
        }


        private void mapearMapaALista()
        {
            cuartos = new Dictionary<string, List<TgcMesh>>();

            initDiccionario();

            foreach (TgcMesh mesh in escena.Meshes)
            {
                string index = parsearMesh(mesh);

                if (cuartos.ContainsKey(index))
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

            if (!relacionesCuartos.ContainsKey(index) && cuartos.ContainsKey(index))
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

        internal string[] obtenerContiguos(string cuarto)
        {
            return relacionesCuartos[cuarto];
        }

        internal void activarObjetos()
        {
        	foreach(Accionable a in objetos)
        	{
        		a.acciona(Personaje.Instance.camaraFPS.camaraFramework.Position, Personaje.Instance.camaraFPS.camaraFramework.viewDir);
        	}
        }

        internal void dispose()
        {
            escena.disposeAll();
        }

        /*ACA DEBERIA DE HABER LOGICA ENTRE RELACIONES DE LOS CUARTOS*/
        internal bool colisionaPersonaje(TgcBoundingSphere cuerpoPersonaje, ref TgcBoundingBox obstaculo)
        {
            foreach (TgcMesh mesh in cuartos[ColinaAzul.Instance.dondeEstaPesonaje()])
            {
                if (ColinaAzul.Instance.colisionaEsferaCaja(cuerpoPersonaje, mesh.BoundingBox))
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

        public void updateEscenaFiltrada()
        {
            string nombreCuarto = ColinaAzul.Instance.dondeEstaPesonaje();

            if (escenaFiltrada.Count > 0)
            {
                escenaFiltrada.Clear();
            }

            escenaFiltrada = clonarLista(cuartos[nombreCuarto]);

            foreach (string otroCuarto in relacionesCuartos[nombreCuarto])
            {

                if (cuartos.ContainsKey(otroCuarto))
                {
                    string index = otroCuarto;

                    if (otroCuarto[0] == 'p')
                    {
                        index = relacionesCuartos[otroCuarto][0];

                        foreach (TgcMesh mesh in cuartos[otroCuarto])
                        {
                            escenaFiltrada.Add(mesh);//Aca agrego el mesh de la puerta, esto se tiene q cambiar
                        }
                        //escenaFiltrada.Add(puertas[otroCuarto].getMesh());//ACA ROMPE PORQUE LAS PUERTAS QUE AGREGO TIENEN OTRA KEY!!!

                        if (index == nombreCuarto && relacionesCuartos[otroCuarto].Count() > 1)
                        {
                            index = relacionesCuartos[otroCuarto][1];
                        }
                    }

                    foreach (TgcMesh mesh in cuartos[index])
                    {
                        escenaFiltrada.Add(mesh);
                    }
                }
            }
            
            //Agrego todos los accionables
            foreach(Accionable a in objetos)
            {
            	escenaFiltrada.Add(a.getMesh());
            }
            
            
        }

        private List<TgcMesh> clonarLista(List<TgcMesh> list)
        {
            List<TgcMesh> listaClon = new List<TgcMesh>();

            foreach (TgcMesh mesh in list)
            {
                listaClon.Add(mesh);
            }

            return listaClon;
        }
    }
}
