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
using System.Windows.Forms;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class CamaraFPS
    {
        public Vector3 posicion { get; set; }

        public Vector3 direccionVista { get; set; }

        public CamaraFramework camaraFramework { get; set; }

        #region Singleton
        private static volatile CamaraFPS instancia = null;

        /// <summary>
        /// Permite acceder a una instancia de la clase GuiController desde cualquier parte del codigo.
        /// </summary>
        public static CamaraFPS Instance
        {
            get
            { return newInstance(); }
        }

        /// <summary>
        /// Crear nueva instancia. Solo el MainForm lo hace
        /// </summary>
        internal static CamaraFPS newInstance()
        {
            if (instancia != null) { }
            else
            {
                new CamaraFPS(new Vector3(1085, 330, 10862), new Vector3(1185, 330, 10862));
            }
            return instancia;
        }

        #endregion

        private CamaraFPS(Vector3 posicionInicial, Vector3 direccionVistaInicial)
        {
            this.posicion = posicionInicial;
            this.direccionVista = direccionVistaInicial;

            camaraFramework = new CamaraFramework();

            init();

            instancia = this;//Esto es para implementar el patron Singleton
        }
        
        public void update(){
        	posicion = camaraFramework.getPosition();
            direccionVista = camaraFramework.viewDir;
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
            usarModificadoresIniciales();
            usarVariablesDeUsuarioIniciales();
            update();
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
            GuiController.Instance.Modifiers.addBoolean("moverCamaraConClik", "Activar", false);
            GuiController.Instance.Modifiers.addFloat("velocidadCaminar", 0f, 1000f, 550f);
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
            camaraFramework.camaraConClikActivado = (bool)GuiController.Instance.Modifiers["moverCamaraConClik"];
            camaraFramework.MovementSpeed = (float)GuiController.Instance.Modifiers["velocidadCaminar"];
        }

    }
}
