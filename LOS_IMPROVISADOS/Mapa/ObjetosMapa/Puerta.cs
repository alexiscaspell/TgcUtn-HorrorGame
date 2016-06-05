/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 24/05/2016
 * Time: 13:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Mapa.ObjetosMapa
{
	/// <summary>
	/// Description of Class2.
	/// </summary>
	public class Puerta
	{
		TgcMesh mesh;
		TgcScene escena;
		const float speed = 10;
		float anguloRotacion;
		int abriendose = 0; //0=no se mueve, 1=se abre, -1=se cierra
		int abriendoseAnterior = 0;
		
		public Puerta(float posX, float posY, float posZ){
			
			TgcSceneLoader loader = new TgcSceneLoader();
			escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\puertaBerreta-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\mapa\\");
			
			mesh = escena.Meshes[0];
			
			mesh.Position = new Vector3(posX,posY,posZ);
			mesh.Rotation = new Vector3(0,0,0);
			
			mesh.Scale = new Vector3(0.8f,0.8f,0.8f);
		}
		
		public void abrirPuerta(){
			
			anguloRotacion = speed;
			abriendose = 1;
			
			//Para que abra y cierre alternadamente
			if(abriendoseAnterior == 1){
				abriendose = -1;
				abriendoseAnterior = -1;
			}else if(abriendoseAnterior == -1){
				abriendose = 1;
				abriendoseAnterior = 1;
			}
		}
		
		public void update(float elapsedTime){
			
			//se Abre la puerta al apretar E
			if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.E))
            {
                abrirPuerta();
            }

			if(abriendose == 0){
				//No hago nada
			}else if(abriendose == 1){
				
				mesh.Rotation += new Vector3(0,anguloRotacion*elapsedTime,0);
			}else if(abriendose == -1){
				
				mesh.rotateY(anguloRotacion * elapsedTime);
			}
			
			//Checkeo que no se haya pasado
			if(mesh.Rotation.Y > 90){
				//Clavo la rotacion en 90
				mesh.Rotation = new Vector3(0,90,0);
				abriendose = 0;
			}
			
			if(mesh.Rotation.Y < 0){
				
				mesh.Rotation = new Vector3(0,0,0);
				abriendose = 0;
			}
		}
		
		public void render(){
			mesh.render();
		}
		
		
	}
}
