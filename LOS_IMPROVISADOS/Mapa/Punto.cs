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

        private const float posY = 100;//ESTE VALOR SE VA HARDCODEANDO

        private const float radio = 30f;

        private TgcBoundingSphere esfera;

        //lobo
        public List<int> listaDeOrdenEnQueElPersonajePasoPorEstePunto { get; set; }
        
        public Punto(float posX, float posZ)
        {
            posicion = new Vector3(posX, 0, posZ);
            Vector3 posicionEsfera = new Vector3(posX, posY, posZ);
            esfera = new TgcBoundingSphere(posicionEsfera, radio);


            //lobo
            listaDeOrdenEnQueElPersonajePasoPorEstePunto = new List<int> { };
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
