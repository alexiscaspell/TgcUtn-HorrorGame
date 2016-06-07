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

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of MapaObjeto.
	/// </summary>
	public class MapaObjeto : Accionable
	{
		public MapaObjeto()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Mapa\\mapaObjeto-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Mapa");
			
			this.mesh = escena.Meshes[0];
		}
		
		public override void execute()
		{
			Inventario.Instance.agregarItem(new MapaItem() );
		}
	}
}
