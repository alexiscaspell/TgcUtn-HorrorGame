﻿/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 03/06/2016
 * Time: 14:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Baterias.
	/// </summary>
	public class Bateria : Accionable
	{		
		public Bateria()
		{
			TgcSceneLoader loader = new TgcSceneLoader();
			TgcScene escena = loader.loadSceneFromFile(
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Bateria\\bateria-TgcScene.xml",
				GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\Bateria");
			
			this.mesh = escena.Meshes[0];
		}

		public override void execute()
		{
			Inventario.Instance.agregarItem(new PilaItem() );
		}
		
		
	}
}
