/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 05/06/2016
 * Time: 15:34
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
	/// Description of PilaItem.
	/// </summary>
	public class PilaItem : Item
	{
		public PilaItem()
		{
			sprite = new TgcSprite();
			sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
			                                          "Media\\Objetos\\Inventario\\PilaItem.png");
		}
		
		public override void execute()
		{
			//recargo la bateria
			Personaje.Instance.configIluminador.iluminadores[0].bateria.recargar();
			
			//Ya use el item, desocupo el slot
			Inventario.Instance.quitarItem();//sin argumentos, usa los defaults
		}
	}
}
