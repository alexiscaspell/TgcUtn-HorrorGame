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
			
			LinternaObjeto linterna = new LinternaObjeto();
			linterna.cambiarVectores(new Vector3(886.27f,189.29f,10070.98f), new Vector3(1,1,1));
			lista.Add(linterna);
			
			FarolObjeto farol = new FarolObjeto();
			farol.cambiarVectores(new Vector3(10923.17f,217.0f,16466.28f), new Vector3(1,1,1));
			lista.Add(farol);
			
			#region Barriles
			Barril barril1 = new Barril();
			barril1.cambiarVectores(new Vector3(20245.44f, 77.6337f, 9634.468f), new Vector3(1f,1f,1f));
			lista.Add(barril1);
			
			Barril barril2 = new Barril();
			barril2.cambiarVectores(new Vector3(1139.93f,55.68f,590.56f), new Vector3(1f,1f,1f));
			lista.Add(barril2);
			#endregion Barriles
			
			#region Llaves
			LlaveObjeto llave1 = LlaveObjeto.LlaveGris();
			llave1.cambiarVectores(new Vector3(14485.52f,190.0f,1016.86f), new Vector3(0.5f,0.5f,0.5f));
			lista.Add(llave1);
			
			LlaveObjeto llave2 = LlaveObjeto.LlaveExit();
			llave2.cambiarVectores(new Vector3(1379.92f,264.31f,1293.35f), new Vector3(1f,1f,1f));
			lista.Add(llave2);
			
			LlaveObjeto llaveMano = LlaveObjeto.ManoObjeto();
			llaveMano.cambiarVectores(new Vector3(23695.42f,12.56f,18625.01f), new Vector3(0.5f,0.5f,0.5f));
			llaveMano.getMesh().rotateZ(FastMath.PI/2);
			lista.Add(llaveMano);
			
			LlaveObjeto llaveMarron = LlaveObjeto.LlaveMarron();
			llaveMarron.cambiarVectores(new Vector3(6116.38f,243.94f,14732.98f), new Vector3(1f,1f,1f));
			lista.Add(llaveMarron);
			
			LlaveObjeto llaveOxidada = LlaveObjeto.LlaveOxidada();
			llaveOxidada.cambiarVectores(new Vector3(22545.29f,4.02f,1609.09f), new Vector3(1f,1f,1f));
			lista.Add(llaveOxidada);
			#endregion Llaves
			
			#region cruces
			CruzObjeto cruz1 = new CruzObjeto();
			cruz1.cambiarVectores(new Vector3(821.06f,261.99f,1658.88f), new Vector3(1f,1f,1f));
			lista.Add(cruz1);
			
			CruzObjeto cruz2 = new CruzObjeto();
			cruz2.cambiarVectores(new Vector3(18198.37f,3.43f,1227.26f), new Vector3(1f,1f,1f));
			lista.Add(cruz2);
			
			CruzObjeto cruz3 = new CruzObjeto();
			cruz3.cambiarVectores(new Vector3(14044.40f,183.74f,10670.11f), new Vector3(1f,1f,1f));
			lista.Add(cruz3);
			#endregion cruces
			
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
			
			Bateria bateria4 = new Bateria();
			bateria4.cambiarVectores(new Vector3(14484.60f,358.43f,536.55f), new Vector3(0.1f,0.1f,0.1f));
			lista.Add(bateria4);
			
			Bateria bateria5 = new Bateria();
			bateria5.cambiarVectores(new Vector3(13628.82f,180.84f,766.8f), new Vector3(0.1f,0.1f,0.1f));
			lista.Add(bateria5);
			
			Bateria bateria6 = new Bateria();
			bateria6.cambiarVectores(new Vector3(13680.95f,180.84f,855.0f), new Vector3(0.1f,0.1f,0.1f));
			lista.Add(bateria6);
			
			Bateria bateria7 = new Bateria(0);
			bateria7.cambiarVectores(new Vector3(178.68f,269.13f,610.43f), new Vector3(1f,1f,1f));
			lista.Add(bateria7);
			
			Bateria bateria8 = new Bateria(0);
			bateria8.cambiarVectores(new Vector3(165.4f,185.01f,881.57f), new Vector3(1f,1f,1f));
			lista.Add(bateria8);
			#endregion Baterias
			
			#region Puertas
			//puertas con llave
			Puerta puertaGris = Puerta.PuertaGris(1,true);
			puertaGris.cambiarVectores(new Vector3(20949.09f,300.28f,2200.48f), new Vector3(1f,1f,1f));
			lista.Add(puertaGris);
			
			Puerta puertaMarron = Puerta.PuertaMarron(2,true);
			puertaMarron.cambiarVectores(new Vector3(13548.04f,299.59f,400f), new Vector3(1f,1f,1f));
			lista.Add(puertaMarron);
			
			Puerta puertaBlindada = Puerta.PuertaBlindada(3,true);
			puertaBlindada.cambiarVectores(new Vector3(659.27f,295.06f,2997.02f), new Vector3(1f,1f,1f));
			lista.Add(puertaBlindada);
			
			Puerta puertaOxidada = Puerta.PuertaOxidada(4,true);
			puertaOxidada.cambiarVectores(new Vector3(23150.87f,297.81f,16801.77f), new Vector3(1f,1f,1f));
			lista.Add(puertaOxidada);
			
			Puerta puertaExit = Puerta.PuertaExit(5,true);
			puertaExit.cambiarVectores(new Vector3(20950.15f,276f,8902.31f), new Vector3(1f,1f,1f));
			lista.Add(puertaExit);
			
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
			
//			Puerta puerta4 = new Puerta(-1,true);
//			puerta4.cambiarVectores(new Vector3(679.77f,0.0f,2802.13f), new Vector3(1.5f,0.45f,0.7f));
//			lista.Add(puerta4);
//			Puerta puerta4 = Puerta.Puerta3(1,true);
//			puerta4.cambiarVectores(new Vector3(654.2393f,301.5963f,2598.488f),new Vector3(1f,1f,1f));
//			lista.Add(puerta4);
//			
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
