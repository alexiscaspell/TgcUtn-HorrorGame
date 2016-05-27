/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 26/05/2016
 * Time: 18:11
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
using TgcViewer.Utils.Sound;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	public class MenuJuego
	{
		TgcSprite pantallaPrincipal;
		Boton[] arrayBotones = new Boton[2];
		const int cantBotones = 2;
		int cursor = 0;
		public bool renderizar = true;
		
		private TgcStaticSound sonidoCambio = new TgcStaticSound();
		private TgcStaticSound sonidoAceptar = new TgcStaticSound();
		
		public void init(){
			
			pantallaPrincipal = new TgcSprite();
			pantallaPrincipal.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir
			                                                     + "Media\\Menu\\Dark-Souls-Wallpaper-Download.png");

			pantallaPrincipal.Position = new Vector2(0,0);
			
			Size screenSize = GuiController.Instance.Panel3d.Size;
            Size textureSize = pantallaPrincipal.Texture.Size;
            
            float widthScale = (float)screenSize.Width / (float)textureSize.Width;
            float heightScale = (float)screenSize.Height / (float)textureSize.Height;
          	
            pantallaPrincipal.Scaling = new Vector2(widthScale, heightScale);
            //pantallaPrincipal.Scaling = new Vector2(textureSize.Width / screenSize.Width, textureSize.Height / screenSize.Height);
            //pantallaPrincipal.Scaling = new Vector2(0.0005f * screenSize.Width, 0.001f *screenSize.Height); //Esta proporcion queda mas o menos ajustada al tamaño de la pantalla.
            pantallaPrincipal.Position = new Vector2(FastMath.Max(screenSize.Width / 2 - textureSize.Width / 2, 0), FastMath.Max(screenSize.Height / 2 - textureSize.Height / 2, 0));

            //Inicializo los botones
            for(int i=0; i<cantBotones; i++){
            	arrayBotones[i] = new Boton();
            }
            
            arrayBotones[0].botonStart();
            arrayBotones[1].botonExit();
            
            //Cargo los sonidos
            sonidoAceptar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\SonidoAceptar.wav");
            sonidoCambio.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\SonidoCambio.wav");
            
		}
		
		public void render(){
			
			if(!renderizar){
				//Si ya sali del menu no hago nada mas
				return;
			}
			
			//Cambio el cursor de posicion
			if(GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.UpArrow))
            {
				if(cursor - 1 == -1){
					cursor = cantBotones -1;
				}else{
					cursor = (cursor - 1) % cantBotones;
				}
				
				sonidoCambio.play();
            }
			if(GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.DownArrow))
            {
				cursor = (cursor + 1) % cantBotones;
				
				sonidoCambio.play();
            }
			if(GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.Space))
            {
				arrayBotones[cursor].accionBoton(this);
				
				sonidoAceptar.play();
            }
			
			//Coloreo el boton seleccionado
			for(int i= 0; i<cantBotones;i++){
				arrayBotones[i].setColor(Color.White);
			}
			arrayBotones[cursor].setColor(Color.Yellow);
			
			GuiController.Instance.Drawer2D.beginDrawSprite();

            pantallaPrincipal.render();
            for(int i = 0; i<cantBotones; i++){
            	arrayBotones[i].render();
            }

            GuiController.Instance.Drawer2D.endDrawSprite();
            
		}
	}
}
