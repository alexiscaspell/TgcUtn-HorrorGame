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
	public abstract class Agarrable
	{
		protected TgcMesh mesh;
		protected int agarrado = 1; //cant de veces que se puede accionar
		
		public bool acciona(Vector3 pos, Vector3 dir, TgcBoundingBox box){
			
			this.agarrado--;
			
			TgcRay rayo = new TgcRay(pos, dir);
			Vector3 vectorInutil;
			
			return TgcCollisionUtils.intersectRayAABB(rayo, box, out vectorInutil);
			
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
