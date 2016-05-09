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
        public Vector3 dir = new Vector3(1,0,0);

        public Boss()
        {
            TgcSceneLoader loader = new TgcSceneLoader();

            cuerpo = loader.loadSceneFromFile(
        GuiController.Instance.AlumnoEjemplosDir + "Media\\Calabera\\Calabera-TgcScene.xml",
        GuiController.Instance.AlumnoEjemplosDir + "Media\\Calabera\\");

            foreach (TgcMesh mesh in cuerpo.Meshes)
            {
                mesh.AutoUpdateBoundingBox = true;
            }
        }

        public void render()
        {
            cuerpo.renderAll();
                cuerpo.BoundingBox.render();
        }
        

        public void update(CamaraFPS camara,float elapsedTime,Caja caja)//LA CAJA LA PASO TEMPORALMENTE SOLO PARA PROBAR COLISIONES
        {
           Vector3 movement = camara.posicion;
            Vector3 aux = camara.posicion;

            float angulo;
            bool collide = false;//ESTE BOOL ME DICE SI HAY COLISION

            movement.Subtract(cuerpo.BoundingBox.Position);//ACA MUEVO EL BOUNDING BOX SIMULANDO EL MOV DEL BOSS
            //movement.Subtract(new Vector3(0, movement.Y, 0));
            movement.Normalize();
            movement *= MOVEMENT_SPEED * elapsedTime;
            cuerpo.BoundingBox.move(movement);
            
            TgcCollisionUtils.BoxBoxResult result = TgcCollisionUtils.classifyBoxBox(cuerpo.BoundingBox, caja.getBoundingBox());
            if (result == TgcCollisionUtils.BoxBoxResult.Adentro || result == TgcCollisionUtils.BoxBoxResult.Atravesando)
            {
                collide = true;
                cuerpo.BoundingBox.move(-movement);//SI EL BOUNDING BOX CHOCA CON LA CAJA RETROCEDO EL BOUNDING BOX 
                                                   //Y NO DEJO AVANZAR AL BOSS
            }


            if (!collide)//SI NO HAY COLISION AVANZO
            {
                foreach (TgcMesh mesh in cuerpo.Meshes)
                {
            		movement = aux;
                    movement.Subtract(mesh.BoundingBox.Position);
                    movement.Subtract(new Vector3(0, movement.Y, 0));
                    movement.Normalize();
                    dir.Subtract(new Vector3(0, dir.Y,0));
                    
                    angulo = FastMath.Acos( Vector3.Dot(movement,dir) / (movement.Length() * dir.Length())  );
                    
                    dir = movement;
                              
                    if(!float.IsNaN(angulo)){
                 		mesh.rotateY(angulo);
                    }
                    
                    movement *= MOVEMENT_SPEED * elapsedTime;
                    mesh.move(movement);
                    movement = aux;
                }

            }

        }


    }
}
