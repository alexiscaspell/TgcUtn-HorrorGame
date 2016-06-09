/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 08/06/2016
 * Time: 23:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Sound;
using AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.linternas;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Objetos
{
	/// <summary>
	/// Description of LinternaObjeto.
	/// </summary>
	public class LinternaObjeto : Accionable
	{
		TgcStaticSound sonidoAgarrar;
		
		public LinternaObjeto()
		
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Linterna\\flashlight-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Linterna\\");
			
			this.mesh = escena.Meshes[0];
			
			sonidoAgarrar = new TgcStaticSound();
			sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");
		}
		
		public override void execute()
		{
			Personaje.Instance.configIluminador.iluminadores[0].iluminadorObtenido = true;
			Personaje.Instance.configIluminador.posicionIluminadorActual = 0;
		}
	}
}
