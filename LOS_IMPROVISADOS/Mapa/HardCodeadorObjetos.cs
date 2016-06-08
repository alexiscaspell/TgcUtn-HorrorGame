/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 07/06/2016
 * Time: 19:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Microsoft.DirectX;
using AlumnoEjemplos.LOS_IMPROVISADOS.Objetos;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of HardCodeadorObjetos.
	/// </summary>
	public static class HardCodeadorObjetos
	{
		public static List<Accionable> HardCodearObjetos()
		{
			List<Accionable> lista = new List<Accionable>();

			
			MapaObjeto mapa = new MapaObjeto();
			mapa.cambiarVectores(new Vector3(2179.103f,8.6139f,10272.55f), new Vector3(1,1,1));
			mapa.getMesh().rotateY(FastMath.PI/4);
			lista.Add(mapa);
			
			#region Barriles
			Barril barril1 = new Barril();
			barril1.cambiarVectores(new Vector3(20245.44f, 77.6337f, 9634.468f), new Vector3(1f,1f,1f));
			lista.Add(barril1);
			#endregion Barriles
			
			#region Baterias
			Bateria bateria1 = new Bateria();
			bateria1.cambiarVectores(new Vector3(1060.653f,184.4886f,6661.554f), new Vector3(0.1f,0.1f,0.1f));
			lista.Add(bateria1);
			
			Bateria bateria2 = new Bateria();
			bateria2.cambiarVectores(new Vector3(5342.72f,192.2363f,10692f), new Vector3(0.1f,0.1f,0.1f));
			lista.Add(bateria2);
			
			Bateria bateria3 = new Bateria();
			bateria3.cambiarVectores(new Vector3(5326.568f,15.2f,12911.81f), new Vector3(0.1f,0.1f,0.1f));
			lista.Add(bateria3);
			#endregion Baterias
			
			#region Puertas
			//Puertas Ejez
			Puerta puerta1 = new Puerta(-1,true);
			puerta1.cambiarVectores(new Vector3(2479.43f,0.0f,11201.52f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta1);
			
			Puerta puerta2 = new Puerta(-1, true);
			puerta2.cambiarVectores(new Vector3(5583.99f,0.0f,5905.84f),new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta2);
			
			Puerta puerta3 = new Puerta(-1, true);
			puerta3.cambiarVectores(new Vector3(5583.99f,0.0f,7497.41f),new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta3);
			
			Puerta puerta4 = new Puerta(-1,true);
			puerta4.cambiarVectores(new Vector3(679.77f,0.0f,2802.13f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta4);
			
			Puerta puerta5 = new Puerta(-1,true);
			puerta5.cambiarVectores(new Vector3(8977.60f,0.0f,5502.11f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta5);
			
			Puerta puerta6 = new Puerta(-1,true);
			puerta6.cambiarVectores(new Vector3(8977.60f,0.0f,7699.14f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta6);
			
			Puerta puerta7 = new Puerta(-1,true);
			puerta7.cambiarVectores(new Vector3(12077.20f,0.0f,8399.31f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta7);
			
			Puerta puerta8 = new Puerta(-1,true);
			puerta8.cambiarVectores(new Vector3(12077.20f,0.0f,10693.86f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta8);
			
			Puerta puerta9 = new Puerta(-1,true);
			puerta9.cambiarVectores(new Vector3(12077.20f,0.0f,15313.91f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta9);
			
			Puerta puerta10 = new Puerta(-1,true);
			puerta10.cambiarVectores(new Vector3(12776.87f,0.0f,15287.96f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta10);
			
			Puerta puerta11 = new Puerta(-1,true);
			puerta11.cambiarVectores(new Vector3(12776.87f,0.0f,12984.76f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta11);
			
			Puerta puerta12 = new Puerta(-1,true);
			puerta12.cambiarVectores(new Vector3(12776.87f,0.0f,10684.99f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta12);
			
			Puerta puerta13 = new Puerta(-1,true);
			puerta13.cambiarVectores(new Vector3(23175.89f,0.0f,16995.78f), new Vector3(1.5f,0.45f,0.7f));
			lista.Add(puerta13);
			
			//Puertas ejeX
			Puerta puerta21 = Puerta.Puerta2(-1, false);
			puerta21.cambiarVectores(new Vector3(710.11f,0.0f,11222.56f), new Vector3(0.7f,0.45f,1.5f));
			lista.Add(puerta21);
			
			Puerta puerta22 = Puerta.Puerta2(-1,false);
			puerta22.cambiarVectores(new Vector3(6002.04f,0.0f,10220.73f), new Vector3(0.7f,0.45f,1.5f));
			lista.Add(puerta22);
			
			Puerta puerta23 = Puerta.Puerta2(-1,false);
			puerta23.cambiarVectores(new Vector3(7200.565f,0.0f,3625.022f), new Vector3(0.7f,0.45f,1.5f));
			lista.Add(puerta23);
			
			Puerta puerta24 = Puerta.Puerta2(-1,false);
			puerta24.cambiarVectores(new Vector3(17598.86f,0.0f,1725.658f), new Vector3(0.7f,0.45f,1.5f));
			lista.Add(puerta24);
			
			Puerta puerta25 = Puerta.Puerta2(-1,false);
			puerta25.cambiarVectores(new Vector3(16601.78f,0.0f,1725.658f), new Vector3(0.7f,0.45f,1.5f));
			lista.Add(puerta25);
			
			Puerta puerta26 = Puerta.Puerta2(-1,false);
			puerta26.cambiarVectores(new Vector3(16699.96f,0.0f,9827.324f), new Vector3(0.7f,0.45f,1.5f));
			lista.Add(puerta26);
			
			Puerta puerta27 = Puerta.Puerta2(-1,false);
			puerta27.cambiarVectores(new Vector3(25586.18f,0.0f,18221.57f), new Vector3(0.7f,0.45f,1.5f));
			lista.Add(puerta27);
			#endregion Puertas
			
			return lista;
		}
	}
}
