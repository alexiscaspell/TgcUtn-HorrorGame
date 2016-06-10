/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 09/06/2016
 * Time: 23:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using TgcViewer;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of ScreenSize.
	/// </summary>
	public static class ScreenSizeClass
	{
		private static bool yaActivado = false;
		private static Size screenSize;
		public static Size ScreenSize
		{
			get{
				if(!yaActivado){
					screenSize = GuiController.Instance.Panel3d.Size;
					yaActivado = true;
				}
				return screenSize;
			}
		}
	}
}
