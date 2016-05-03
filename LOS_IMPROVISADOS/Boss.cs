using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Boss
    {
        private TgcScene cuerpo;
        private const float MOVEMENT_SPEED = 20f;

        public Boss()
        {
            TgcSceneLoader loader = new TgcSceneLoader();

            cuerpo = loader.loadSceneFromFile(
        GuiController.Instance.AlumnoEjemplosDir + "Media\\boss\\BOSS-TgcScene.xml",
        GuiController.Instance.AlumnoEjemplosDir + "Media\\boss\\");

            foreach (TgcMesh mesh in cuerpo.Meshes)
            {
                mesh.AutoUpdateBoundingBox = true;
            }
        }

        public void render()
        {
            cuerpo.renderAll();
        }

        public void update(CamaraFPS camara,float elapsedTime,Caja caja)//LA CAJA LA PASO TEMPORALMENTE SOLO PARA PROBAR COLISIONES
        {
           Vector3 movement = camara.posicion;
            Vector3 aux = camara.posicion;

            if (caja.estaCerca(cuerpo.BoundingBox.Position)) { }//ESTO NO FUNCIONA
            else
            {
                foreach (TgcMesh mesh in cuerpo.Meshes)
                {
                    movement.Subtract(mesh.BoundingBox.Position);
                    movement.Subtract(new Vector3(0, movement.Y, 0));
                    movement.Normalize();
                    movement *= MOVEMENT_SPEED * elapsedTime;
                    mesh.move(movement);
                    movement = aux;
                }

                /*movement.Subtract(cuerpo.BoundingBox.Position);//ESTO TAMPOCO FUNCIONA
                movement.Subtract(new Vector3(0, movement.Y, 0));
                movement.Normalize();
                movement *= MOVEMENT_SPEED * elapsedTime;
                cuerpo.BoundingBox.Position.Add(movement);*/
            }

        }


    }
}
