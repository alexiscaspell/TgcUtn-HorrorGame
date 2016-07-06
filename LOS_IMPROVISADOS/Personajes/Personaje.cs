using System.Collections.Generic;
using TgcViewer;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Sound;
using Microsoft.DirectX;
using AlumnoEjemplos.LOS_IMPROVISADOS.EfectosPosProcesado;
using AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Configuradores;
using System;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Personaje
    {
        #region Singleton
        private static volatile Personaje instancia = null;

        public static Personaje Instance
        {
        	get{
        		if(instancia==null)instancia = new Personaje();
        		
        		return instancia;
        	}
        }

        #endregion
        
        private Ganaste creditosEnd;

        private TgcBoundingBox ganasteBox;
        public TgcBoundingSphere cuerpo;
        
        public Mapa mapa;

        public CamaraFPS camaraFPS { get; set; }

        public ConfigIluminador configIluminador { get; set; }   
        
        public ConfigPosProcesados configPosProcesado { get; set; }   

        //Para checkear si puede abrir la puerta o no
        public int llaveActual = 0; //llave 0 => no tiene ninguna llave.

        private Vector3 posMemento;

        enum state { PASEANDO, ASUSTADO, ESCONDIDO };

        state estado;

        //private float slideFactor = 5;//Factor de slide hardcodeado

        private float radius = 30;//Radio de esfera hardcodeado
        
        //private TgcStaticSound sonidoPasos;
        
        private float alturaAgachado;
        private float alturaParado;
        private bool muerto = false;
        private bool ganaste = false;

        //lobo
        private float tiempoParaDejarRastro = 0.012f;//0.025f;
        private float sumadorParaDejarRastro = 0;
        //private TgcStaticSound sonidoPieDerecho;
        //private TgcStaticSound sonidoPieIzquierdo;
        private TgcStaticSound sonidoPasos;
        private TgcStaticSound sonidoRespiracion;

        //private List<TgcStaticSound> sonidoPies;
        
        //timer para que no sea tan brusco el cambio de estado
        float timerRetrasoPostProcesadoYEscondido = 0;
        private TgcStaticSound sonidoLatidos;
        private TgcStaticSound sonidoRespiracionAgitada;
        
        private Personaje()
        {

            mapa = Mapa.Instance;

            camaraFPS = CamaraFPS.Instance;

            alturaParado = camaraFPS.camaraFramework.Position.Y;
            alturaAgachado = alturaParado / 3;

            cuerpo = new TgcBoundingSphere(camaraFPS.camaraFramework.Position, radius);

            //Endgame
            ganasteBox = new TgcBoundingBox(new Vector3(24000, 0, 8500), new Vector3(24100, 600, 8900));
            creditosEnd = new Ganaste();
            
            configIluminador = new ConfigIluminador(mapa, camaraFPS);
            configPosProcesado = new ConfigPosProcesados(mapa);

            //sonidoPieDerecho = new TgcStaticSound();
            //sonidoPieIzquierdo = new TgcStaticSound();

            sonidoPasos = new TgcStaticSound();
            sonidoPasos.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\pasos.wav", 0);

            sonidoRespiracion = new TgcStaticSound();
            sonidoRespiracion.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\respiracion.wav", 0);

            sonidoLatidos = new TgcStaticSound();
            sonidoLatidos.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\latidos.wav", 0);

            sonidoRespiracionAgitada = new TgcStaticSound();
            sonidoRespiracionAgitada.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\respiracionReverb.wav", 0);

            estado = state.PASEANDO;
        }

        internal bool estasMirandoBoss(AnimatedBoss boss)
        {

            /*Vector3 direccionBoss = cuerpo.Position - boss.getPosition();

            TgcRay rayoBoss = new TgcRay(boss.getPosition(), direccionBoss);
            Plane farPlane = GuiController.Instance.Frustum.FarPlane;
            float t;//= GuiController.Instance.ElapsedTime;
            Vector3 ptoColision;

            if(TgcCollisionUtils.intersectRayPlane(rayoBoss, farPlane, out t, out ptoColision))
            {
                return false;
            }*/

            TgcRay rayoBoss = new TgcRay(cuerpo.Position, camaraFPS.camaraFramework.viewDir);
            Vector3 vectorInutil;

            if (!TgcCollisionUtils.intersectRayAABB(rayoBoss,boss.getBoundingBox(),out vectorInutil))
            {
                return false;
            }

            string testoInutil = "";
            Vector3 posInicial = cuerpo.Position;
            Vector3 posFinal = boss.getPosition();

            foreach (TgcMesh mesh in Mapa.Instance.meshesDeCuartoEnLaPosicion(cuerpo.Position,ref testoInutil))
            {
                bool resultado = TgcCollisionUtils.intersectSegmentAABB(posInicial, posFinal, mesh.BoundingBox, out vectorInutil);
                if (resultado)
                {
                    return false;
                }
            }

            return true;
        }

        public void update()
        {
            float elapsedTime = GuiController.Instance.ElapsedTime;

            Vector3 posActual = camaraFPS.camaraFramework.Position;
            //updateMemento();
            posMemento = posActual;

            if (GuiController.Instance.D3dInput.buttonPressed(TgcViewer.Utils.Input.TgcD3dInput.MouseButtons.BUTTON_LEFT) && !muerto)
            {
                configIluminador.apagarOPrenderIlumniador();
            }

            if (GuiController.Instance.D3dInput.buttonPressed(TgcViewer.Utils.Input.TgcD3dInput.MouseButtons.BUTTON_RIGHT) && !muerto)
            {
                configIluminador.cambiarAIluminadorFluor();
            }

            if (GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.LeftShift) && !muerto)
            {
                camaraFPS.camaraFramework.setPosition(new Vector3(posActual.X,alturaAgachado, posActual.Z));
                camaraFPS.camaraFramework.MovementSpeed /= 2; 
            }

            if (GuiController.Instance.D3dInput.keyUp(Microsoft.DirectX.DirectInput.Key.LeftShift) && !muerto)
            {
                camaraFPS.camaraFramework.setPosition(new Vector3(posActual.X,alturaParado, posActual.Z));
                camaraFPS.camaraFramework.MovementSpeed *= 2;
            }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.F) && !muerto)
            {
                configIluminador.cambiarASiguienteIluminador();
            }
            /*
            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.R))
            {
                configIluminador.recargarBateriaLinterna();
            }*/

            //Checkeo para movimiento de sonido
            if (GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.W) && !muerto)
            {
                sonidoPasos.play(true);
                //reproducirSonidoPasos();
            }
			if (GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.A) && !muerto)
            {
                sonidoPasos.play(true);
                //reproducirSonidoPasos();
            }
			if (GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.S) && !muerto)
            {
                sonidoPasos.play(true);
                //reproducirSonidoPasos();
            }
			if (GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.D) && !muerto)
            {
                sonidoPasos.play(true);
                //reproducirSonidoPasos();
            }
			
			//Si no esta ninguna direccion apretada, paro el sonido
			if( !GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.W) &&
			    !GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.A) &&
			    !GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.S) &&
			    !GuiController.Instance.D3dInput.keyDown(Microsoft.DirectX.DirectInput.Key.D) )
			{
                sonidoPasos.stop();
                //sonidoPies[0].stop();
                //sonidoPies[1].stop();
			}
            
			//Activar objetos
			if(GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.E))
			{
                //Aca deberia ir el codigo
                mapa.activarObjetos();
				//Mapa despues deberia tomar la posicion de la camara y checkear la colision
			}

            //Activar gameOverScreen (solo p/debug)
            /*if(GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.M))
			{
				GameOver.Instance.activar();
                morir();
			}*/

            if (!muerto)
            {
                verificarSiMori();
            }
            if(!ganaste)
            {
            	verificarSiGane();
            }

            //updateEstado();

            renderizarLoQueVeo();

            //mapa.iluminarCuartos();

            cuerpo.setCenter(posActual);
            


            //lobo
            //voy dejando el rastro por donde voy pasando y lo guardo en el DiosMapa
            //el primer if para no hacerlo en cada render, si no despues de un tiempo, es para la performance
            sumadorParaDejarRastro += GuiController.Instance.ElapsedTime;
            if (sumadorParaDejarRastro > tiempoParaDejarRastro)
            {
                sumadorParaDejarRastro = 0;

                Punto puntoDondeEstoy = DiosMapa.Instance.obtenerPuntoPorPosicion(camaraFPS.camaraFramework.getPosition());
                    puntoDondeEstoy.listaDeOrdenEnQueElPersonajePasoPorEstePunto.Add(DiosMapa.Instance.contadorSiguienteABuscar());

                DiosMapa.Instance.agregarPuntoAListaPersecucion(puntoDondeEstoy);
            }
            
            //Agrego el render para el final
            creditosEnd.render();
        }

        internal bool estaSiendoPerseguido()
        {
            return estado == state.ASUSTADO;
        }

        private void renderizarLoQueVeo()
        {
            if (estado==state.ASUSTADO)
            {
                configPosProcesado.renderizarPosProcesado(GuiController.Instance.ElapsedTime,2);

                if (!muerto)
                {
                    sonidoLatidos.play();
                    sonidoRespiracionAgitada.play();
                }
                //configIluminador.renderizarIluminador();
            }
            if (estado==state.PASEANDO)
            {
                configIluminador.renderizarIluminador();
            }

            if (estado==state.ESCONDIDO)
            {
                timerRetrasoPostProcesadoYEscondido += GuiController.Instance.ElapsedTime;

            	bool yaTermino = false;

                sonidoRespiracion.play();
            	
            	if(timerRetrasoPostProcesadoYEscondido < 7)//4 me parece mejor
            	{
                    ((PosProcesadoBur)configPosProcesado.posProcesados[2]).initRedAndBlur();
                    configPosProcesado.renderizarPosProcesado(GuiController.Instance.ElapsedTime,2);
            	}
                else
                {            		            	
                	//yaTermino = configPosProcesado.renderizarEfectoEscondido() || !agachado();
                	yaTermino = configPosProcesado.renderizarEfectoEscondido();
            	}

                if (yaTermino || configIluminador.iluminadorEstaEncendido() )
                {
                    estado = state.PASEANDO;
                    timerRetrasoPostProcesadoYEscondido = 0;
                    ((PosProcesadoBur)configPosProcesado.posProcesados[2]).initRedAndBlur();
                    //sonidoRespiracion.stop();
                }
            }

        }

        /*private void updateEstado()
        {
            if (estado==state.ESCONDIDO)
            {
                //respiracionHon
            }
        }*/

        internal void calmate()
        {
            estado = state.ESCONDIDO;
            
            Random r = new Random();
            timerRetrasoPostProcesadoYEscondido = r.Next() % 5;
        }

        /*private int pieActual = 1;

        private void reproducirSonidoPasos()
        {
            sonidoPies[pieActual].stop();

            pieActual = pieActual % 1;

            sonidoPies[pieActual].play();
        }*/

        internal bool iluminadorEncendido()
        {
            return configIluminador.iluminadorEstaEncendido();
        }

        internal bool fluorActivado()
        {
            return configIluminador.fluorActivado();
        }

        private void verificarSiMori()
        {
            if (ColinaAzul.Instance.colisionaEsferaCaja(cuerpo,AnimatedBoss.Instance.getBoundingBox()))
            {
                morir();
            }
        }

        public void calcularColisiones()
        {
            TgcBoundingBox obstaculo = new TgcBoundingBox();

            Vector3 posActual = camaraFPS.camaraFramework.Position;
            posActual.Y = alturaAgachado;
            
            cuerpo.setCenter(posActual);

            if (mapa.colisionaPersonaje(cuerpo, ref obstaculo))
            {
                Vector3 slide = obtenerVectorSlide(obstaculo);

                Vector3 desplazamiento = camaraFPS.camaraFramework.Position - posMemento;

                //desplazamiento.Normalize();

                Vector3 movement =  Vector3.Dot(desplazamiento, slide) * slide;

                cuerpo.setCenter(posMemento + movement);

                if (mapa.colisionaPersonaje(cuerpo, ref obstaculo))
                {
                    movement = new Vector3(0, 0, 0);
                }

                camaraFPS.camaraFramework.setPosition(posMemento + movement);
            }
        }

        internal bool agachado()
        {
            return camaraFPS.camaraFramework.Position.Y < alturaParado;
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

        public void morir()
        {
          GameOver.Instance.activar();
          camaraFPS.camaraFramework.activada = false;
          AnimatedBoss.Instance.activado = false;
          configIluminador.apagarBateria();
          muerto = true;
        }

        public void ganar()
        {
        	creditosEnd.activar();
        	camaraFPS.camaraFramework.activada = false;
        	AnimatedBoss.Instance.activado = false;
        	configIluminador.apagarBateria();
        	ganaste = true;
        }
		private void verificarSiGane()
        {
			if (ColinaAzul.Instance.colisionaEsferaCaja(cuerpo,ganasteBox))
            {
                ganar();
            }
        }

        internal void asustate()
        {
            estado = state.ASUSTADO;
        }
        
    }
}

/*
        private void verificarSiMori()
        {
            if (ColinaAzul.Instance.colisionaEsferaCaja(cuerpo,AnimatedBoss.Instance.getBoundingBox()))
            {
                morir();
            }
        }

        public void calcularColisiones()
        {
            TgcBoundingBox obstaculo = new TgcBoundingBox();

            Vector3 posActual = camaraFPS.camaraFramework.Position;
            posActual.Y = alturaAgachado;
            
            cuerpo.setCenter(posActual);

            if (mapa.colisionaPersonaje(cuerpo, ref obstaculo))
            {
                Vector3 slide = obtenerVectorSlide(obstaculo);

                Vector3 desplazamiento = camaraFPS.camaraFramework.Position - posMemento;

                //desplazamiento.Normalize();

                Vector3 movement =  Vector3.Dot(desplazamiento, slide) * slide;

                cuerpo.setCenter(posMemento + movement);

                if (mapa.colisionaPersonaje(cuerpo, ref obstaculo))
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

        public void morir()
        {
//            GameOver.Instance.activar();
//            camaraFPS.camaraFramework.activada = false;
//            AnimatedBoss.Instance.activado = false;
//            configIluminador.apagarBateria();
//            muerto = true;
        }




        }
    }

*/
