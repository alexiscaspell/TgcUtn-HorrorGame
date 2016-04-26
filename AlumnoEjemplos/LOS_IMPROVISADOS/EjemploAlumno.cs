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

namespace AlumnoEjemplos.MiGrupo
{
    public class EjemploAlumno : TgcExample
    {
        /////VARIABLES GLOBALES/////
        private TgcScene tgcEscena;

        private LuzLinterna luzLinterna;

        private CamaraFPS camaraFPS;

        private Caja cajaInteraccion;

        private Palanca palanca;

        private Bateria bateria;


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
            return "Juego de Terror - Juego de terror en primera persona basado en juegos famosos como Amnesia, Outlas, Penumbra, etc";
        }


        public override void init()
        {
            Device d3dDevice = GuiController.Instance.D3dDevice;

            TgcSceneLoader loader = new TgcSceneLoader();
            tgcEscena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\habitacionMiedo\\habitacionMiedo-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\habitacionMiedo\\");

            camaraFPS = new CamaraFPS(new Vector3(280f, 25f, 95f), new Vector3(180f, 25f, 95f));
            camaraFPS.init();

            luzLinterna = new LuzLinterna(tgcEscena, camaraFPS.camaraFramework);
            luzLinterna.init();

            cajaInteraccion = new Caja();
            cajaInteraccion.init();

            palanca = new Palanca();
            palanca.init();

            bateria = new Bateria();
            bateria.init(3);

        }


        public override void render(float elapsedTime)
        {
            TgcD3dInput d3dInput = GuiController.Instance.D3dInput;
            Device d3dDevice = GuiController.Instance.D3dDevice;

            camaraFPS.render();
            luzLinterna.render();

            cajaInteraccion.render(camaraFPS);
            palanca.render();
            bateria.render();

            //////////////////MUESTRO LOS OBJETOS//////////////////
            //tgcEscena.renderAll();

        }


        public override void close()
        {
            tgcEscena.disposeAll();
        }

    }
}
