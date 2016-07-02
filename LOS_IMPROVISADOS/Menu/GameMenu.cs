using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.Sound;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using AlumnoEjemplos.LOS_IMPROVISADOS;

namespace AlumnoEjemplos.MiGrupo
{
    public class GameMenu
    {
        EjemploAlumno application;
        private TgcSprite pantalla;
        private List<GameButton> botones;
        private int selectedButton = 0;

        private TgcStaticSound sonidoCambio = new TgcStaticSound();
        private TgcStaticSound sonidoAceptar = new TgcStaticSound();

        public GameMenu(string imagenFondo)
        {
            pantalla = new TgcSprite();
            pantalla.Texture = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir
                                                                 + "Media\\Menu\\" + imagenFondo + ".png");

            botones = new List<GameButton>();

            //Cargo los sonidos
            sonidoAceptar.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\SonidoAceptar.wav");
            sonidoCambio.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\SonidoCambio.wav");
        }

        internal void init(EjemploAlumno app)
        {
            application = app;

            pantalla.Position = new Vector2(0, 0);
			
            Size screenSize = ScreenSizeClass.ScreenSize;
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
            update();

            GuiController.Instance.Drawer2D.beginDrawSprite();
            pantalla.render();

            foreach (GameButton boton in botones)
            {
                boton.render();
            }
            GuiController.Instance.Drawer2D.endDrawSprite();
        }

        private void update()
        {
            if (botones.Count == 0) { return; }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.UpArrow)&&selectedButton>0)
            {
                sonidoCambio.play();
                selectedButton--;
            }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.DownArrow)&&selectedButton<botones.Count-1)
            {
                sonidoCambio.play();
                selectedButton++;
            }

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.Space))//por ahora cn space
            {
                sonidoAceptar.play();

                botones[selectedButton].execute(application,this);
            }

            for (int i = 0; i < botones.Count; i++)
            {
                botones[i].unSelect();
            }

            botones[selectedButton].select();
        }

    }
}