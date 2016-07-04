using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using TgcViewer;
using TgcViewer.Utils.Shaders;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Interpolation;
using System.Drawing;
using TgcViewer.Utils;
using TgcViewer.Utils.TgcGeometry;
using AlumnoEjemplos.LOS_IMPROVISADOS;
using Microsoft.DirectX;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.EfectosPosProcesado
{
    class PosProcesadoAlarma : APosProcesado
    {
        protected const float renderDistance = 5000;

        public PosProcesadoAlarma(Mapa mapa) : base(mapa)
        {
            init();
        }

        public override void init()
        {
            //Inicio el fondoNegro
            TgcTexture texturaFondo = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
                                                               "Media\\mapa\\fondoNegro.png");

            cajaNegra = TgcBox.fromSize(new Vector3(renderDistance, renderDistance, renderDistance), texturaFondo);

            meshes = new List<TgcMesh>();

            Device d3dDevice = GuiController.Instance.D3dDevice;

            //Activamos el renderizado customizado. De esta forma el framework nos delega control total sobre como dibujar en pantalla
            //La responsabilidad cae toda de nuestro lado
            GuiController.Instance.CustomRenderEnabled = true;


            //Se crean 2 triangulos (o Quad) con las dimensiones de la pantalla con sus posiciones ya transformadas
            // x = -1 es el extremo izquiedo de la pantalla, x = 1 es el extremo derecho
            // Lo mismo para la Y con arriba y abajo
            // la Z en 1 simpre
            CustomVertex.PositionTextured[] screenQuadVertices = new CustomVertex.PositionTextured[]
            {
                new CustomVertex.PositionTextured( -1, 1, 1, 0,0),
                new CustomVertex.PositionTextured(1,  1, 1, 1,0),
                new CustomVertex.PositionTextured(-1, -1, 1, 0,1),
                new CustomVertex.PositionTextured(1,-1, 1, 1,1)
            };
            //vertex buffer de los triangulos
            screenQuadVB = new VertexBuffer(typeof(CustomVertex.PositionTextured),
                    4, d3dDevice, Usage.Dynamic | Usage.WriteOnly,
                        CustomVertex.PositionTextured.Format, Pool.Default);
            screenQuadVB.SetData(screenQuadVertices, 0, LockFlags.None);

            //Creamos un Render Targer sobre el cual se va a dibujar la pantalla
            renderTarget2D = new Texture(d3dDevice, d3dDevice.PresentationParameters.BackBufferWidth
                    , d3dDevice.PresentationParameters.BackBufferHeight, 1, Usage.RenderTarget,
                        Format.X8R8G8B8, Pool.Default);


            //Cargar shader con efectos de Post-Procesado
            effect = TgcShaders.loadEffect(GuiController.Instance.AlumnoEjemplosDir + "Media\\Shaders\\PostProcess.fx");

            //Configurar Technique dentro del shader
            effect.Technique = "AlarmaTechnique";


            //Cargar textura que se va a dibujar arriba de la escena del Render Target
            alarmTexture = TgcTexture.createTexture(d3dDevice, GuiController.Instance.ExamplesMediaDir + "Shaders\\efecto_alarma.png");

            //Interpolador para efecto de variar la intensidad de la textura de alarma
            intVaivenAlarm = new InterpoladorVaiven();
            intVaivenAlarm.Min = 0;
            intVaivenAlarm.Max = 1;
            intVaivenAlarm.Speed = 2;
            intVaivenAlarm.reset();
            
        }

        public override void render(float elapsedTime)
        {
            meshes.Clear();

            updateFondo();

            foreach (TgcMesh mesh in mapa.escena.Meshes)
            {
                if (TgcCollisionUtils.testAABBAABB(mesh.BoundingBox, cajaNegra.BoundingBox))
                {
                    meshes.Add(mesh);
                }
            }
            foreach (Accionable a in mapa.objetos)
            {
                if (TgcCollisionUtils.testAABBAABB(a.getMesh().BoundingBox, cajaNegra.BoundingBox))
                {
                    meshes.Add(a.getMesh());
                }
            }

            Device d3dDevice = GuiController.Instance.D3dDevice;

            //Cargamos el Render Targer al cual se va a dibujar la escena 3D. Antes nos guardamos el surface original
            //En vez de dibujar a la pantalla, dibujamos a un buffer auxiliar, nuestro Render Target.
            pOldRT = d3dDevice.GetRenderTarget(0);
            Surface pSurf = renderTarget2D.GetSurfaceLevel(0);
            d3dDevice.SetRenderTarget(0, pSurf);
            d3dDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);


            //Dibujamos la escena comun, pero en vez de a la pantalla al Render Target
            drawSceneToRenderTarget(d3dDevice);

            //Liberar memoria de surface de Render Target
            pSurf.Dispose();

            //Si quisieramos ver que se dibujo, podemos guardar el resultado a una textura en un archivo para debugear su resultado (ojo, es lento)
            //TextureLoader.Save(GuiController.Instance.ExamplesMediaDir + "Shaders\\render_target.bmp", ImageFileFormat.Bmp, renderTarget2D);


            //Ahora volvemos a restaurar el Render Target original (osea dibujar a la pantalla)
            d3dDevice.SetRenderTarget(0, pOldRT);


            //Luego tomamos lo dibujado antes y lo combinamos con una textura con efecto de alarma
            drawPostProcess(d3dDevice);
        }

        public override void drawSceneToRenderTarget(Device d3dDevice)
        {
            //Arrancamos el renderizado. Esto lo tenemos que hacer nosotros a mano porque estamos en modo CustomRenderEnabled = true
            d3dDevice.BeginScene();

            //Como estamos en modo CustomRenderEnabled, tenemos que dibujar todo nosotros, incluso el contador de FPS
            GuiController.Instance.Text3d.drawText("FPS: " + HighResolutionTimer.Instance.FramesPerSecond, 0, 0, Color.Yellow);
            
            //Dibujamos todos los meshes del escenario
            foreach (TgcMesh m in meshes)
            {
                m.render();
            }

        }


        public override void drawPostProcess(Device d3dDevice)
        {
            //Terminamos manualmente el renderizado de esta escena. Esto manda todo a dibujar al GPU al Render Target que cargamos antes
            d3dDevice.EndScene();

            //Arrancamos la escena
            d3dDevice.BeginScene();

            //Cargamos para renderizar el unico modelo que tenemos, un Quad que ocupa toda la pantalla, con la textura de todo lo dibujado antes
            d3dDevice.VertexFormat = CustomVertex.PositionTextured.Format;
            d3dDevice.SetStreamSource(0, screenQuadVB, 0);

            //Ver si el efecto de alarma esta activado, configurar Technique del shader segun corresponda
            effect.Technique = "AlarmaTechnique";

            //Cargamos parametros en el shader de Post-Procesado
            effect.SetValue("render_target2D", renderTarget2D);
            effect.SetValue("textura_alarma", alarmTexture.D3dTexture);
            effect.SetValue("alarmaScaleFactor", intVaivenAlarm.update());

            //Limiamos la pantalla y ejecutamos el render del shader
            d3dDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
            effect.Begin(FX.None);
            effect.BeginPass(0);
            d3dDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            effect.EndPass();
            effect.End();

            //Terminamos el renderizado de la escena
            d3dDevice.EndScene();
        }
        
        public override void close()
        {
            foreach (TgcMesh m in meshes)
            {
                m.dispose();
            }
            effect.Dispose();
            alarmTexture.dispose();
            screenQuadVB.Dispose();
            renderTarget2D.Dispose();
        }

        public TgcBox cajaNegra;

        public void updateFondo()
        {
            cajaNegra.Position = CamaraFPS.Instance.camaraFramework.Position + CamaraFPS.Instance.camaraFramework.viewDir * (renderDistance / 2);
        }
    }
}
