/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 03/06/2016
 * Time: 14:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Agarrable.
	/// </summary>
	public abstract class Accionable
	{
		protected TgcMesh mesh;
		public TgcMesh getMesh()
		{
			return this.mesh;
		}
		
		protected int agarrado = 1; //cant de veces que se puede accionar
		internal float distAccion = 300f;
		
		public void acciona(Vector3 posInicial, Vector3 dir){
			
			Vector3 vectorInutil;
			Vector3 posFinal = dir * distAccion + posInicial;
			
			bool resultado = TgcCollisionUtils.intersectSegmentAABB(posInicial, posFinal, mesh.BoundingBox, out vectorInutil);
			
			if(resultado && agarrado >= 1)
			{
				agarrado--;
				execute();
            }
		}
		
		public virtual void execute()
		{
			//Dejo este vacio para poder tener un item que no haga nada, quizas sirva
		}
		
		public TgcBoundingBox getBB()
		{
			return this.mesh.BoundingBox;
		}
		
		public virtual void render()
		{
			if(agarrado >= 1){
				mesh.render();
			}
		}
		
		public void cambiarVectores(Vector3 pos, Vector3 scale)
		{
			mesh.move(pos);
			mesh.Scale = scale;
		}
	}
}
