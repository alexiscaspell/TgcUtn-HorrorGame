using TgcViewer.Example;
using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Input;

using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using TgcViewer.Utils.Shaders;
using System.Drawing;
using TgcViewer.Utils.TgcGeometry;
using AlumnoEjemplos.LOS_IMPROVISADOS;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas;


namespace AlumnoEjemplos.MiGrupo
{
    public class Prueba : TgcExample
    {

        const float MOVEMENT_SPEED = 10f;
        private Vector3 movement;

        /////VARIABLES GLOBALES/////
        private TgcScene tgcEscena;

        private LuzLinterna luzLinterna;

        private CamaraFPS camaraFPS;

        private Caja cajaInteraccion;
        

        private Palanca palanca;

        private TgcBox cajaPrueba;

        public override string getCategory()
        {
            return "AlumnoEjemplos";
        }

        public override string getName()
        {
            return "Prueba";
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
                GuiController.Instance.AlumnoEjemplosDir + "Media\\habitacionMiedo\\habitacionMiedo-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\habitacionMiedo\\");


            camaraFPS = CamaraFPS.Instance;
            camaraFPS.init();

            //luzLinterna = new LuzLinterna(tgcEscena, camaraFPS);
            luzLinterna.init();

            cajaInteraccion = new Caja();
            cajaInteraccion.init();

            palanca = new Palanca();
            palanca.init();
            

            cajaPrueba = TgcBox.fromSize(new Vector3(0, 0, 0), new Vector3(10, 10, 10), Color.Blue);

        }


        public override void render(float elapsedTime)
        {


            TgcD3dInput d3dInput = GuiController.Instance.D3dInput;
            Device d3dDevice = GuiController.Instance.D3dDevice;

            camaraFPS.update();
            camaraFPS.render();
            luzLinterna.render();

            cajaInteraccion.render(camaraFPS);
            palanca.render();

            //////////////////MUESTRO LOS OBJETOS//////////////////
            //tgcEscena.renderAll();

            //Hago que la caja siga al jugador
            movement = camaraFPS.posicion;
            movement.Subtract(cajaPrueba.Position);
            movement.Normalize();

            movement *= MOVEMENT_SPEED * elapsedTime;

            cajaPrueba.move(movement);

            cajaPrueba.updateValues();
            cajaPrueba.render();
        }


        public override void close()
        {
            tgcEscena.disposeAll();
        }

    }
}
