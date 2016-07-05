/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 04/07/2016
 * Time: 16:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using Microsoft.DirectX;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.Sound;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Ganaste.
	/// </summary>
	public class Ganaste
	{
		TgcSprite creditos;
		TgcStaticSound musicaCreditos;
		
		bool activado = false;
		
		float speed = 100;
		
		public Ganaste()
		{
			creditos = new TgcSprite();
			creditos.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
			                                            "Media\\Ganaste\\creditos.png");
			
			musicaCreditos = new TgcStaticSound();
			musicaCreditos.loadSound(GuiController.Instance.AlumnoEjemplosDir +
			                         "Media\\Ganaste\\musicBox.wav");
			
			//Calculo el escalado
			Size screenSize = ScreenSizeClass.ScreenSize;
			Size creditosSize = creditos.Texture.Size;
			
			//Lo escalo de acuerdo al ancho
			float escalado = (float)screenSize.Width / (float)creditosSize.Width;
      		//Para mantener el aspect Ratio
            creditos.Scaling = new Vector2(escalado,escalado);
            
            //Pos inicial centro, abajo de todo
            creditos.Position = new Vector2(
            		screenSize.Width/2 - creditosSize.Width * escalado / 2,
            		screenSize.Height);
            
            //calculo el speed(dur aprox:47 seg)
            speed = (screenSize.Height / 2 + creditosSize.Height * escalado) / 47;
            
        }
		
		public void render()
		{
			if(activado)
			{
				update(GuiController.Instance.ElapsedTime);
			
				GuiController.Instance.Drawer2D.beginDrawSprite();
	            creditos.render();
				GuiController.Instance.Drawer2D.endDrawSprite();
			}
		}
		
		public void activar()
		{
			activado = true;
			musicaCreditos.play();
		}
		
		private void update(float elapsedTime)
		{
			creditos.Position -= new Vector2(0, speed * elapsedTime);
		}
	}
}
