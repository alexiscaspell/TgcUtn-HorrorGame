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
        public TgcScene tgcEscena { set; get; }
        public CamaraFPS camaraFPS { get; set; }

        public List<Iluminador> iluminadores { get; set; }
        public int posicionIluminadorActual { get; set; }

        public List<APosProcesado> posProcesados { get; set; }

        private TgcBox cuerpo;

        public Personaje(TgcScene tgcEscena, CamaraFPS camaraFPS)
        {
            this.tgcEscena = tgcEscena;
            this.camaraFPS = camaraFPS;
            this.posicionIluminadorActual = 0;

            cuerpo = TgcBox.fromSize(new Vector3(10, camaraFPS.posicion.Y + 2, 14));

            cuerpo.Position = camaraFPS.camaraFramework.LookAt;

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
            camaraFPS.camaraFramework.setPosition(camaraFPS.camaraFramework.Position - vecRetroceso);
            cuerpo.Position = camaraFPS.camaraFramework.LookAt;
        }

        public void update()
        {
            updateMemento();

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.R))
            {
                recargarBateriaLinterna();
            }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.F))
            {
                cambiarASiguienteIluminador();
            }

            cuerpo.Position = camaraFPS.camaraFramework.LookAt;

            //efecto de que se esta muriendo
            if (!iluminadores[posicionIluminadorActual].bateria.tenesBateria())
            {
                renderizarPosProcesado();
            }

            renderizarIluminador();
        }

        public override Vector3 getPosition()
        {
            return camaraFPS.camaraFramework.Position;
        }

        public override TgcBoundingBox getBoundingBox()
        {
            return cuerpo.BoundingBox;
        }
    }
}
