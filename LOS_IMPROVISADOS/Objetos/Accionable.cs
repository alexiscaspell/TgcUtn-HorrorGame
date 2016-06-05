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
		protected int agarrado; //cant de veces que se puede accionar
		protected const float distAccion = 100f;
		
		public void acciona(Vector3 posInicial, Vector3 dir){
			
			Vector3 vectorInutil;
			Vector3 posFinal = dir * distAccion;
			
			bool resultado = TgcCollisionUtils.intersectSegmentAABB(posInicial, posFinal, mesh.BoundingBox, out vectorInutil);
			
			if(resultado)
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
		
		public void render()
		{
			if(agarrado > 1){
				mesh.render();
			}
		}
	}
}
