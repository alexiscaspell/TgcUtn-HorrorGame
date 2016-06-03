/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 31/05/2016
 * Time: 18:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Cama.
	/// </summary>
	public class Cama
	{
		TgcMesh camaMesh;
		TgcScene escenaCama;
		
		public void init(){
			
			TgcSceneLoader loader = new TgcSceneLoader();
			
			escenaCama = loader.loadSceneFromFile(GuiController.Instance.AlumnoEjemplosDir +
			                         "Media\\CamaHospital\\hospital+bed-TgcScene.xml");
			
			camaMesh = escenaCama.Meshes[0];

            camaMesh.Position = CamaraFPS.Instance.posicion;
		}
		
		public void render(){
			
			foreach(TgcMesh mesh in escenaCama.Meshes){
				mesh.render();
			}
			
			//camaMesh.render();
		}
	}
}
