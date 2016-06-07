/*
 * Created by SharpDevelop.
 * User: Lelouch
 * Date: 05/06/2016
 * Time: 18:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Sound;
using TgcViewer;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
	/// <summary>
	/// Description of Puerta.
	/// </summary>
	public class Puerta : Accionable
	{
		const float speed = 10;
		float anguloRotacion = 0;
		bool abierta = false;
		bool rotando = false;
		
		TgcStaticSound puertaCerrada;
		TgcStaticSound puertaAbriendose;
        int nroPuerta;//Para que checkee que tenga la misma llave
        private TgcScene escena;

        //Hay que hacerle un constructor que le asigne la metadata
        //(El agarrado deberia ir en 1000000 o algo asi exagerado para que siempre la pueda abrir/cerrar)

        public Puerta(int nroPuerta,TgcMesh mesh)
        {
            this.mesh = mesh;
            this.nroPuerta = nroPuerta;
        }

        public void init(Vector3 posicion, Vector3 escalado)
        {
            mesh.Position = posicion;
            mesh.Scale = escalado;
        }

        public override void execute()
		{
			if(Personaje.Instance.llaveActual == nroPuerta || nroPuerta <= 0)
			{
				rotando = true;
				nroPuerta = -1; //Para necesitar usar la llave 1 sola vez
				
				puertaAbriendose.play();
				return;
			}
			
			puertaCerrada.play();
		}

		public void update()
        {
            if (rotando)
            {
                anguloRotacion += speed;

                if (anguloRotacion <= 90)
                {
                    if (abierta)
                    {
                        mesh.rotateY(Geometry.DegreeToRadian(-speed));
                    }
                    else
                    {
                        mesh.rotateY(Geometry.DegreeToRadian(speed));
                    }
                }
                else
                {
                    anguloRotacion = 0;
                    rotando = false;
                    abierta = !abierta;
                }
            }
        }

        public void render()
        {
        	update();
            mesh.render();
        }

        internal TgcMesh getMesh()
        {
            return mesh;
        }
    }
}
