/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 08/06/2016
 * Time: 23:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Sound;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.faroles;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Objetos
{
	/// <summary>
	/// Description of FarolObjeto.
	/// </summary>
	public class FarolObjeto : Accionable
	{
		TgcStaticSound sonidoAgarrar;
		
		public FarolObjeto()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Farol\\lantern-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Farol\\");
			
			this.mesh = escena.Meshes[0];
			
			sonidoAgarrar = new TgcStaticSound();
			sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");
		}
		
		public override void execute()
		{
			Personaje.Instance.configIluminador.iluminadores[1].iluminadorObtenido = true;
			Personaje.Instance.configIluminador.posicionIluminadorActual = 1;
		}
	}
}
