﻿using System.Collections.Generic;
using TgcViewer;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Sound;
using Microsoft.DirectX;
using AlumnoEjemplos.LOS_IMPROVISADOS.EfectosPosProcesado;
using AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Configuradores;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Personaje
    {
        private TgcBoundingSphere cuerpo;
        
        public Mapa mapa;

        public CamaraFPS camaraFPS { get; set; }

        public ConfigIluminador configIluminador { get; set; }

        public List<APosProcesado> posProcesados { get; set; }
        
        public List<Agarrable> objetos {get; set;}

        private Vector3 posMemento;

        private float slideFactor = 5;//Factor de slide hardcodeado

        private float radius = 30;//Radio de esfera hardcodeado
        
        private TgcStaticSound sonidoPasos;
        
        private float alturaAgachado;
        private float alturaParado;

        public Personaje(Mapa mapa)
        {
            this.mapa = mapa;
            this.camaraFPS = CamaraFPS.Instance;

            alturaParado = camaraFPS.camaraFramework.Position.Y;

            alturaAgachado = alturaParado / 3;

            cuerpo = new TgcBoundingSphere(camaraFPS.camaraFramework.Position, radius);

            configIluminador = new ConfigIluminador(mapa.escena, camaraFPS);

            iniciarPosProcesadores();
            
            sonidoPasos = new TgcStaticSound();
            sonidoPasos.loadSound(GuiController.Instance.AlumnoEjemplosDir +
                                  "Media\\Sonidos\\pasos.wav", 0);
            
        }

        /***********************POSPROCESADO***********************/
        public void iniciarPosProcesadores()
        {
            PosProcesadoAlarma posProcesadoAlarma = new PosProcesadoAlarma(mapa.escena);
            PosProcesoHechoMierda efectoHechoMierda = new PosProcesoHechoMierda(mapa.escena);
            
            efectoHechoMierda.init();

            posProcesados = new List<APosProcesado>() { posProcesadoAlarma };
            posProcesados.Add(efectoHechoMierda);
        }

        public void renderizarPosProcesado(float elapsedTime)
        {
            //por ahora lo hago solo con el primero, despues veo como implemento los demas
            //posProcesados[0].render(elapsedTime);
            posProcesados[1].render(elapsedTime);
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

        public void update()
        {
            float elapsedTime = GuiController.Instance.ElapsedTime;

            Vector3 posActual = camaraFPS.camaraFramework.Position;
            //updateMemento();
            posMemento = posActual;

            if (GuiController.Instance.D3dInput.buttonPressed(TgcViewer.Utils.Input.TgcD3dInput.MouseButtons.BUTTON_LEFT))
            {
                configIluminador.apagarOPrenderIlumniador();
            }

            if (GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.LeftShift))
            {
                camaraFPS.camaraFramework.setPosition(new Vector3(posActual.X,alturaAgachado, posActual.Z));
            }

            if (GuiController.Instance.D3dInput.keyUp(Microsoft.DirectX.DirectInput.Key.LeftShift))
            {
                camaraFPS.camaraFramework.setPosition(new Vector3(posActual.X,alturaParado, posActual.Z));
            }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.R))
            {
                configIluminador.recargarBateriaLinterna();
            }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.F))
            {
                configIluminador.cambiarASiguienteIluminador();
            }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.Q))
            {
                configIluminador.cambiarAIluminadorFluor();
            }
            
            //Checkeo para movimiento de sonido
            if (GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.W))
            {
            	sonidoPasos.play(true);
            }
			if (GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.A))
            {
            	sonidoPasos.play(true);
            }
			if (GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.S))
            {
            	sonidoPasos.play(true);
            }
			if (GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.D))
            {
            	sonidoPasos.play(true);
            }
			
			//Si no esta ninguna direccion apretada, paro el sonido
			if( !GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.W) &&
			    !GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.A) &&
			    !GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.S) &&
			    !GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.D) )
			{
				sonidoPasos.stop();
			}
            
			//Activar objetos
			if(GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.E))
			{
			   	foreach(Agarrable a in objetos)
			   	{
			   		a.acciona(this.camaraFPS.camaraFramework.Position, this.camaraFPS.camaraFramework.viewDir,a.getBB());
			   	}
			}
			
            //efecto de que se esta muriendo
            if (configIluminador.iluminadorActualSeQuedoSinBateria())
            {
                renderizarPosProcesado(elapsedTime);
            }

            configIluminador.renderizarIluminador();

            cuerpo.setCenter(posActual);
        }

        public void calcularColisiones()
        {
            TgcBoundingBox obstaculo = new TgcBoundingBox();

            Vector3 posActual = camaraFPS.camaraFramework.Position;

            cuerpo.setCenter(posActual);

            if (mapa.colisionaEsfera(cuerpo, ref obstaculo))
            {
                Vector3 slide = obtenerVectorSlide(obstaculo);

                Vector3 desplazamiento = camaraFPS.camaraFramework.Position - posMemento;

                desplazamiento.Normalize();

                Vector3 movement = slideFactor * Vector3.Dot(desplazamiento, slide) * slide;

                cuerpo.setCenter(posMemento + movement);

                if (mapa.colisionaEsfera(cuerpo, ref obstaculo))
                {
                    movement = new Vector3(0, 0, 0);
                }

                camaraFPS.camaraFramework.setPosition(posMemento + movement);
            }
        }

        private Vector3 obtenerVectorSlide(TgcBoundingBox box)
        {
            Vector3 posActual = camaraFPS.camaraFramework.Position;

            Vector3 closestPoint = TgcCollisionUtils.closestPointAABB(posActual, box);

            if (closestPoint.X==box.PMax.X||closestPoint.X==box.PMin.X)
            {
                return new Vector3(0, 0, 1);
            }

            return new Vector3(1, 0, 0);
        }


        }
    }
