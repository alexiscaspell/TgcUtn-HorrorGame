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
        public List<TgcMesh> meshes { get; set; }

        public List<APosProcesado> posProcesados { get; set; }

        public ConfigPosProcesados(List<TgcMesh> meshes)
        {
            this.meshes = meshes;
            iniciarPosProcesadores();
        }

        public void iniciarPosProcesadores()
        {
            PosProcesadoAlarma posProcesadoAlarma = new PosProcesadoAlarma(meshes);
            PosProcesoHechoMierda efectoHechoMierda = new PosProcesoHechoMierda(meshes);

            efectoHechoMierda.init();

            posProcesados = new List<APosProcesado>() { posProcesadoAlarma };
            posProcesados.Add(efectoHechoMierda);
        }

        public void renderizarPosProcesado(float elapsedTime)
        {
            //posProcesados[0].render(elapsedTime);
            posProcesados[1].render(elapsedTime);
        }
    }
}
