using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.faroles;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.fluors;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas;
using System.Collections.Generic;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcGeometry;
using Microsoft.DirectX;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.general;
using AlumnoEjemplos.LOS_IMPROVISADOS.EfectosPosProcesado;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Personaje : Colisionador
    {
        private TgcBoundingSphere cuerpo;

        public TgcScene tgcEscena { set; get; }
        public Mapa mapa;
        public CamaraFPS camaraFPS { get; set; }

        public List<Iluminador> iluminadores { get; set; }
        public int posicionIluminadorActual { get; set; }

        public List<APosProcesado> posProcesados { get; set; }

        //private TgcBox cuerpo;

        public Personaje(Mapa mapa, CamaraFPS camaraFPS)
        {
            this.mapa = mapa;
            this.tgcEscena = mapa.escena;
            this.camaraFPS = camaraFPS;
            this.posicionIluminadorActual = 0;

            //cuerpo = TgcBox.fromSize(new Vector3(10, camaraFPS.posicion.Y + 2, 14));

            //cuerpo.Position = camaraFPS.camaraFramework.LookAt;

            cuerpo = new TgcBoundingSphere(camaraFPS.camaraFramework.Position, 9);

            iniciarIluminadores();

            iniciarPosProcesadores();
        }

        /***********************ILUMINADOR/***********************/
        public void iniciarIluminadores()
        {
            Iluminador linterna = new Iluminador(new LuzLinterna(tgcEscena, camaraFPS), new ManoLinterna(), new BateriaLinterna());
            Iluminador farol = new Iluminador(new LuzFarol(tgcEscena, camaraFPS), new ManoFarol(), new BateriaFarol());
            Iluminador fluor = new Iluminador(new LuzFluor(tgcEscena, camaraFPS), new ManoFluor(), new BateriaFluor());

            iluminadores = new List<Iluminador>() { linterna, farol, fluor };
        }

        public void renderizarIluminador()
        {
            iluminadores[posicionIluminadorActual].render();

            iluminadores[2].bateria.gastarBateria(10);//hago que el fluor se gaste aunque no la este usando
        }

        public void renderizarIluminador(int posicionIluminador)
        {
            if (posicionIluminador >= iluminadores.Count || posicionIluminador < 0)
                return;

            posicionIluminadorActual = posicionIluminador;
            iluminadores[posicionIluminadorActual].render();
        }

        public void cambiarASiguienteIluminador()
        {
            posicionIluminadorActual++;

            if (posicionIluminadorActual >= iluminadores.Count)
                posicionIluminadorActual = 0;
        }

        /***********************POSPROCESADO***********************/
        public void iniciarPosProcesadores()
        {
            PosProcesadoAlarma posProcesadoAlarma = new PosProcesadoAlarma(tgcEscena);

            posProcesados = new List<APosProcesado>() { posProcesadoAlarma };
        }

        public void renderizarPosProcesado()
        {
            //por ahora lo hago solo con el primero, despues veo como implemento los demas
            posProcesados[0].render();
        }


        internal bool estasMirandoBoss(Boss boss)
        {
            Vector3 direccionBoss = cuerpo.Position - boss.getPosition();

            TgcRay rayoBoss = new TgcRay(boss.getPosition(), direccionBoss);
            Plane farPlane = GuiController.Instance.Frustum.FarPlane;
            float t;//= GuiController.Instance.ElapsedTime;
            Vector3 ptoColision;
            return !TgcCollisionUtils.intersectRayPlane(rayoBoss, farPlane, out t, out ptoColision);
        }

        public void recargarBateriaLinterna()
        {
            iluminadores[posicionIluminadorActual].bateria.recargar();
        }

        public override void retroceder(Vector3 vecRetroceso)
        {
            //camaraFPS.camaraFramework.setPosition(camaraFPS.camaraFramework.Position - vecRetroceso);
            //cuerpo.Position = camaraFPS.camaraFramework.LookAt;
        }

        Vector3 posMemento;

        public void update()
        {
            //updateMemento();
            posMemento = camaraFPS.camaraFramework.Position;

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.R))
            {
                recargarBateriaLinterna();
            }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.F))
            {
                cambiarASiguienteIluminador();
            }

            //cuerpo.Position = camaraFPS.camaraFramework.LookAt;

            //efecto de que se esta muriendo
            if (!iluminadores[posicionIluminadorActual].bateria.tenesBateria())
            {
                renderizarPosProcesado();
            }

            renderizarIluminador();

            cuerpo.setCenter(camaraFPS.camaraFramework.Position);
        }

        public override Vector3 getPosition()
        {
            return camaraFPS.camaraFramework.Position;
        }

        public override TgcBoundingBox getBoundingBox()
        {
            return new TgcBoundingBox();//cuerpo.BoundingBox;
        }

        public void calcularColisiones()
        {
            TgcBoundingBox obstaculo = new TgcBoundingBox();

            Vector3 posActual = camaraFPS.camaraFramework.Position;

            cuerpo.setCenter(posActual);

            if (mapa.colisionaEsfera(cuerpo, obstaculo))
            {
                Vector3 slide = obtenerVectorSlide(obstaculo);//new Vector3(1, 0, 0);

                Vector3 desplazamiento = camaraFPS.camaraFramework.Position - posMemento;

                Vector3 movement = Vector3.Dot(desplazamiento, slide) * slide;

                camaraFPS.camaraFramework.setPosition(posMemento + movement);
            }
        }

        private Vector3 obtenerVectorSlide(TgcBoundingBox box)
        {
            Vector3 posActual = camaraFPS.camaraFramework.Position;

            Vector3 desplazamiento = posActual - posMemento;

            desplazamiento.Normalize();

            TgcRay rayoX = new TgcRay(posActual, new Vector3(desplazamiento.X, 0, 0));

           Vector3 posColision;

            if (TgcCollisionUtils.intersectRayAABB(rayoX, box, out posColision))
            {
                return new Vector3(0, 0, 1);
            }

            return new Vector3(1, 0, 0);
        }
            /*
                float radio = 9;

                Vector3 posActual = camaraFPS.camaraFramework.Position;

                TgcRay colisionRay = new TgcRay(posActual, desplazamiento);

                Vector3 ptoColision;

                if(!TgcCollisionUtils.intersectRayAABB(colisionRay, obstaculo, out ptoColision))
                {
                    desplazamiento = -desplazamiento;
                    colisionRay = new TgcRay(posActual, desplazamiento);
                    TgcCollisionUtils.intersectRayAABB(colisionRay, obstaculo, out ptoColision);
                }

                Vector3 movement = Vector3.Dot(desplazamiento, slide) * slide;

                desplazamiento.Normalize();

                Vector3 nuevaPos = posActual - desplazamiento * (radio - (ptoColision - posActual).LengthSq())+movement;*/

      public Vector3 closestPointAABB(Vector3 point,TgcBoundingBox box)
      {
            Vector3 closestPoint = new Vector3(0,0,0);

            for (int i = 0; i < 3; i++)
            {
                float v = getComponent(point,i);

                if (v < getComponent(box.PMin, i))
                {
                    v = getComponent(box.PMin, i);
                }
                if (v > getComponent(box.PMax,i))
                {
                    v = getComponent(box.PMax,i);
                }

                setComponent(closestPoint, i, v);
            }
            return closestPoint;
        }

        private float getComponent(Vector3 point,int i)
        {
            switch (i)
            {
                case 1: return point.X;
                case 2: return point.Y;;
                case 3: return point.Z;
            }

            return 0;
        }

        private void setComponent(Vector3 point, int i,float value)
        {
            switch (i)
            {
                case 1:
                    point = new Vector3(value, point.Y, point.Z);
                    break;
                case 2:
                    point = new Vector3(point.X, value, point.Z);
                    break;
                case 3:
                    point = new Vector3(point.X, point.Y, value);
                    break;
            }
        }

        }
    }

