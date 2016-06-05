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
using TgcViewer.Utils._2D;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Item.
	/// </summary>
	public class Item
	{
		private TgcSprite item = new TgcSprite();
		
		public Item(TgcSprite sprite)
		{
			this.item = sprite;
		}
		public void render()
		{
			item.render();
		}
		public void execute()
		{
			//Despues le agrego comportamiento
			return;
		}
		public void select()
		{
			item.Color = Color.Yellow;
		}
		public void unselect()
		{
			item.Color = Color.White;
		}
	}
}
