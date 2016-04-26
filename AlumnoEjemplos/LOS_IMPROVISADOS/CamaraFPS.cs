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
        
        public void init()
        {
            crearModificadores();
            crearVariablesDeUsuario();

            camaraFramework.Enable = true;
            camaraFramework.setCamera(posicion, direccionVista);
            camaraFramework.JumpSpeed = 200f;
            /*
            GuiController.Instance.FpsCamera.Enable = true;
            GuiController.Instance.FpsCamera.setCamera(posicion, direccionVista);
            GuiController.Instance.FpsCamera.JumpSpeed = 200f;
            */
        }

        public void render()
        {
            Device d3dDevice = GuiController.Instance.D3dDevice;
            usarModificadoresIniciales();
            usarVariablesDeUsuarioIniciales();

            camaraFramework.updateCamera();
            camaraFramework.updateViewMatrix(d3dDevice);
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
            GuiController.Instance.Modifiers.addFloat("velocidadCaminar", 0f, 400f, 40f);
        }

        public void usarVariablesDeUsuarioIniciales()
        {
            GuiController.Instance.UserVars.setValue("camaraX", GuiController.Instance.FpsCamera.getPosition().X);
            GuiController.Instance.UserVars.setValue("camaraY", GuiController.Instance.FpsCamera.getPosition().Y);
            GuiController.Instance.UserVars.setValue("camaraZ", GuiController.Instance.FpsCamera.getPosition().Z);

            GuiController.Instance.UserVars.setValue("vistaX", GuiController.Instance.FpsCamera.getLookAt().X);
            GuiController.Instance.UserVars.setValue("vistaY", GuiController.Instance.FpsCamera.getLookAt().Y);
            GuiController.Instance.UserVars.setValue("vistaZ", GuiController.Instance.FpsCamera.getLookAt().Z);
        }

        public void usarModificadoresIniciales()
        {
            //GuiController.Instance.FpsCamera.MovementSpeed = (float)GuiController.Instance.Modifiers["velocidadCaminar"];
            camaraFramework.MovementSpeed = (float)GuiController.Instance.Modifiers["velocidadCaminar"];
        }

    }
}
