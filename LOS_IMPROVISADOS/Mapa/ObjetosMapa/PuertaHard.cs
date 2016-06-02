using System;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer;
using Microsoft.DirectX.Direct3D;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{

    public class PuertaHard
    {
        TgcMesh mesh;
        TgcScene escena;
        const float speed = 10;
        float anguloRotacion = 0;
        bool rotando = false;
        private bool abierta = false;

        public PuertaHard(Vector3 posicion)
        {

            TgcSceneLoader loader = new TgcSceneLoader();
            escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\puertaBerreta-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\");

            mesh = escena.Meshes[0];

            mesh.Position = posicion;

            mesh.Scale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        public void update()
        {
            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.E))
            {
                rotando = true;
            }
            if (rotando)
            {
                anguloRotacion += speed;

                if (anguloRotacion <= 90)
                {
                    if (abierta)
                    {
                        mesh.rotateY(Geometry.DegreeToRadian(-speed));
                    }
                    else
                    {
                        mesh.rotateY(Geometry.DegreeToRadian(speed));
                    }
                }
                else
                {
                    anguloRotacion = 0;
                    rotando = false;
                    abierta = !abierta;
                }
            }
        }

        public void render()
        {
            mesh.render();
        }

    }
}