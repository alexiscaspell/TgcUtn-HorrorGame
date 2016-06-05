/*
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
		PilaItem item;
		
		public Bateria(TgcMesh mesh)
		{
			this.mesh = mesh;
			agarrado = 1;
			item = new PilaItem();
		}

		public override void execute()
		{
			Inventario.Instance.agregarItem(item);
		}
		
		
	}
}
