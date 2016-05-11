using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Mapa
    {
        public TgcScene escena { get; set; }

        public Mapa()
        {
            TgcSceneLoader loader = new TgcSceneLoader();
            escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\mapaScene-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\");
        }

        public void detectarColisiones(List<Colisionador> colisionadores)
        {
            foreach (Colisionador colisionador in colisionadores)
            {
                if (colisiona(colisionador))
                {
                    colisionador.colisionar();
                }
            }

            }

        internal void dispose()
        {
            escena.disposeAll();
        }

        private bool colisiona(Colisionador colisionador)
        {
            foreach (TgcMesh mesh in escena.Meshes)
            {
                TgcBoundingBox obstaculo = mesh.BoundingBox;

                TgcCollisionUtils.BoxBoxResult result = TgcCollisionUtils.classifyBoxBox(colisionador.getBoundingBox(), obstaculo);
                if (result != TgcCollisionUtils.BoxBoxResult.Afuera)//result == TgcCollisionUtils.BoxBoxResult.Adentro || result == TgcCollisionUtils.BoxBoxResult.Atravesando)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
