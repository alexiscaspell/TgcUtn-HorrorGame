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
using TgcViewer.Utils.Sound;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Baterias.
	/// </summary>
	public class Bateria : Accionable
	{		
		TgcStaticSound sonidoAgarrar;
		
		public Bateria()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Pilas\\bateria-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Pilas\\");
			
			this.mesh = escena.Meshes[0];
			
			sonidoAgarrar = new TgcStaticSound();
			sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");
		}
		
		//Hago un constructor con un entero para usar el otro modelo
		public Bateria(int a)
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Pilas\\PilaBienUbicada\\pila-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Pilas\\PilaBienUbicada\\");
			
			this.mesh = escena.Meshes[0];
			
			sonidoAgarrar = new TgcStaticSound();
			sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");
		}

		public override void execute()
		{
			Inventario.Instance.agregarItem(new PilaItem() );
			sonidoAgarrar.play();
		}
		
		
	}
}
