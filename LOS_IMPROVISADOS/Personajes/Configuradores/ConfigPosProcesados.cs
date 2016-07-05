using AlumnoEjemplos.LOS_IMPROVISADOS.EfectosPosProcesado;
using System.Collections.Generic;
using System;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Configuradores
{
    class ConfigPosProcesados
    {
        private EfectoEscondido escondidoLight;

        public Mapa mapa { get; set; }

        public List<APosProcesado> posProcesados { get; set; }

        public ConfigPosProcesados(Mapa mapa)
        {
            this.mapa = mapa;
            iniciarPosProcesadores();
        }

        public void iniciarPosProcesadores()
        {
            PosProcesadoAlarma posProcesadoAlarma = new PosProcesadoAlarma(mapa);
            PosProcesoHechoMierda efectoHechoMierda = new PosProcesoHechoMierda(mapa);
            PosProcesadoBur posProcesadoBur = new PosProcesadoBur(mapa);
            escondidoLight = new EfectoEscondido(mapa, CamaraFPS.Instance);
            escondidoLight.init();

            posProcesados = new List<APosProcesado>() { posProcesadoAlarma , efectoHechoMierda , posProcesadoBur};
        }

        public void renderizarPosProcesado(float elapsedTime)
        {
            posProcesados[0].render(elapsedTime);
            //posProcesados[1].render(elapsedTime);
        }

        public void renderizarPosProcesado(float elapsedTime, int posicionPosProcesado)
        {
            if (posicionPosProcesado >= posProcesados.Count || posicionPosProcesado < 0) return;
            posProcesados[posicionPosProcesado].render(elapsedTime);
        }

        internal bool renderizarEfectoEscondido()
        {
            escondidoLight.render();
            return escondidoLight.terminoEfecto;
        }
    }
}
