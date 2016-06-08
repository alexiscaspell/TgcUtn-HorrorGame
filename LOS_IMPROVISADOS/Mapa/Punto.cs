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

        public bool activo = true;

        private const float posY = 110;//5;//ESTE VALOR SE VA HARDCODEANDO

        private const float radio = 1f;

        private TgcBoundingSphere esfera;

        public Punto(float posX, float posZ)
        {
            posicion = new Vector3(posX, 0, posZ);
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
