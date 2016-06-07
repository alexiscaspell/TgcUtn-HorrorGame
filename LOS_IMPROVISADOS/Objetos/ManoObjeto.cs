/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 07/06/2016
 * Time: 00:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using AlumnoEjemplos.LOS_IMPROVISADOS.Objetos.Inventario;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of ManoObjeto.
	/// </summary>
	public class ManoObjeto : Accionable
	{
		public ManoObjeto()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Mano\\mano-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Mano");
			
			this.mesh = escena.Meshes[0];
		}
		
		public override void execute()
		{
			Inventario.Instance.agregarItem(Llave.LlaveMano() );
		}
	}
}
