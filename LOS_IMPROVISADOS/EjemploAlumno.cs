using TgcViewer.Example;
using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Input;

using Microsoft.DirectX.Direct3D;
using AlumnoEjemplos.LOS_IMPROVISADOS;
using TgcViewer.Utils.TgcGeometry;
using System.Windows.Forms;
using System.Drawing;
using System;
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
        
        private List<Colisionador> colisionadores;
        
        private List<Punto> mapaPuntos;
        
        private Puerta puerta;
        
        private TgcStaticSound sonidoFondo;

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

            mapa = new Mapa();

            camaraFPS = new CamaraFPS(new Vector3(50,32,200/*280f, 25f, 60f*/), new Vector3(270f, 32f,60f));

            personaje = new Personaje(mapa, camaraFPS);

            boss = new Boss(camaraFPS);
            boss.init(40f,new Vector3(100,10,100));
            
            Cursor.Hide();
            colisionadores = new List<Colisionador>();
            colisionadores.Add(boss);
            colisionadores.Add(personaje);
            
            puerta = new Puerta(630,32,200);
            
            sonidoFondo = new TgcStaticSound();
            sonidoFondo.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\asd16.wav");
    	}


        public override void render(float elapsedTime)
        {
            TgcD3dInput d3dInput = GuiController.Instance.D3dInput;
            Device d3dDevice = GuiController.Instance.D3dDevice;

            Size screenSize = GuiController.Instance.Panel3d.Size;
            Cursor.Position = new Point(screenSize.Width / 2, screenSize.Height / 2);

            boss.setColisiona(personaje.estasMirandoBoss(boss));//El mejor truco del mundo! (seteo q el boss colisione solo si estoy mirando)

            mapa.detectarColisiones(colisionadores);

            personaje.calcularColisiones();

            camaraFPS.render();

            personaje.update();

            boss.update(elapsedTime);
            boss.render();
            
            puerta.update(elapsedTime);
            puerta.render();
       
            sonidoFondo.play(true);
        }

        public override void close()
        {
            mapa.dispose();

            boss.dispose();
        }
        


    }
}
