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

namespace AlumnoEjemplos.MiGrupo
{
    public class EjemploAlumno : TgcExample
    {
        private CamaraFPS camaraFPS;

        private Personaje personaje;

        private Boss boss;

        private Mapa mapa; 
   
        //private MenuJuego menuPrincipal = new MenuJuego();
        
        private Puerta puerta;
        
        private TgcStaticSound sonidoFondo;

        private AnimatedBoss bossAnimado;
        private PuertaHard puertaHard;

        public bool playing;
        public GameMenu menuActual;

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

            mapa = new Mapa();

            camaraFPS = CamaraFPS.Instance;

            personaje = new Personaje(mapa);

            //boss = new Boss(camaraFPS);
            //boss.init(40f, new Vector3(100, 10, 100));

            Cursor.Hide();

            bossAnimado = new AnimatedBoss();
            bossAnimado.init(300f, new Vector3(camaraFPS.posicion.X,0,camaraFPS.posicion.Z+200));//Esto es para probar a un boss con esqueleto

            //puerta = new Puerta(630, 32, 200);

            puertaHard = new PuertaHard(new Vector3(630, 32, 200));//Esta es una puerta medio hardcodeada
            sonidoFondo = new TgcStaticSound();
            sonidoFondo.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\asd16.wav");
            sonidoFondo.play(true);

            FactoryMenu factoryMenu = FactoryMenu.Instance;
            factoryMenu.setApplication(this);

            menuActual = factoryMenu.menuPrincipal();

            playing = false;

            //menuPrincipal.init();

        }

        public override void render(float elapsedTime)
        {
            TgcD3dInput d3dInput = GuiController.Instance.D3dInput;
            Device d3dDevice = GuiController.Instance.D3dDevice;

            Size screenSize = GuiController.Instance.Panel3d.Size;
            Cursor.Position = new Point(screenSize.Width / 2, screenSize.Height / 2);

            if (!playing)
            {
                menuActual.render();
            }

            else { 
            //boss.setColisiona(personaje.estasMirandoBoss(boss));//El mejor truco del mundo! (seteo q el boss colisione solo si estoy mirando)
           
            //mapa.detectarColisiones(colisionadores);

            personaje.calcularColisiones();

            camaraFPS.render();

            personaje.update(elapsedTime);

            //boss.update(elapsedTime);
            //boss.render();

            bossAnimado.update();
            bossAnimado.render();

            //puerta.update(elapsedTime);
            //puerta.render();

            puertaHard.update();
            puertaHard.render();
            
            //menuPrincipal.render();
            }
        }

        public override void close()
        {
            mapa.dispose();

            //boss.dispose();
            bossAnimado.dispose();
        }
        
    }
}
