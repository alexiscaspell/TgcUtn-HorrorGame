using AlumnoEjemplos.LOS_IMPROVISADOS.EfectosPosProcesado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Configuradores
{
    class ConfigPosProcesados
    {
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

            posProcesados = new List<APosProcesado>() { posProcesadoAlarma , efectoHechoMierda };
        }

        public void renderizarPosProcesado(float elapsedTime)
        {
            posProcesados[0].render(elapsedTime);
            //posProcesados[1].render(elapsedTime);
        }
    }
}
