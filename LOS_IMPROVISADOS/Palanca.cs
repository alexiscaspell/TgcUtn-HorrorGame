using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Palanca
    {
        private TgcCylinder cilindro;

        private float radio;

        private float longitud;

        private Vector3 posicion;

        private bool bajando;

        private int rotacion;

        public void init()
        {
            posicion = new Vector3(280, 22, 111);
            radio = 1;
            longitud = 5;

            rotacion = 0;

            cilindro =  new TgcCylinder(posicion, radio, longitud);

            cilindro.Color = Color.Black;

            cilindro.rotateX(Geometry.DegreeToRadian(-45f));



            /*float x = (float) (280 + longitud * Math.Cos(Geometry.DegreeToRadian(45f)));
            float y = (float)(22 - longitud * Math.Sin(Geometry.DegreeToRadian(45f)));

            cilindro.Center = new Vector3(x, y, 111);*/

            cilindro.updateValues();
                
        }

        public void render()
        {
            update();
            cilindro.render();
        }

        private void update()
        {
            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.F))
            {
                bajando = true;
            }
            if (bajando) {
                bajarGrados();
            }
        }

        private void bajarGrados()
        {
            
            cilindro.rotateX(Geometry.DegreeToRadian(-1f));

            Vector3 nuevaPos = cilindro.Position;
            nuevaPos.Subtract(new Vector3(0, (float)(longitud * Math.Sin(Geometry.DegreeToRadian(1f))), 0));//0.02), 0));//Math.Sin(1)), 0));
            
            cilindro.Position = nuevaPos;//new Vector3(280, 0, 111); 

            cilindro.updateValues();       

            rotacion++;

            if (rotacion == 45)
            {
                bajando = false;
                rotacion = 0;
            }
        }
    }
}
