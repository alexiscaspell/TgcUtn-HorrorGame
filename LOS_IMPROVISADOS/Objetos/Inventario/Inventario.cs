/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 04/06/2016
 * Time: 17:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.Sound;
using AlumnoEjemplos.LOS_IMPROVISADOS.Objetos.Inventario;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Inventario.
	/// </summary>
	public class Inventario
	{
		
		private TgcSprite inventarioScreen;
		private TgcSprite minimap;
		public bool minimapActivado;
		
		//sonidos inventario
        TgcStaticSound sonidoCambio = new TgcStaticSound();
        TgcStaticSound sonidoAbrir = new TgcStaticSound();
        TgcStaticSound sonidoCerrar = new TgcStaticSound();

		private const int cantFilas = 2;
		private const int cantColumnas = 3;//por fila
		private Item[,] listaItems;
		private int indiceFila = 0;
		private int indiceColumna = 0;
		
		private float invWidth;
		private float invHeight;
		
		private bool activado = false; //Para Renderizarlo solo en el momento que debo hacerlo
		
		public TgcTexture texturaSlotLibre;
		
		#region singleton
		private static Inventario instance;
		
		public static Inventario Instance
		{
			get{
				if(instance == null){
					instance = new Inventario();
				}
				
				return instance;
			}
		}
		
		private Inventario()
		{
			//Cargo todos los recursos
			inventarioScreen = new TgcSprite();
			inventarioScreen.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
			                                                    "Media\\Objetos\\Inventario\\Inventario.png");
			minimap = new TgcSprite();
			minimap.Texture =TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
			                                                    "Media\\Objetos\\Inventario\\mapaPiolaTransparenteAzul.png");
			
			listaItems = new Item[cantFilas,cantColumnas];
			
			sonidoCambio.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\SonidoCambio.wav");
			
			sonidoAbrir.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_open.wav");
			sonidoCerrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_close.wav");
			
			init();
			
			Item.texturaSlotVacio = this.texturaSlotLibre;
		}
		#endregion singleton
		
		private void init()
		{
			Size screenSize = ScreenSizeClass.ScreenSize;
            Size textureSize = inventarioScreen.Texture.Size;

            //Asi ocupa toda la pantalla
            float widthScale = (float)screenSize.Width / (float)textureSize.Width;
            float heightScale = (float)screenSize.Height / (float)textureSize.Height;
            //Respeto el aspect Ratio
            float finalScaleInv = FastMath.Min(widthScale,heightScale);
            //Ahora ajusto el tamaño relativo a la pantalla
            finalScaleInv *= 0.7f;
            //Lo pongo en el medio de la pantalla
            inventarioScreen.Scaling = new Vector2(finalScaleInv, finalScaleInv);
            //inventarioScreen.Position = new Vector2(FastMath.Max(screenSize.Width / 2 - textureSize.Width / 2, 0), FastMath.Max(screenSize.Height / 2 - textureSize.Height / 2, 0));
			inventarioScreen.Position = new Vector2(screenSize.Width / 2 - textureSize.Width *finalScaleInv / 2, screenSize.Height / 2 - textureSize.Height*finalScaleInv / 2);

			
            //Ahora creo todos los slots vacios
            //Primero necesito un sprite para hacer los calculos
            TgcSprite slotVacioCalculo = new TgcSprite();
            slotVacioCalculo.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
                                              "Media\\Objetos\\Inventario\\Slot Libre.png");
            
            //Hago esto para cargarla solo una vez y reusarla
            texturaSlotLibre = slotVacioCalculo.Texture;
            
            //Es como si tuviera una nueva pantalla con el tamaño del inventario repito todo lo anterior
            invWidth = (float)textureSize.Width * finalScaleInv;
            invHeight = (float)textureSize.Height * finalScaleInv;
            //Calculo las escalas
            widthScale = invWidth / (float)slotVacioCalculo.Texture.Size.Width;
            heightScale = invHeight / (float)slotVacioCalculo.Texture.Size.Height;
            //Respeto Aspect Ratio
            float finalScaleSlot = FastMath.Min(widthScale,heightScale);
            //Ajusto relativo al menu
            finalScaleSlot *= 0.15f;
            
            //La posicion varia dependiendo cada objeto
