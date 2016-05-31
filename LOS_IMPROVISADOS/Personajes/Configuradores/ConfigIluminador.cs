﻿using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.faroles;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.fluors;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.general;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas;
using System.Collections.Generic;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Configuradores
{
    class ConfigIluminador
    {
        public TgcScene escena { get; set; }
        public CamaraFPS camaraFPS { get; set; }

        public List<Iluminador> iluminadores { get; set; }
        public int posicionIluminadorActual { get; set; }

        public ConfigIluminador(TgcScene escena, CamaraFPS camaraFPS)
        {
            this.escena = escena;
            this.camaraFPS = camaraFPS;

            this.posicionIluminadorActual = 0;

            Iluminador linterna = new Iluminador(new LuzLinterna(escena, camaraFPS), new ManoLinterna(), new BateriaLinterna());
            Iluminador farol = new Iluminador(new LuzFarol(escena, camaraFPS), new ManoFarol(), new BateriaFarol());
            Iluminador fluor = new Iluminador(new LuzFluor(escena, camaraFPS), new ManoFluor(), new BateriaFluor());

            iluminadores = new List<Iluminador>() { linterna, farol, fluor };
        }

        public void renderizarIluminador()
        {
            //esto es para que te cambie el fluor si no tiene carga
            if (posicionIluminadorActual == 2 && !iluminadores[2].bateria.tenesBateria())
                cambiarASiguienteIluminador();

            iluminadores[posicionIluminadorActual].render();
            //iluminadores[2].bateria.gastarBateria();//hago que el fluor se gaste aunque no la este usando
        }

        public void cambiarASiguienteIluminador()
        {
            //esto es para que no pueda cambiar si esta usando el fluor
            if (posicionIluminadorActual == 2 && iluminadores[2].bateria.tenesBateria()) return;

            posicionIluminadorActual++;

            if (posicionIluminadorActual >= 2)
                posicionIluminadorActual = 0;
        }

        public void cambiarAIluminadorFluor()
        {
            if (posicionIluminadorActual == 2) return;

            iluminadores[2].bateria.recargar();
            posicionIluminadorActual = 2;
        }

        public void recargarBateriaLinterna()
        {
            if (posicionIluminadorActual == 0)
                iluminadores[posicionIluminadorActual].bateria.recargar();
        }

        public bool iluminadorActualSeQuedoSinBateria()
        {
            return !iluminadores[posicionIluminadorActual].bateria.tenesBateria();
        }
    }
}