using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.faroles;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.fluors;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas;
using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using System;
using TgcViewer.Utils.TgcGeometry;
using Microsoft.DirectX;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Personaje:Colisionador
    {
        public TgcScene tgcEscena { set; get; }

        public CamaraFPS camaraFPS { get; set; }

        public List<Iluminador> iluminadores { get; set; }

        public int posicionIluminadorActual { get; set; }

        private TgcBox cuerpo;

        public Personaje(TgcScene tgcEscena, CamaraFPS camaraFPS)
        {
            this.tgcEscena = tgcEscena;
            this.camaraFPS = camaraFPS;
            this.posicionIluminadorActual = 0;

            Iluminador linterna = new Iluminador(new LuzLinterna(tgcEscena, camaraFPS), new PantallaLinterna(), new Logica());
            Iluminador farol = new Iluminador(new LuzFarol(tgcEscena, camaraFPS), new PantallaFarol(), new Logica());
            Iluminador fluor = new Iluminador(new LuzFluor(tgcEscena, camaraFPS), new PantallaFluor(), new Logica());

            iluminadores = new List<Iluminador>() {linterna, farol, fluor};
        }
        
        public void iniciarIluminadores()
        {
            foreach (var iluminador in iluminadores)
            {
                iluminador.init();
            }

            cuerpo = TgcBox.fromSize(new Vector3(4, camaraFPS.posicion.Y + 2, 14));

            cuerpo.Position = camaraFPS.camaraFramework.LookAt;
        }

        public void renderizarIluminador()
        {
            iluminadores[posicionIluminadorActual].render();
        }

        public void renderizarIluminador(int posicionIluminador)
        {
            if (posicionIluminador >= iluminadores.Count || posicionIluminador < 0)
                return;

            posicionIluminadorActual = posicionIluminador;
            iluminadores[posicionIluminadorActual].render();
        }
        
        public void cambiarASiguienteIluminador()
        {
            posicionIluminadorActual++;

            if (posicionIluminadorActual >= iluminadores.Count)
                posicionIluminadorActual = 0;
            
        }

        public void update()
        {
            updateMemento();

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.E))
            {
                cambiarASiguienteIluminador();
            }

                cuerpo.Position = camaraFPS.camaraFramework.LookAt;
        }

        internal bool estasMirandoBoss()
        {
            return true;
        }

        public override void retroceder(Vector3 vecRetroceso)
        {
            camaraFPS.camaraFramework.setPosition(camaraFPS.camaraFramework.Position - vecRetroceso);
            //camaraFPS.camaraFramework.updateCamera();
            //camaraFPS.update();
            cuerpo.Position = camaraFPS.camaraFramework.LookAt;
        }

        public override Vector3 getPosition()
        {
            return camaraFPS.camaraFramework.Position;
        }

        public override TgcBoundingBox getBoundingBox()
        {
            return cuerpo.BoundingBox;
        }
    }
}
