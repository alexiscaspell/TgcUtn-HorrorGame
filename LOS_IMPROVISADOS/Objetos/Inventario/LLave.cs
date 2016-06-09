/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 05/06/2016
 * Time: 18:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Objetos.Inventario
{
	/// <summary>
	/// Description of LLave.
	/// </summary>
	public class Llave : Item
	{
		public int nroLlave;

		#region constructores
		private Llave(){}
		public static Llave Llave1()
		{
			Llave nuevaLlave = new Llave();
			
			nuevaLlave.sprite = new TgcSprite();
			nuevaLlave.sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
			                                          "Media\\Objetos\\Inventario\\Llave1.png");
			nuevaLlave.nroLlave = 1;
			
			return nuevaLlave;
		}
		public static Llave Llave2()
		{
			Llave nuevaLlave = new Llave();
			
			nuevaLlave.sprite = new TgcSprite();
			nuevaLlave.sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
			                                          "Media\\Objetos\\Inventario\\Llave2.png");
			nuevaLlave.nroLlave = 2;
			
			return nuevaLlave;
		}
		public static Llave LlaveMano()
		{
			Llave nuevaLlave = new Llave();
			
			nuevaLlave.sprite = new TgcSprite();
			nuevaLlave.sprite.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
			                                          "Media\\Objetos\\Inventario\\LlaveMano.png");
			nuevaLlave.nroLlave = 3;
			
			return nuevaLlave;
		}
		//n constructores para cada tipo de llave
		#endregion constructores
		
		public override void execute()
		{
			Personaje.Instance.llaveActual = nroLlave;
		}
		
		
	}
}
