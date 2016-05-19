using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.faroles;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.fluors;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas;
using System.Collections.Generic;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcGeometry;
using Microsoft.DirectX;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.general;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;

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
            
            cuerpo = TgcBox.fromSize(new Vector3(10, camaraFPS.posicion.Y + 2, 14));

            cuerpo.Position = camaraFPS.camaraFramework.LookAt;

            iniciarIluminadores();
        }
        
        public void iniciarIluminadores()
        {
            Iluminador linterna = new Iluminador(new LuzLinterna(tgcEscena, camaraFPS), new ManoLinterna(), new BateriaLinterna());
            Iluminador farol = new Iluminador(new LuzFarol(tgcEscena, camaraFPS), new ManoFarol(), new BateriaFarol());
            Iluminador fluor = new Iluminador(new LuzFluor(tgcEscena, camaraFPS), new ManoFluor(), new BateriaFluor());

            iluminadores = new List<Iluminador>() { linterna, farol, fluor };
        }

        public void renderizarIluminador()
        {
            iluminadores[posicionIluminadorActual].render();

            //hago que el fluor se gaste aunque no la este usando
            iluminadores[2].bateria.gastarBateria(10);
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

        internal bool estasMirandoBoss()
        {
            return true;
        }

        public void recargarBateriaLinterna()
        {
            iluminadores[posicionIluminadorActual].bateria.recargar();
        }

        public override void retroceder(Vector3 vecRetroceso)
        {
            camaraFPS.camaraFramework.setPosition(camaraFPS.camaraFramework.Position - vecRetroceso);
            cuerpo.Position = camaraFPS.camaraFramework.LookAt;
        }

        public void update()
        {
            updateMemento();

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.R))
            {
                recargarBateriaLinterna();
            }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.F))
            {
                cambiarASiguienteIluminador();
            }

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
