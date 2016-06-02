using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Punto
    {
        private Vector3 posicion;

        private bool activo { get; set; }

        private const float posY = 5;//ESTE VALOR SE VA HARDCODEANDO

        private const float radio = 0.001f;

        private TgcBoundingSphere esfera;

        public Punto(float posX, float posZ)
        {
            posicion = new Vector3(posX,0, posZ);
            this.activo = activo;
            Vector3 posicionEsfera = new Vector3(posX, posY, posZ);
            esfera = new TgcBoundingSphere(posicionEsfera, radio);
        }

        internal TgcBoundingSphere getSphere()
        {
            return esfera;
        }

        internal Vector3 getPosition()
        {
            return posicion;
        }
    }
}
