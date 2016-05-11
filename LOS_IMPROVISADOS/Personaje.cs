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

            Iluminador linterna = new Iluminador(new LuzLinterna(tgcEscena, camaraFPS), new ManoLinterna(), new BateriaLinterna());
            Iluminador farol = new Iluminador(new LuzFarol(tgcEscena, camaraFPS), new ManoFarol(), new BateriaFarol());
            Iluminador fluor = new Iluminador(new LuzFluor(tgcEscena, camaraFPS), new ManoFluor(), new BateriaFluor());

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
