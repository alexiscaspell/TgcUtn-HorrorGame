using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores
{
    class Iluminador
    {
        public TgcScene tgcEscena { set; get; }

        public CamaraFPS camaraFPS { get; set; }

        public List<AEfecto> efectos { get; set; }

        public int efectoActual { get; set; }

        public Iluminador(TgcScene tgcEscena, CamaraFPS camaraFPS)
        {
            this.tgcEscena = tgcEscena;
            this.camaraFPS = camaraFPS;
            this.efectoActual = 0;

            efectos = new List<AEfecto>();
                efectos.Add(new LuzLinterna(tgcEscena, camaraFPS));
                efectos.Add(new LuzFarol(tgcEscena, camaraFPS));
                efectos.Add(new LuzFluorescente(tgcEscena, camaraFPS));
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
            
        }

    }
}
