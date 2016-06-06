/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 04/06/2016
 * Time: 17:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Item.
	/// </summary>
	public class Item
	{
		public TgcSprite sprite = new TgcSprite();//La textura de los sprites debe tener exactamente el mismo tamaño
		public static TgcTexture texturaSlotVacio {get; set;}
		public bool slotOcupado;
		
		//constructor Vacio
		public Item(){}
		
		public Item(TgcSprite sprite, bool slotOcupado)
		{
			this.sprite = sprite;
			this.slotOcupado = slotOcupado;
		}
		
//		public void ocuparSlot(TgcTexture textura)
//		{
//			sprite.Texture = textura;
//			slotOcupado = true;
//		}
		
//		public void desocuparSlot()
//		{
//			sprite.Texture = texturaSlotVacio;
//			slotOcupado = false;
//		}
		
		public void render()
		{
			sprite.render();
		}
		
		public virtual void execute()
		{
			//Despues le agrego comportamiento
			return;
		}
		
		public void select()
		{
			sprite.Color = Color.Yellow;
		}
		
		public void unselect()
		{
			sprite.Color = Color.White;
		}
		
		
	}
}
