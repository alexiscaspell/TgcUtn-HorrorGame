/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 05/06/2016
 * Time: 18:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
		const float ajusteBB = 250;
		
		const float speed = 50;
		float anguloInicial;
		float anguloRotacion = 0;
		bool abierta = false;
		bool rotando = false;
		
        TgcStaticSound puertaCerrada = new TgcStaticSound();
        TgcStaticSound puertaAbriendose = new TgcStaticSound();
        int nroPuerta;//Para que checkee que tenga la misma llave

        //Angulo en grados
        public Puerta(int nroPuerta, float anguloInicial)
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
        }
        
        public Puerta(int nroPuerta,TgcMesh mesh)
        {
        	this.agarrado = int.MaxValue;
            this.mesh = mesh;
            this.nroPuerta = nroPuerta;
        }

        public void init(Vector3 posicion, Vector3 escalado)
        {
            mesh.Position = posicion;
            mesh.Scale = escalado;
        }

        public override void execute()
		{
			if(Personaje.Instance.llaveActual == nroPuerta || nroPuerta <= 0)
			{
				rotando = true;
				nroPuerta = -1; //Para necesitar usar la llave 1 sola vez
				
				if(abierta)
				{
					mesh.BoundingBox.move(new Vector3(ajusteBB,0,-ajusteBB));
				}else{
					mesh.BoundingBox.move(new Vector3(-ajusteBB,0,ajusteBB));
				}
				
				puertaAbriendose.play();
				return;
			}
			
			puertaCerrada.play();
		}

		public void update()
        {
//            if (rotando)
//            {
//                anguloRotacion += speed;
//
//                if (anguloRotacion <= 90)
//                {
//                    if (abierta)
//                    {
//                        mesh.rotateY(Geometry.DegreeToRadian(-speed));
//                    }
//                    else
//                    {
//                        mesh.rotateY(Geometry.DegreeToRadian(speed));
//                    }
//                }
//                else
//                {
//                    anguloRotacion = 0;
//                    rotando = false;
//                    abierta = !abierta;
//                }
//            }
            
            if(rotando)
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
            		anguloRotacion = 0;
            		rotando = false;
            		abierta = !abierta;
            	}
            	
            }
            
        }

        public override void render()
        {
        	update();
            mesh.render();
            mesh.BoundingBox.render();
        }
    }
}
