/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 05/06/2016
 * Time: 23:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of MapaItem.
	/// </summary>
	public class MapaItem : Item
	{
		public MapaItem()
		{
			sprite = new TgcSprite();
			sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
			                                          "Media\\Objetos\\Inventario\\mapaIcon.png");
		}
		
		public override void execute()
		{
			Inventario.Instance.minimapActivado = true;
		}
		
	}
}
