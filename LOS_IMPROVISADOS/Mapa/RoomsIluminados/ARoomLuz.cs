using Microsoft.DirectX;
using System.Collections.Generic;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    abstract class ARoomLuz
    {
        public List<TgcMesh> meshesRoom;

        public Vector3 posicion { get; set; }

        public TgcScene escenaLampara { get; set; }

        public string idRoom { get; set; }

        public ARoomLuz()
        {
        }

        public void init()
        {
            string idPorReferencia = "";
            this.meshesRoom = Mapa.Instance.meshesDeCuartoEnLaPosicion(this.posicion, ref idPorReferencia);
            this.meshesRoom.AddRange(escenaLampara.Meshes);
            this.idRoom = idPorReferencia;

            foreach (TgcMesh mesh in meshesRoom)
            {
                mesh.Effect = GuiController.Instance.Shaders.TgcMeshSpotLightShader;
                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
            }
        }

        public void dispose()
        {
            escenaLampara.disposeAll();

            foreach (TgcMesh mesh in meshesRoom)
            {
                mesh.dispose();
            }
        }

        abstract public void render();
    }
}
