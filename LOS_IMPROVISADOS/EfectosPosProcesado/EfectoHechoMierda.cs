/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 30/05/2016
 * Time: 18:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using TgcViewer;
using TgcViewer.Utils.Shaders;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Interpolation;
using System.Drawing;
using TgcViewer.Utils;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.EfectosPosProcesado
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class PosProcesoHechoMierda : APosProcesado
	{
		
		float time;

		public PosProcesoHechoMierda(Mapa mapa):base(mapa)
		{
			this.mapa = mapa;
		}
		
		public override void init()
		{
            meshes = mapa.escena.Meshes;//escenaFiltrada;

            Device d3dDevice = GuiController.Instance.D3dDevice;
            
            GuiController.Instance.CustomRenderEnabled = true;

            CustomVertex.PositionTextured[] screenQuadVertices = new CustomVertex.PositionTextured[]
            {
                new CustomVertex.PositionTextured( -1, 1, 1, 0,0),
                new CustomVertex.PositionTextured(1,  1, 1, 1,0),
                new CustomVertex.PositionTextured(-1, -1, 1, 0,1),
                new CustomVertex.PositionTextured(1,-1, 1, 1,1)
            };
            
            screenQuadVB = new VertexBuffer(typeof(CustomVertex.PositionTextured),
                    4, d3dDevice, Usage.Dynamic | Usage.WriteOnly,
                        CustomVertex.PositionTextured.Format, Pool.Default);
            screenQuadVB.SetData(screenQuadVertices, 0, LockFlags.None);

            renderTarget2D = new Texture(d3dDevice, d3dDevice.PresentationParameters.BackBufferWidth
                    , d3dDevice.PresentationParameters.BackBufferHeight, 1, Usage.RenderTarget,
                        Format.X8R8G8B8, Pool.Default);
            
            effect = TgcShaders.loadEffect(GuiController.Instance.AlumnoEjemplosDir + "\\LOS_IMPROVISADOS\\Shaders\\HechoMierda.fx");

            effect.Technique = "HechoMierdaTechnique";
            	            
            foreach(TgcMesh mesh in meshes){
            	mesh.Effect = effect;
            }
            
            time = 0;
		}
		
//		public override void render(float elapsedTime)
//		{
//			Device d3dDevice = GuiController.Instance.D3dDevice;
//			
//			time += elapsedTime;
//			
//			pOldRT = d3dDevice.GetRenderTarget(0);
//            Surface pSurf = renderTarget2D.GetSurfaceLevel(0);
//            d3dDevice.SetRenderTarget(0, pSurf);
//            d3dDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
//
//            drawSceneToRenderTarget(d3dDevice);
//
//            pSurf.Dispose();
//            
//            d3dDevice.SetRenderTarget(0, pOldRT);
//            
//            drawPostProcess(d3dDevice);
//		}

		public override void render(float elapsedTime){

            meshes = mapa.escena.Meshes;//escenaFiltrada;
			
			Device device = GuiController.Instance.D3dDevice;
			
			device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
			effect.SetValue("time", time);
			
			foreach(TgcMesh mesh in meshes){
				mesh.Effect = effect;
				mesh.Technique = "HechoMierdaTechnique";
				mesh.render();
			}
			
			time += elapsedTime;
		}
		
		public override void drawSceneToRenderTarget(Device d3dDevice)
		{
			d3dDevice.BeginScene();
			
			GuiController.Instance.Text3d.drawText("FPS: " + HighResolutionTimer.Instance.FramesPerSecond, 0, 0, Color.Yellow);
            
			foreach (TgcMesh m in meshes)
            {
				m.Effect = effect;
				m.Technique = "HechoMierdaTechnique";
                m.render();
            }
		}
		
		public override void drawPostProcess(Device d3dDevice)
		{
			d3dDevice.EndScene();

			d3dDevice.BeginScene();
			
			d3dDevice.VertexFormat = CustomVertex.PositionTextured.Format;
            d3dDevice.SetStreamSource(0, screenQuadVB, 0);
            
            effect.Technique = "HechoMierdaTechnique";
            
            effect.SetValue("time",time);
            
            d3dDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
            effect.Begin(FX.None);
            effect.BeginPass(0);
            d3dDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            effect.EndPass();
            effect.End();
            
            d3dDevice.EndScene();
		}
		
		public override void close()
		{
			foreach (TgcMesh m in meshes)
            {
                m.dispose();
            }
            effect.Dispose();
            alarmTexture.dispose();
            screenQuadVB.Dispose();
            renderTarget2D.Dispose();
		}
	}
}