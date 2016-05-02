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
using AlumnoEjemplos.LOS_IMPROVISADOS.Efectos;

namespace AlumnoEjemplos.MiGrupo
{
    public class EjemploAlumno : TgcExample
    {

        ////////VARIABLES GLOBALES////////
        private TgcScene tgcEscena;
        private CamaraFPS camaraFPS;

        private EfectosEscena efectoEscena;
       
        
        private Bateria bateriaLinterna;
        private Caja cajaInteraccion;
        private Palanca palanca;

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
                GuiController.Instance.AlumnoEjemplosDir + "Media\\habitacionMiedo\\habitacionMiedo-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\habitacionMiedo\\");
            

            camaraFPS = new CamaraFPS(new Vector3(280f, 25f, 95f), new Vector3(279f, 25f, 95f));
                camaraFPS.init();

            efectoEscena = new EfectosEscena(tgcEscena, camaraFPS);
                efectoEscena.iniciarEfectos();
            


            cajaInteraccion = new Caja();
            cajaInteraccion.init();

            palanca = new Palanca();
            palanca.init();

            bateriaLinterna = new Bateria();
            bateriaLinterna.init(3);
        }


        public override void render(float elapsedTime)
        {
            TgcD3dInput d3dInput = GuiController.Instance.D3dInput;
            Device d3dDevice = GuiController.Instance.D3dDevice;

            if (d3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.E))
            {
                efectoEscena.cambiarASiguienteEfecto();
            }

            camaraFPS.render();
            efectoEscena.renderizarEfecto();

            
            cajaInteraccion.render(camaraFPS);
            palanca.render();
            bateriaLinterna.render();

            //////////////////MUESTRO LOS OBJETOS//////////////////
            //tgcEscena.renderAll();
            
        }


        public override void close()
        {
            tgcEscena.disposeAll();
        }

    }
}
