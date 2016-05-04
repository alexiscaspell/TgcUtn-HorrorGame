using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcGeometry;
using System.Drawing;
using TgcViewer.Example;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Input;
using Microsoft.DirectX.Direct3D;
using TgcViewer.Utils.Shaders;



namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Caja
    {
        private TgcBox caja;

        public Vector3 posicion;

        private Vector3 tamanio;

        private Color color;
        
        public void move(Vector3 v){
        	caja.move(v);
        }
        
        public void changePosicion(Vector3 v){
        	posicion = v;
        }

        public TgcBoundingBox getBoundingBox()
        {
            return caja.BoundingBox;
        }

        public void init()
        {
            posicion = new Vector3(280, 10, 120);

            tamanio = new Vector3(20, 20, 20);

            color = Color.Red;

            caja = TgcBox.fromSize(tamanio, color);

            caja.Position = posicion;

        }

        public void render(CamaraFPS camara)
        {
            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.B)&&estaCerca(camara.posicion))
            {
                color = Color.Blue;
            }
            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.R) && estaCerca(camara.posicion))
            {
                color = Color.Red;
            }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.F))
            {
                //caja.rotateY(45);
                //caja.rotateY(Geometry.DegreeToRadian(45f));
                //posicion.TransformCoordinate()
                    //caja.Rotation = GuiController.Instance.FpsCamera.getLookAt();

                //TgcBoundingBox colisionador = new TgcBoundingBox();

                
            }

            

            update();

            caja.render();

        }

        private void update()
        {
            caja.Position = posicion;
            caja.Color = color;
            caja.Size = tamanio;

            caja.updateValues();
        }

        internal void changeColor(Color color)
        {
            this.color = color;
        }

        public bool estaCerca(Vector3 posObjeto)
        {
            Vector3 vectorAux = posObjeto;
            vectorAux.Subtract(posicion);
            //vectorAux.Subtract(new Vector3(0, vectorAux.Y, 0));//ESTO ES PARA Q NO IMPORTE LA ALTURA

            return vectorAux.Length() <= 30;

        }

    }
}
