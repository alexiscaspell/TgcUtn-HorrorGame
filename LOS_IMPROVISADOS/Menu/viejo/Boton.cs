/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 26/05/2016
 * Time: 19:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Boton
	{
		public TgcSprite spriteBoton;
		public delegate void Del(MenuJuego menu);
		Del delAccionBoton;
        internal Vector2 normalScale;

        public void botonStart(){
			
			spriteBoton = new TgcSprite();
			spriteBoton.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir
			                                                     + "Media\\Menu\\botonStart3.png");

			Size screenSize = GuiController.Instance.Panel3d.Size;
            Size textureSize = spriteBoton.Texture.Size;

            spriteBoton.Position = new Vector2( 0.75f*screenSize.Width, 0.2f*screenSize.Height);
            normalScale = new Vector2(0.0004f * screenSize.Width, 0.0007f * screenSize.Height);
            spriteBoton.Scaling = normalScale;
            
            delAccionBoton = accionStart;
		}
		
		public void botonExit(){
			
						
			spriteBoton = new TgcSprite();
			spriteBoton.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir
			                                                     + "Media\\Menu\\botonHelp.png");

			Size screenSize = GuiController.Instance.Panel3d.Size;
            Size textureSize = spriteBoton.Texture.Size;

            spriteBoton.Position = new Vector2( (screenSize.Width - textureSize.Width)/2, 600);
            
            delAccionBoton = accionExit;
		}
		
		public void render(){

			spriteBoton.render();
		}
		
		public void setColor(Color color){
			this.spriteBoton.Color = color;
		}
		
		//Defino las acciones de los botones
		public void accionBoton(MenuJuego menu){
			//Para tratar a todos los botones por igual
			delAccionBoton(menu);
		}
		
		public void accionStart(MenuJuego menu){
			menu.renderizar = false;
		}
		
		public void accionExit(MenuJuego menu){
			return;
		}
	}
}
