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
    class Boss:Colisionador
    {
        private TgcScene sceneBicho;
        //private const float MOVEMENT_SPEED = 20f;
        private float velocidadMovimiento;
        private Vector3 dir = new Vector3(0, 0, -1);//1, 0, 0);
        private TgcMesh cuerpo;
        CamaraFPS camara;

        public Boss(CamaraFPS camara)
        {
            TgcSceneLoader loader = new TgcSceneLoader();

            sceneBicho = loader.loadSceneFromFile(
            GuiController.Instance.AlumnoEjemplosDir + "Media\\Calabera\\Calabera-TgcScene.xml",
            GuiController.Instance.AlumnoEjemplosDir + "Media\\Calabera\\");

            cuerpo = sceneBicho.Meshes[0];

            cuerpo.AutoTransformEnable = true;

            this.camara = camara;
        }

        public void init(float velocidadMovimiento,Vector3 posicion)
        {
            this.velocidadMovimiento = velocidadMovimiento;

            cuerpo.Position = posicion;
            /*dir = cuerpo.Position - cuerpo.BoundingBox.calculateBoxCenter();*/
        }

        public void render()
        {
            cuerpo.render();
        }

        private void seguirPersonaje(float elapsedTime)
        {
            Vector3 movement = camara.posicion;

            bool hayColision = false;//Esto voy a llamar a un metodo de clase de ColinaAzul

            if (!hayColision)
            {
                movement.Subtract(cuerpo.BoundingBox.Position);
                movement.Subtract(new Vector3(0, movement.Y, 0));
                movement.Normalize();
                dir.Subtract(new Vector3(0, dir.Y, 0));

                float angulo = FastMath.Acos(Vector3.Dot(movement, dir) / (movement.Length() * dir.Length()));

                Vector3 normal = Vector3.Cross(dir, movement);

                dir = movement;

                

                /*if (!float.IsNaN(angulo))
                {
                    cuerpo.rotateY((FastMath.Abs(normal.Y) / normal.Y) * angulo);
                }*/

                if (!float.IsNaN(angulo))
                {
                    if (normal.Y > 0)
                    {
                        cuerpo.rotateY(angulo);
                    }
                    else
                        cuerpo.rotateY(-angulo);
                }

                movement *= velocidadMovimiento * elapsedTime;
                cuerpo.move(movement);
            }
        }

        public void update(float elapsedTime)
        {
            updateMemento();

            seguirPersonaje(elapsedTime);//Por ahora solo tengo esto
        }

        public void dispose()
        {
            sceneBicho.disposeAll();
        }

        public override Vector3 getPosition()
        {
            return cuerpo.Position;
        }

        public override TgcBoundingBox getBoundingBox()
        {
            return cuerpo.BoundingBox;
        }

        public override void retroceder(Vector3 vecRetroceso)
        {
            cuerpo.move(-vecRetroceso);
        }

    }


}

/*
 * 
 * public Vector3 rotar_xz(Vector3 v, float an)
        {
            return new Vector3((float)(v.X * Math.Cos(an) - v.Z * Math.Sin(an)),
                                v.Y,
                                (float)(v.X * Math.Sin(an) + v.Z * Math.Cos(an)));

        }*/
