using TgcViewer.Example;
using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Input;

using Microsoft.DirectX.Direct3D;
using AlumnoEjemplos.LOS_IMPROVISADOS;

namespace AlumnoEjemplos.MiGrupo
{
    public class EjemploAlumno : TgcExample
    {
        private TgcScene tgcEscena;
        private CamaraFPS camaraFPS;

        private Personaje personaje;

       // private Bateria bateriaLinterna;
        private Caja cajaInteraccion;
        private Palanca palanca;
        private Boss boss;

        public override string getCategory()
        {
            return "AlumnoEjemplos";
        }
        
        public override string getName()
        {
            return "Los Improvisados";
        }
        
        public override string getDescription()
        {
            return "Juego de Terror - Juego de terror en primera persona basado en juegos famosos como Amnesia, Outlast, Penumbra, etc";
        }


        public override void init()
        {
            Device d3dDevice = GuiController.Instance.D3dDevice;
            
            TgcSceneLoader loader = new TgcSceneLoader();
            tgcEscena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\mapaScene-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\");

            camaraFPS = new CamaraFPS(new Vector3(50,45,200/*280f, 25f, 60f*/), new Vector3(270f, 45f, 60f));
                camaraFPS.init();

            personaje = new Personaje(tgcEscena, camaraFPS);
                personaje.iniciarIluminadores();

            boss = new Boss(camaraFPS);
            boss.init(30f,new Vector3(100,0,100));

            cajaInteraccion = new Caja();
            cajaInteraccion.init();

            palanca = new Palanca();
            palanca.init();
            /*
            bateriaLinterna = new Bateria();
            bateriaLinterna.init(3);*/
        }


        public override void render(float elapsedTime)
        {
            TgcD3dInput d3dInput = GuiController.Instance.D3dInput;
            Device d3dDevice = GuiController.Instance.D3dDevice;

            if (d3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.E))
            {
                personaje.cambiarASiguienteIluminador();
            }

            if (d3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.R))
            {
                personaje.recargarBateriaLinterna();
            }

            camaraFPS.render();

            personaje.renderizarIluminador();

            boss.update(elapsedTime);
            boss.render();

            cajaInteraccion.render(camaraFPS);
            palanca.render();
            //bateriaLinterna.render();            
        }


        public override void close()
        {
            tgcEscena.disposeAll();
        }

    }
}
