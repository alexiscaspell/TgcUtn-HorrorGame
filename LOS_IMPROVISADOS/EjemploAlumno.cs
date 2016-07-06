using TgcViewer.Example;
using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.Input;
using Microsoft.DirectX.Direct3D;
using AlumnoEjemplos.LOS_IMPROVISADOS;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using TgcViewer.Utils.Sound;
using TgcViewer.Utils;

namespace AlumnoEjemplos.MiGrupo
{
    public class EjemploAlumno : TgcExample
    {
        private CamaraFPS camaraFPS;

        private Personaje personaje;

        private Mapa mapa; 
        
        private TgcStaticSound sonidoFondo;

        private AnimatedBoss bossAnimado;
        
        public bool playing;
        public GameMenu menuActual;

        DiosMapa diosMapa;
        private FactoryMenu factoryMenu;
        private GameMenu menuPausa;

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
            return "Escape from Hospital - Juego de terror en primera persona basado en juegos famosos como Amnesia, Outlast, Penumbra, etc";
        }


        public override void init()
        {
            Device d3dDevice = GuiController.Instance.D3dDevice;

            mapa = Mapa.Instance;
            mapa.init();

            camaraFPS = CamaraFPS.Instance;

            personaje = Personaje.Instance;

            Cursor.Hide();

            diosMapa = DiosMapa.Instance;//ESTO DEJARLO ANTES DE LA INSTANCIACION DEL BOSS!!!
            diosMapa.init(0.009f);//Quiero que mapee 100x100 ptos del mapa
            diosMapa.generarMatriz();//Genera matriz de vias del boss
            diosMapa.generarCaminos();

            bossAnimado = AnimatedBoss.Instance;
            bossAnimado.init(300f, new Vector3(1062, 0, 3020));

            sonidoFondo = new TgcStaticSound();
            sonidoFondo.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\asd16.wav");
            sonidoFondo.play(true);

            factoryMenu = FactoryMenu.Instance;
            factoryMenu.setApplication(this);

            menuActual = factoryMenu.menuPrincipal();

            menuPausa = factoryMenu.menuPausa();

            playing = false;
            
        }

        public override void render(float elapsedTime)
        {
            TgcD3dInput d3dInput = GuiController.Instance.D3dInput;
            Device d3dDevice = GuiController.Instance.D3dDevice;

            Size screenSize = ScreenSizeClass.ScreenSize;
            Cursor.Position = new Point(screenSize.Width / 2, screenSize.Height / 2);

            if (!playing)
            {
                menuActual.render();
            }

            else {

                if (d3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.P))//Menu de pausa, tiene error de que podes seguir caminando
                {
                    playing = false;
                    menuActual = menuPausa;
                    return;
                }

             d3dDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);

             //d3dDevice.Transform.Projection =  Matrix.PerspectiveFovLH(Geometry.DegreeToRadian(45.0f),
               //                                TgcD3dDevice.aspectRatio, TgcD3dDevice.zNearPlaneDistance, 5000);

            personaje.calcularColisiones();

            camaraFPS.render();

            //bossAnimado.update();
            //bossAnimado.render();

            personaje.update();

            //personaje.configPosProcesado.renderizarPosProcesado(elapsedTime, 2);
            
            GameOver.Instance.render(elapsedTime);
            
            }

            GuiController.Instance.Text3d.drawText("FPS: " + HighResolutionTimer.Instance.FramesPerSecond, 0, 0, Color.Yellow);
        }

        public override void close()
        {
            mapa.dispose();

            bossAnimado.dispose();
        }
        
    }
}
