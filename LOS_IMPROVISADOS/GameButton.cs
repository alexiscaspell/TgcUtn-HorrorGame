using Microsoft.DirectX;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcSceneLoader;
using System;

namespace AlumnoEjemplos.MiGrupo
{
    public class GameButton
    {
        private TgcSprite spriteBoton;
        internal ButtonAction accion;

        public GameButton(string image)
        {
            spriteBoton = new TgcSprite();
            spriteBoton.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir
                                                                 + "Media\\Menu\\botonStart3.png");
        }

        public void init(Vector2 position,Vector2 scaling)
        {
            spriteBoton.Position = position;
            spriteBoton.Scaling = scaling;
        }

        public void click()
        {
            accion.execute();
        }

        internal void setAction(ButtonAction action)
        {
            this.accion = action;
        }

        internal void render()
        {
            throw new NotImplementedException();
        }
    }
}