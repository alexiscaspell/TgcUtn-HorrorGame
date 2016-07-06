/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 05/06/2016
 * Time: 18:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Sound;
using TgcViewer;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Puerta.
	/// </summary>
	public class Puerta : Accionable
	{
		CamaraFramework camara = CamaraFPS.Instance.camaraFramework;
		
		const float ajusteBB = 250;
		
		const float speed = 50;
		float anguloRotacion = 0;
		public bool abierta = false;
		bool colaActivada = false;
		
		Queue<bool> colaRotando = new Queue<bool>();
		
		bool paraleloEjeZ; //para el tema de la animacion de las puertas
		
        TgcStaticSound puertaCerrada = new TgcStaticSound();
        TgcStaticSound puertaAbriendose = new TgcStaticSound();
        int nroPuerta;//Para que checkee que tenga la misma llave
        public bool animacionCamaraActivada = true;

        //Angulo en grados
        public Puerta(){}
        
       	#region constructores
        public Puerta(int nroPuerta, bool paraleloEjeZ)
        {
        	TgcSceneLoader loader = new TgcSceneLoader();
            
        	TgcScene escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\Puerta1\\puerta-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\Puerta1\\");
        	
        	mesh = escena.Meshes[0];
        	
        	puertaCerrada.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\puertaCerrada.wav");
        	puertaAbriendose.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\aperturaPuerta.wav");
        	
        	this.nroPuerta = nroPuerta;
        	this.agarrado = int.MaxValue;
        	this.paraleloEjeZ = paraleloEjeZ;
        }
        
        public static Puerta Puerta2(int nroPuerta, bool paraleloEjeZ)
        {
        	TgcSceneLoader loader = new TgcSceneLoader();
            
        	TgcScene escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\Puerta2\\puerta2-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\Puerta2\\");
        	
        	TgcMesh nuevoMesh = escena.Meshes[0];
        	
        	Puerta nuevaPuerta = new Puerta(nroPuerta, paraleloEjeZ);
        	
        	nuevaPuerta.mesh = nuevoMesh;
        	
        	return nuevaPuerta;
        }
//        
//        public static Puerta Puerta3(int nroPuerta, bool paraleloEjeZ)
//        {
//        	Puerta nuevaPuerta = new Puerta();
//        	
//        	TgcSceneLoader loader = new TgcSceneLoader();
//            
//        	TgcScene escena = loader.loadSceneFromFile(
//                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\Puerta3\\puerta-TgcScene.xml",
//                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\Puerta3\\");
//        	
//        	TgcMesh nuevoMesh = escena.Meshes[0];
//        	
//        	nuevaPuerta.mesh = nuevoMesh;
//        	
//        	nuevaPuerta.puertaCerrada.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\puertaCerrada.wav");
//        	nuevaPuerta.puertaAbriendose.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\aperturaPuerta.wav");
//        	
//        	nuevaPuerta.nroPuerta = nroPuerta;
//        	nuevaPuerta.agarrado = int.MaxValue;
//        	nuevaPuerta.paraleloEjeZ = paraleloEjeZ;
//        	
//        	return nuevaPuerta;
//        }
        
        public static Puerta PuertaGris(int nroPuerta, bool paraleloEjeZ)
        {
        	Puerta nuevaPuerta = new Puerta();
        	
        	TgcSceneLoader loader = new TgcSceneLoader();
            
        	TgcScene escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\PuertaGris\\puerta-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\PuertaGris\\");
        	
        	TgcMesh nuevoMesh = escena.Meshes[0];
        	
        	nuevaPuerta.mesh = nuevoMesh;
        	
        	nuevaPuerta.puertaCerrada.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\puertaCerrada.wav");
        	nuevaPuerta.puertaAbriendose.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\aperturaPuerta.wav");
        	
        	nuevaPuerta.nroPuerta = nroPuerta;
        	nuevaPuerta.agarrado = int.MaxValue;
        	nuevaPuerta.paraleloEjeZ = paraleloEjeZ;
        	
        	return nuevaPuerta;
        }
        
        public static Puerta PuertaMarron(int nroPuerta, bool paraleloEjeZ)
        {
        	Puerta nuevaPuerta = new Puerta();
        	
        	TgcSceneLoader loader = new TgcSceneLoader();
            
        	TgcScene escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\PuertaMarron\\puerta-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\PuertaMarron\\");
        	
        	TgcMesh nuevoMesh = escena.Meshes[0];
        	
        	nuevaPuerta.mesh = nuevoMesh;
        	
        	nuevaPuerta.puertaCerrada.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\puertaCerrada.wav");
        	nuevaPuerta.puertaAbriendose.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\aperturaPuerta.wav");
        	
        	nuevaPuerta.nroPuerta = nroPuerta;
        	nuevaPuerta.agarrado = int.MaxValue;
        	nuevaPuerta.paraleloEjeZ = paraleloEjeZ;
        	
        	return nuevaPuerta;
        }
        
        public static Puerta PuertaBlindada(int nroPuerta, bool paraleloEjeZ)
        {
        	Puerta nuevaPuerta = new Puerta();
        	
        	TgcSceneLoader loader = new TgcSceneLoader();
            
        	TgcScene escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\PuertaBlindada\\puerta-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\PuertaBlindada\\");
        	
        	TgcMesh nuevoMesh = escena.Meshes[0];
        	
        	nuevaPuerta.mesh = nuevoMesh;
        	

        	nuevaPuerta.puertaCerrada.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\puertaCerrada.wav");
        	nuevaPuerta.puertaAbriendose.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\aperturaPuerta.wav");

        	//nuevaPuerta.puertaCerrada.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Sonidos\\electronicDoor.wav");
        	//nuevaPuerta.puertaAbriendose.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\EarthQuake.wav");

        	
        	nuevaPuerta.nroPuerta = nroPuerta;
        	nuevaPuerta.agarrado = int.MaxValue;
        	nuevaPuerta.paraleloEjeZ = paraleloEjeZ;
        	
        	return nuevaPuerta;
        }
        
        public static Puerta PuertaExit(int nroPuerta, bool paraleloEjeZ)
        {
        	Puerta nuevaPuerta = new Puerta();
        	
        	TgcSceneLoader loader = new TgcSceneLoader();
            
        	TgcScene escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\PuertaExit\\PuertaExit-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\PuertaExit\\");
        	
        	TgcMesh nuevoMesh = escena.Meshes[0];
        	
        	nuevaPuerta.mesh = nuevoMesh;
        	
        	nuevaPuerta.puertaCerrada.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\puertaCerrada.wav");
        	nuevaPuerta.puertaAbriendose.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\aperturaPuerta.wav");
        	
        	nuevaPuerta.nroPuerta = nroPuerta;
        	nuevaPuerta.agarrado = int.MaxValue;
        	nuevaPuerta.paraleloEjeZ = paraleloEjeZ;
        	
        	return nuevaPuerta;
        }
        
        public static Puerta PuertaOxidada(int nroPuerta, bool paraleloEjeZ)
        {
        	Puerta nuevaPuerta = new Puerta();
        	
        	TgcSceneLoader loader = new TgcSceneLoader();
            
        	TgcScene escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\PuertaOxidada\\puerta-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\PuertaOxidada\\");
        	
        	TgcMesh nuevoMesh = escena.Meshes[0];
        	
        	nuevaPuerta.mesh = nuevoMesh;
        	
        	nuevaPuerta.puertaCerrada.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\puertaCerrada.wav");
        	nuevaPuerta.puertaAbriendose.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\aperturaPuerta.wav");
        	
        	nuevaPuerta.nroPuerta = nroPuerta;
        	nuevaPuerta.agarrado = int.MaxValue;
        	nuevaPuerta.paraleloEjeZ = paraleloEjeZ;
        	
        	return nuevaPuerta;
        }
        
        public Puerta(int nroPuerta,TgcMesh mesh)
        {
        	this.agarrado = int.MaxValue;
            this.mesh = mesh;
            this.nroPuerta = nroPuerta;
        }
        #endregion constructores

        public void init(Vector3 posicion, Vector3 escalado)
        {
            mesh.Position = posicion;
            mesh.Scale = escalado;
        }

        public override void execute()
		{
			if(Personaje.Instance.llaveActual == nroPuerta || nroPuerta <= 0)
			{
				colaRotando.Enqueue(true);

				if(nroPuerta>0)
				{
					Inventario.Instance.quitarLlave(nroPuerta);
					nroPuerta = -1; //Para necesitar usar la llave 1 sola vez
				}

				nroPuerta = -1; //Para necesitar usar la llave 1 sola vez

                //Aca hago que le diga al boss que puede pasar o no
                //DiosMapa.Instance.activarODesactivarPunto(mesh.BoundingBox.Position);
				
				//puertaAbriendose.play();

                if(animacionCamaraActivada)
                { 
				
				//Le paso la direccion x o z, dependiendo del tipo de puerta
				if(paraleloEjeZ)
				{
					camara.animar(this.mesh.Position.X, !abierta, paraleloEjeZ);
				}else{
					camara.animar(this.mesh.Position.Z, !abierta, paraleloEjeZ);
				}
                    if (Personaje.Instance.estaSiendoPerseguido()&&abierta)
                    {
                        Mapa.Instance.agregarPuertaAPuertasDePersecucion(this);
                    }

                }

                return;
			}
			
			puertaCerrada.play();
		}

		public void update()
        {            
//            if(rotando)
//            {
//            	float cambioAngulo = speed*GuiController.Instance.ElapsedTime;
//            	anguloRotacion += cambioAngulo;
//            	
//            	if(anguloRotacion <= 90)
//            	{
//            		if(abierta)
//            		{
//            			mesh.rotateY(Geometry.DegreeToRadian(-cambioAngulo));
//            		}else{
//            			mesh.rotateY(Geometry.DegreeToRadian(cambioAngulo));
//            		}
//            	}else{
//            		anguloRotacion = 0;
//            		rotando = false;
//            		abierta = !abierta;
//            	}
//            	
//            }

			if(!colaActivada)
			{
				if(colaRotando.Count > 0)
				{
					colaRotando.Dequeue();
					colaActivada = true;
					
					puertaAbriendose.play();
				}
								
			}

			if(colaActivada)
			{
		            	float cambioAngulo = speed*GuiController.Instance.ElapsedTime;
		            	anguloRotacion += cambioAngulo;
		            	
		            	if(anguloRotacion <= 90)
		            	{
		            		if(abierta)
		            		{
		            			mesh.rotateY(Geometry.DegreeToRadian(-cambioAngulo));
		            		}else{
		            			mesh.rotateY(Geometry.DegreeToRadian(cambioAngulo));
		            		}
		            	}else{
		            		//Ajuste de BB
							if(abierta && paraleloEjeZ)
							{
								mesh.BoundingBox.move(new Vector3(ajusteBB,0,-ajusteBB));
							}
							if(!abierta && paraleloEjeZ){
								mesh.BoundingBox.move(new Vector3(-ajusteBB,0,ajusteBB));
							}
							if(abierta && !paraleloEjeZ){
								mesh.BoundingBox.move(new Vector3(-ajusteBB,0,-ajusteBB));
							}
							if(!abierta && !paraleloEjeZ){
								mesh.BoundingBox.move(new Vector3(ajusteBB,0,ajusteBB));
							}
							
		            		anguloRotacion = 0;
		            		colaActivada = false;
		            		abierta = !abierta;
		            		
		            	}
			}
           
            
        }

        public override void render()
        {
        	update();
            mesh.render();
            camara.animacionPuerta();
        }
    }
}
