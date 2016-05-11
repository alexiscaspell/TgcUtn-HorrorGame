using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    abstract class Colisionador
    {
        private Vector3 posicionMemento;
        private bool colisiona = true;

        public void updateMemento()
        {
            posicionMemento = getPosition();
        }

        public void setColisiona(bool collide)
        {
            colisiona = collide;
        }

        public void colisionar()
        {
            if (colisiona) { 
            Vector3 vecRetroceso = getPosition() - posicionMemento;
            vecRetroceso.Subtract(new Vector3(0, vecRetroceso.Y, 0));
            vecRetroceso.Multiply(1.5f);
            retroceder(vecRetroceso);
            }
        }

        public abstract void retroceder(Vector3 vecRetroceso);
        
        abstract public Vector3 getPosition();

        public abstract TgcBoundingBox getBoundingBox();

    }
}
