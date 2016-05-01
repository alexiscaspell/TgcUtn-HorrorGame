using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;

using Microsoft.DirectX.Direct3D;
using System.Drawing;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class CamaraFPS
    {
        public Vector3 posicion { get; set; }

        public Vector3 direccionVista { get; set; }

        public CamaraFramework camaraFramework { get; set; }
        
        public CamaraFPS(Vector3 posicionInicial, Vector3 direccionVistaInicial)
        {
            this.posicion = posicionInicial;
            this.direccionVista = direccionVistaInicial;

            camaraFramework = new CamaraFramework();
        }
        
        public void update(){
        	posicion = camaraFramework.Position;
        }
        
        public void init()
        {
            crearModificadores();
            crearVariablesDeUsuario();

            camaraFramework.Enable = true;
            camaraFramework.setCamera(posicion, direccionVista);
            camaraFramework.JumpSpeed = 200f;
        }

        public void render()
        {
            Device d3dDevice = GuiController.Instance.D3dDevice;
            usarModificadoresIniciales();
            usarVariablesDeUsuarioIniciales();
        }
                
        public void crearVariablesDeUsuario()
        {
            GuiController.Instance.UserVars.addVar("camaraX");
            GuiController.Instance.UserVars.addVar("camaraY");
            GuiController.Instance.UserVars.addVar("camaraZ");

            GuiController.Instance.UserVars.addVar("vistaX");
            GuiController.Instance.UserVars.addVar("vistaY");
            GuiController.Instance.UserVars.addVar("vistaZ");

        }

        public void crearModificadores()
        {
            GuiController.Instance.Modifiers.addFloat("velocidadCaminar", 0f, 400f, 60f);
        }

        public void usarVariablesDeUsuarioIniciales()
        {
            GuiController.Instance.UserVars.setValue("camaraX", camaraFramework.getPosition().X);
            GuiController.Instance.UserVars.setValue("camaraY", camaraFramework.getPosition().Y);
            GuiController.Instance.UserVars.setValue("camaraZ", camaraFramework.getPosition().Z);

            GuiController.Instance.UserVars.setValue("vistaX", camaraFramework.getLookAt().X);
            GuiController.Instance.UserVars.setValue("vistaY", camaraFramework.getLookAt().Y);
            GuiController.Instance.UserVars.setValue("vistaZ", camaraFramework.getLookAt().Z);
        }

        public void usarModificadoresIniciales()
        {
            camaraFramework.MovementSpeed = (float)GuiController.Instance.Modifiers["velocidadCaminar"];
        }

    }
}
