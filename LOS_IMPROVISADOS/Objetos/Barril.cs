/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 07/06/2016
 * Time: 00:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Sound;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Objetos
{
	/// <summary>
	/// Description of Barril.
	/// </summary>
	public class Barril : Accionable
	{
		int cantCargas = 5;
		
		TgcStaticSound sonidoRecarga =new TgcStaticSound();
		TgcStaticSound sonidoVacio = new TgcStaticSound();
		
		public Barril()
		{
			this.agarrado = int.MaxValue;
			
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Barril\\barril-TgcScene.xml",
			    GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Barril\\");
			
			this.mesh = escena.Meshes[0];
			
			sonidoVacio.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Barril\\barrilVacio.wav");
			sonidoRecarga.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Barril\\barrilRecarga.wav");
		}
		
		public override void execute()
		{
			if(cantCargas > 0)
			{
				Personaje.Instance.configIluminador.iluminadores[1].bateria.recargar();
				sonidoRecarga.play();
				
				cantCargas--;
			}else{
				sonidoVacio.play();
			}
			
		}
	}
}
