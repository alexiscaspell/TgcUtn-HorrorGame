/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 03/06/2016
 * Time: 14:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Baterias.
	/// </summary>
	public class Bateria : Agarrable
	{
		int i = 0;
		
		public Bateria()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(GuiController.Instance.AlumnoEjemplosDir +
			                                           "Media\\mapa\\pilaScene-TgcScene.xml");
			
			this.mesh = escena.Meshes[i];
		}
		
	}
}
