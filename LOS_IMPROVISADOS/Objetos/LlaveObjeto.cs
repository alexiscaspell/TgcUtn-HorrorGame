/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 08/06/2016
 * Time: 17:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Sound;
using AlumnoEjemplos.LOS_IMPROVISADOS.Objetos.Inventario;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of LlaveObjeto.
	/// </summary>
	public class LlaveObjeto : Accionable
	{
		Llave item;
		TgcStaticSound sonidoAgarrar;
		
		public LlaveObjeto(){}
		
		public static LlaveObjeto ManoObjeto()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Mano\\mano-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Mano\\");
			
			LlaveObjeto nuevaLlave= new LlaveObjeto();
			
			nuevaLlave.mesh = escena.Meshes[0];
			
			nuevaLlave.sonidoAgarrar = new TgcStaticSound();
			nuevaLlave.sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");

			nuevaLlave.item = Llave.LlaveMano();
			
			return nuevaLlave;
		}
		
		public static LlaveObjeto LlaveGris()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\Llave1\\llave1-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\Llave1\\");
			
			LlaveObjeto nuevaLlave= new LlaveObjeto();
			
			nuevaLlave.mesh = escena.Meshes[0];
			
			nuevaLlave.sonidoAgarrar = new TgcStaticSound();
			nuevaLlave.sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");

			nuevaLlave.item = Llave.Llave1();
			
			return nuevaLlave;
		}
		
		public static LlaveObjeto LlaveExit()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\Llave2\\llave2-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\Llave2\\");
			
			LlaveObjeto nuevaLlave= new LlaveObjeto();
			
			nuevaLlave.mesh = escena.Meshes[0];
			
			nuevaLlave.sonidoAgarrar = new TgcStaticSound();
			nuevaLlave.sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");

			nuevaLlave.item = Llave.LlaveExit();
			
			return nuevaLlave;
		}
		
		public static LlaveObjeto LlaveOxidada()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\LlaveOxidada\\LlaveOxidada-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\LlaveOxidada\\");
			
			LlaveObjeto nuevaLlave= new LlaveObjeto();
			
			nuevaLlave.mesh = escena.Meshes[0];
			
			nuevaLlave.sonidoAgarrar = new TgcStaticSound();
			nuevaLlave.sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");

			nuevaLlave.item = Llave.LlaveOxidada();
			
			return nuevaLlave;
		}
		public static LlaveObjeto LlaveMarron()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\LlaveMarron\\LlaveMarron-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Puerta\\LlaveMarron\\");
			
			LlaveObjeto nuevaLlave= new LlaveObjeto();
			
			nuevaLlave.mesh = escena.Meshes[0];
			
			nuevaLlave.sonidoAgarrar = new TgcStaticSound();
			nuevaLlave.sonidoAgarrar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\sonidosJuego\\ruidos inventario\\inventory_click.wav");

			nuevaLlave.item = Llave.LlaveMarron();
			
			return nuevaLlave;
		}
		public override void execute()
		{
			Inventario.Instance.agregarItem(item);
			sonidoAgarrar.play();
		}
		
	}
}
