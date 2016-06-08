/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 07/06/2016
 * Time: 00:46
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
	/// Description of MapaObjeto.
	/// </summary>
	public class MapaObjeto : Accionable
	{
		TgcStaticSound sonidoAgarrar;
		
		public MapaObjeto()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\MapaObjeto\\mapaObjeto-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\MapaObjeto\\");
			
			this.mesh = escena.Meshes[0];
			
			sonidoAgarrar = new TgcStaticSound();
			sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");
		}
		
		public override void execute()
		{
			Inventario.Instance.agregarItem(new MapaItem() );
			sonidoAgarrar.play();
		}
	}
}
