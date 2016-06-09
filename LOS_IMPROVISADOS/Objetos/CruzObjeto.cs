/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 08/06/2016
 * Time: 19:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Sound;
using AlumnoEjemplos.LOS_IMPROVISADOS.Objetos.Inventario;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Objetos
{
	/// <summary>
	/// Description of CruzObjeto.
	/// </summary>
	public class CruzObjeto : Accionable
	{
		TgcStaticSound sonidoAgarrar;
		
		public CruzObjeto()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Cruz\\Cruz-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Cruz\\");
			
			this.mesh = escena.Meshes[0];
			
			sonidoAgarrar = new TgcStaticSound();
			sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");
		}
		
		public override void execute()
		{
			Personaje.Instance.configIluminador.iluminadores[2].bateria.aumentarFluor();
			sonidoAgarrar.play();
		}
		
	}
}
