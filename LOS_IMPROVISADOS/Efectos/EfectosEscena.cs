using AlumnoEjemplos.LOS_IMPROVISADOS.Efectos;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class EfectosEscena
    {
        public TgcScene tgcEscena { set; get; }

        public CamaraFramework camaraFramework { get; set; }

        public List<IEfecto> efectos { get; set; }

        public int efectoActual { get; set; }

        public EfectosEscena(TgcScene tgcEscena, CamaraFramework camaraFramework)
        {
            this.tgcEscena = tgcEscena;
            this.camaraFramework = camaraFramework;
            this.efectoActual = 0;

            efectos = new List<IEfecto>();
                efectos.Add(new LuzLinterna(tgcEscena, camaraFramework));
                efectos.Add(new LuzVela(tgcEscena, camaraFramework));
        }
        
        public void iniciarEfectos()
        {
            foreach (var efecto in efectos)
            {
                efecto.init();
            }
        }

        public void renderizarEfecto()
        {
            efectos[efectoActual].render();
        }

        public void renderizarEfectoNormal()
        {
            Effect currentShader = GuiController.Instance.Shaders.TgcMeshShader;
            
            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                mesh.Effect = currentShader;
                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
            }
            
            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                mesh.render();
            }
        }

        public void cambiarASiguienteEfecto()
        {
            efectoActual++;

            if (efectoActual >= efectos.Count)
                efectoActual = 0;

            //this.renderizarEfecto(numeroEfectoSiguiente);
        }

    }
}