//            Vector2 offsetInicial = new Vector2(invWidth*finalScaleInv/5, invHeight*finalScaleInv/3);
//            float incrementoX = texturaSlotLibre.Size.Width * finalScaleSlot + invWidth/20;
//            float incrementoY = texturaSlotLibre.Size.Height * finalScaleSlot + invHeight/10;
//            offsetInicial += inventarioScreen.Position;

			Vector2 offsetInicial = inventarioScreen.Position;
			offsetInicial += new Vector2(invWidth*0.12f,invHeight*0.34f);
			float incrementoX = texturaSlotLibre.Size.Width * finalScaleSlot + invWidth *0.15f;
			float incrementoY = texturaSlotLibre.Size.Width * finalScaleSlot + invHeight *0.15f;
            
            int i,j;
            for(i=0; i<cantFilas;i++)
            {
            	for(j=0; j<cantColumnas;j++)
            	{
            		//Creo un nuevo TgcSprite y le setteo las cosas
            		Vector2 posicionSlot = offsetInicial + new Vector2(j*incrementoX,i*incrementoY);
            		
            		TgcSprite spriteSlotVacio = new TgcSprite();
            		spriteSlotVacio.Position = posicionSlot;
            		spriteSlotVacio.Texture = texturaSlotLibre;
            		spriteSlotVacio.Scaling = new Vector2(finalScaleSlot,finalScaleSlot);
            		
            		listaItems[i,j] = new Item(spriteSlotVacio, false);
            	}
            }
            
            
			//Inicio el minimap. Hago lo mismo que con el inventario
			minimapActivado = false;
            textureSize = minimap.Texture.Size;
			//Asi ocupa toda la pantalla
            widthScale = (float)screenSize.Width / (float)textureSize.Width;
            heightScale = (float)screenSize.Height / (float)textureSize.Height;
            //Respeto el aspect Ratio
            finalScaleInv = FastMath.Min(widthScale,heightScale);
            //Ahora ajusto el tamaño relativo a la pantalla
            finalScaleInv *= 0.75f; //un poco mas grande que el inv
            //Lo pongo en el medio de la pantalla
            minimap.Scaling = new Vector2(finalScaleInv, finalScaleInv);
            minimap.Position = new Vector2(screenSize.Width / 2 - textureSize.Width *finalScaleInv / 2, screenSize.Height / 2 - textureSize.Height*finalScaleInv / 2);

		}//Fin init
		
		public void render()
		{
			update();
			
			if(activado)
			{
	
	            GuiController.Instance.Drawer2D.beginDrawSprite();
	            
	            if(minimapActivado)
	            {
	            	minimap.render();
	            }else
	            {           
		            inventarioScreen.render();
		
		            foreach(Item item in listaItems)
		            {
		            	item.render();
		            }
	            }
	
	            GuiController.Instance.Drawer2D.endDrawSprite();
			}
		}
		
		private void update()
		{
			//Accion de botones
			if(activado)
			{//Para no moverme cuando el inventario esta desactivado y que no haga soniditos
				if(GuiController.Instance.D3dInput.keyPressed(Key.RightArrow) )
				{
					indiceColumna++;
					sonidoCambio.play();
				}
				if(GuiController.Instance.D3dInput.keyPressed(Key.LeftArrow) )
				{
					indiceColumna--;
					sonidoCambio.play();
				}
				if(GuiController.Instance.D3dInput.keyPressed(Key.DownArrow) )
				{
					indiceFila++;
					sonidoCambio.play();
				}
				if(GuiController.Instance.D3dInput.keyPressed(Key.Up) )
				{
					indiceFila--;
					sonidoCambio.play();
				}
				if(GuiController.Instance.D3dInput.keyPressed(Key.Space) )
				{
					if(minimapActivado)
					{
						minimapActivado = false;
					}else
					{
						listaItems[indiceFila,indiceColumna].execute();
						sonidoCambio.play();
					}
				}
				
			}

			if(GuiController.Instance.D3dInput.keyPressed(Key.Q) )
			{
				if(activado){
					activado=false;
					sonidoCerrar.play();
				}else {
					activado = true;
					sonidoAbrir.play();
				}
			}
			
			int i,j;
			for(i=0; i<cantFilas;i++){
				for(j=0; j<cantColumnas; j++){
					
					listaItems[i,j].unselect();
				}
			}
			
			fixIndice();
			listaItems[indiceFila,indiceColumna].select();

		}
		
		private void fixIndice()
		{
			if(indiceFila<0)indiceFila=0;
			if(indiceFila>=cantFilas)indiceFila=cantFilas-1;
			if(indiceColumna<0)indiceColumna=0;
			if(indiceColumna>=cantColumnas)indiceColumna=cantColumnas-1;
		}
		
		public void agregarItem(Item nuevoItem)
		{
			//Busca un slot libre y lo ocupa
			int i,j;
			for(i=0;i<cantFilas;i++){
				for(j=0;j<cantColumnas;j++){
					
					if(!listaItems[i,j].slotOcupado)
					{
						//Me guardo los valores de la posicion
						Vector2 position = listaItems[i,j].sprite.Position;
						Vector2 scaling = listaItems[i,j].sprite.Scaling;
						
						nuevoItem.sprite.Position = position;
						nuevoItem.sprite.Scaling = scaling;
						nuevoItem.slotOcupado = true;
						
						listaItems[i,j] = nuevoItem;
						
						return;
					}
				}
			}
		}
		
		public void quitarItem(int i, int j)
		{
			TgcSprite spriteVacio = new TgcSprite();
			
			Vector2 position = listaItems[i,j].sprite.Position;
			Vector2 scaling = listaItems[i,j].sprite.Scaling;
			
			spriteVacio.Position = position;
			spriteVacio.Scaling = scaling;
			spriteVacio.Texture = texturaSlotLibre;
			
			Item itemVacio = new Item(spriteVacio, false);
			
			listaItems[i,j] = itemVacio;
		}
		
		public void quitarItem()//Sobrecargo esto para poder hacer que trabaje sobre el indice actual
		{
			TgcSprite spriteVacio = new TgcSprite();
			
			Vector2 position = listaItems[indiceFila,indiceColumna].sprite.Position;
			Vector2 scaling = listaItems[indiceFila,indiceColumna].sprite.Scaling;
			
			spriteVacio.Position = position;
			spriteVacio.Scaling = scaling;
			spriteVacio.Texture = texturaSlotLibre;
			
			Item itemVacio = new Item(spriteVacio, false);
			
			listaItems[indiceFila,indiceColumna] = itemVacio;
		}
		
		public void quitarLlave(int nroLlave)
		{
			//Creo slot vacio
			TgcSprite spriteVacio = new TgcSprite();
			
			Vector2 position = listaItems[indiceFila,indiceColumna].sprite.Position;
			Vector2 scaling = listaItems[indiceFila,indiceColumna].sprite.Scaling;
			
			spriteVacio.Position = position;
			spriteVacio.Scaling = scaling;
			spriteVacio.Texture = texturaSlotLibre;
			
			Item itemVacio = new Item(spriteVacio, false);
			
			//Busco la llave
			for(int i=0;i<cantFilas;i++){
				for(int j=0;j<cantColumnas;j++){
					
					if( listaItems[i,j].GetType().Equals(typeof(Llave)) ){
						if( ((Llave)listaItems[i,j]).nroLlave == nroLlave){
							listaItems[i,j] = itemVacio;
						}
					}
				}
			}
			
			
		}
		
	}
}
