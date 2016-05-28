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
        Vector3 posicion;

        private bool activo { get; set; }

        private const float posY = 5;

        private const float radio = 0.001f;

        private TgcBoundingSphere esfera;

        public Punto(float posX, float posZ, bool activo)
        {
            posicion = new Vector3(posX, posY, posZ);
            this.activo = activo;
            esfera = new TgcBoundingSphere(posicion, radio);
        }

        internal TgcBoundingSphere getSphere()
        {
            return esfera;
        }

        internal void setActivo(bool v)
        {
            activo = v;
        }
    }
}
