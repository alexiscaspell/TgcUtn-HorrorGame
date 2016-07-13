using System.Collections.Generic;
using Microsoft.DirectX.Direct3D;
using TgcViewer;
using TgcViewer.Utils.Shaders;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Interpolation;
using System.Drawing;
using TgcViewer.Utils.TgcGeometry;
using Microsoft.DirectX;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.EfectosPosProcesado
{
    class PosProcesadoBur : APosProcesado
    {
        protected const float renderDistance = 5000;

        private const float valorPredefinidoBlur = 0.01f;
        private const float valorPredefinidoRed = 1.5f;

        private float blur_intensity;
        private float red_intensity;
        private float tiempo = 0f;
        private float signo = 1f; 

        protected const float TIEMPO_EJECUCION_EFECTO = 0.12f;
        protected const float INTENSIDAD_MAX = 0.020f;
        protected const float INTENSIDAD_MIN = 0.001f;
        protected const float AUMENTO = 0.001f;
        private Surface depthStencil;
        //private Surface stencilAnterior;

        public PosProcesadoBur(Mapa mapa) : base(mapa)
        {
            init();
        }

        public override void init()
        {
            //Inicio el fondoNegro
            //TgcTexture texturaFondo = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +"Media\\mapa\\fondoNegro.png");
            //cajaNegra = TgcBox.fromSize(new Vector3(renderDistance, renderDistance, renderDistance), texturaFondo);
            //meshes = new List<TgcMesh>();

            Device d3dDevice = GuiController.Instance.D3dDevice;

            GuiController.Instance.CustomRenderEnabled = true;
            
            CustomVertex.PositionTextured[] screenQuadVertices = new CustomVertex.PositionTextured[]
            {
                new CustomVertex.PositionTextured( -1, 1, 1, 0,0),
                new CustomVertex.PositionTextured(1,  1, 1, 1,0),
                new CustomVertex.PositionTextured(-1, -1, 1, 0,1),
                new CustomVertex.PositionTextured(1,-1, 1, 1,1)
            };
            screenQuadVB = new VertexBuffer(typeof(CustomVertex.PositionTextured),
                    4, d3dDevice, Usage.Dynamic | Usage.WriteOnly,
                        CustomVertex.PositionTextured.Format, Pool.Default);
            screenQuadVB.SetData(screenQuadVertices, 0, LockFlags.None);

            /*renderTarget2D = new Texture(d3dDevice, d3dDevice.PresentationParameters.BackBufferWidth
                    , d3dDevice.PresentationParameters.BackBufferHeight, 1, Usage.RenderTarget,
                        Format.X8R8G8B8, Pool.Default);*/

            renderTarget2D = new Texture(d3dDevice, d3dDevice.PresentationParameters.BackBufferWidth, d3dDevice.PresentationParameters.BackBufferHeight, 1, Usage.RenderTarget, Format.X8R8G8B8, Pool.Default);
            //Creamos un DepthStencil que debe ser compatible con nuestra definicion de renderTarget2D.
            depthStencil = d3dDevice.CreateDepthStencilSurface(d3dDevice.PresentationParameters.BackBufferWidth, d3dDevice.PresentationParameters.BackBufferHeight, DepthFormat.D24S8, MultiSampleType.None, 0, true);
                        
            //int pantallaWidth = ScreenSizeClass.ScreenSize.Width;
            //int pantallaHeight = ScreenSizeClass.ScreenSize.Height;
            //renderTarget2D = new Texture(d3dDevice, pantallaWidth, pantallaHeight, 1, Usage.RenderTarget, Format.X8R8G8B8, Pool.Default);

            //depthStencil = d3dDevice.CreateDepthStencilSurface(pantallaWidth, pantallaHeight, DepthFormat.D24S8, MultiSampleType.None, 0, true);
            //Cargar shader con efectos de Post-Procesado
            effect = TgcShaders.loadEffect(GuiController.Instance.AlumnoEjemplosDir + "Media\\Shaders\\PostProcess.fx");
            effect.Technique = "BlurTechnique";
            

            intVaivenAlarm = new InterpoladorVaiven();
            intVaivenAlarm.Min = 0;
            intVaivenAlarm.Max = 1;
            intVaivenAlarm.Speed = 2;
            intVaivenAlarm.reset();

            initRedAndBlur();

            //stencilAnterior = d3dDevice.DepthStencilSurface;
            
        }

        public void initRedAndBlur()
        {
            blur_intensity = valorPredefinidoBlur;
            red_intensity = valorPredefinidoRed;
            tiempo = 0;
            signo = 1;
        }

        public override void render(float elapsedTime)
        {
            //calculo del efecto
            tiempo += elapsedTime;
            if (tiempo >= TIEMPO_EJECUCION_EFECTO)
            {
                tiempo = 0;

                blur_intensity += AUMENTO * signo;
                if (blur_intensity > INTENSIDAD_MAX || blur_intensity < INTENSIDAD_MIN)
                {
                    signo *= -1;
                }
            }

            red_intensity += 0.3f*elapsedTime;

            /*meshes.Clear();
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
            }*/

            Device d3dDevice = GuiController.Instance.D3dDevice;

            pOldRT = d3dDevice.GetRenderTarget(0);
            Surface pSurf = renderTarget2D.GetSurfaceLevel(0);
            d3dDevice.SetRenderTarget(0, pSurf);

            Surface pOldDS = d3dDevice.DepthStencilSurface;//agregado
            d3dDevice.DepthStencilSurface = depthStencil;//agregado
            
                
            d3dDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);

            drawSceneToRenderTarget(d3dDevice);

            pSurf.Dispose();

            d3dDevice.DepthStencilSurface = pOldDS;//agregado

            d3dDevice.SetRenderTarget(0, pOldRT);

            drawPostProcess(d3dDevice);

            //d3dDevice.DepthStencilSurface = stencilAnterior;//adasdads
        }

        public override void drawSceneToRenderTarget(Device d3dDevice)
        {
            d3dDevice.BeginScene();
            /*foreach (TgcMesh m in meshes)
            {
                m.render();
            }*/
            Personaje.Instance.configIluminador.renderizarIluminador();
            //AnimatedBoss.Instance.render();
        }


        public override void drawPostProcess(Device d3dDevice)
        {
            d3dDevice.EndScene();
            d3dDevice.BeginScene();

            d3dDevice.VertexFormat = CustomVertex.PositionTextured.Format;
            d3dDevice.SetStreamSource(0, screenQuadVB, 0);

            effect.Technique = "BlurTechnique";

            //Cargamos parametros en el shader de Post-Procesado
            effect.SetValue("render_target2D", renderTarget2D);
            effect.SetValue("blur_intensity", blur_intensity);

            if (Personaje.Instance.fluorActivado())
            {
                effect.SetValue("red_intensity", 0);//Esto es para que se muestre solo blur cuando tenes la bengala
                initRedAndBlur();//esto es para que el tipo se sienta un cacho a salvo
            }
            else
            {
                effect.SetValue("red_intensity", red_intensity);
            }

            d3dDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
            effect.Begin(FX.None);
            effect.BeginPass(0);
            d3dDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            effect.EndPass();
            effect.End();

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

        //public TgcBox cajaNegra;

        /*public void updateFondo()
        {
            cajaNegra.Position = CamaraFPS.Instance.camaraFramework.Position + CamaraFPS.Instance.camaraFramework.viewDir * (renderDistance / 2);
        }*/
    }
}
