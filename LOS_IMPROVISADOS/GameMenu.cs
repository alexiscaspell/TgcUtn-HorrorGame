using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.MiGrupo
{
    public class GameMenu
    {
        EjemploAlumno application;
        private TgcSprite pantalla;
        private List<GameButton> botones;

        public GameMenu(string imagenFondo)
        {
            pantalla = new TgcSprite();
            pantalla.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir
                                                                 + "Media\\Menu\\" + imagenFondo + ".png");
        }

        internal void init(EjemploAlumno app)
        {
            application = app;

            botones = new List<GameButton>();

            pantalla.Position = new Vector2(0, 0);

            Size screenSize = GuiController.Instance.Panel3d.Size;
            Size textureSize = pantalla.Texture.Size;

            float widthScale = (float)screenSize.Width / (float)textureSize.Width;
            float heightScale = (float)screenSize.Height / (float)textureSize.Height;

            pantalla.Scaling = new Vector2(widthScale, heightScale);
            pantalla.Position = new Vector2(FastMath.Max(screenSize.Width / 2 - textureSize.Width / 2, 0), FastMath.Max(screenSize.Height / 2 - textureSize.Height / 2, 0));
        }

        public void agregarBoton(GameButton boton)
        {
            botones.Add(boton);
        }

        internal void render()
        {
            GuiController.Instance.Drawer2D.beginDrawSprite();
            pantalla.render();

            foreach (GameButton boton in botones)
            {
                boton.render();
            }
            GuiController.Instance.Drawer2D.endDrawSprite();
        }
    }
}