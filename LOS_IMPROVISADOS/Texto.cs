/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 02/07/2016
 * Time: 16:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.Shaders;
using TgcViewer;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Texto.
	/// </summary>
	public class Texto
	{
		private TgcPlaneWall paredTexto;
		
		public Texto()
		{
			TgcTexture texturaPared = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir + "Media\\texto.png");
			paredTexto = new TgcPlaneWall(new Vector3(0,0,0), new Vector3(1000,1000,1000), TgcPlaneWall.Orientations.YZplane, texturaPared,0.5f,0.5f);
			
			TgcShaders shaders = new TgcShaders();
			shaders.loadCommonShaders();
			
			paredTexto.Effect = shaders.VariosShader;
			paredTexto.Effect.Technique = "PositionTextured";
		}
		
		public void render()
		{
			paredTexto.render();
		}
	}
}
