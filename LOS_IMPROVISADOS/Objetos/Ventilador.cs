/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 03/07/2016
 * Time: 17:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Ventilador.
	/// </summary>
	public class Ventilador : Accionable
	{
		private const int velRotacion = 1;
		
		public Ventilador()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
            
        	TgcScene escena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Ventilador\\ventilador-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Ventilador\\");
        	
        	mesh = escena.Meshes[0];
		}
		
		public override void render()
		{
			update();
			
			mesh.render();
		}
		
		private void update()
		{
			mesh.rotateY(GuiController.Instance.ElapsedTime * velRotacion);
		}
	}
}
