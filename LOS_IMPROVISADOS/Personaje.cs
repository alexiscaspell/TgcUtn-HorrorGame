using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.faroles;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.fluors;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas;
using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Personaje
    {
        public TgcScene tgcEscena { set; get; }

        public CamaraFPS camaraFPS { get; set; }

        public List<Iluminador> iluminadores { get; set; }

        public int posicionIluminadorActual { get; set; }

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

    }
}
