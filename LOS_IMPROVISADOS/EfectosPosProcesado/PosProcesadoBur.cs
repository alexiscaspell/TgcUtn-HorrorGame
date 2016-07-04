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

        private float blur_intensity = 0.01f;
        private float tiempo = 0f;
        private float signo = 1f;

        protected const float TIEMPO_EJECUCION_EFECTO = 0.5f;
        protected const float INTENSIDAD_MAX = 0.025f;
        protected const float INTENSIDAD_MIN = 0.001f;
        protected const float AUMENTO = 0.001f;


        public PosProcesadoBur(Mapa mapa) : base(mapa)
        {
            init();
        }

        public override void init()
        {
            //Inicio el fondoNegro
            TgcTexture texturaFondo = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +"Media\\mapa\\fondoNegro.png");
            cajaNegra = TgcBox.fromSize(new Vector3(renderDistance, renderDistance, renderDistance), texturaFondo);
            meshes = new List<TgcMesh>();

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

            renderTarget2D = new Texture(d3dDevice, d3dDevice.PresentationParameters.BackBufferWidth
                    , d3dDevice.PresentationParameters.BackBufferHeight, 1, Usage.RenderTarget,
                        Format.X8R8G8B8, Pool.Default);


            //Cargar shader con efectos de Post-Procesado
            effect = TgcShaders.loadEffect(GuiController.Instance.AlumnoEjemplosDir + "\\LOS_IMPROVISADOS\\Shaders\\Blur.fx");
            effect.Technique = "BlurTechnique";
            

            intVaivenAlarm = new InterpoladorVaiven();
            intVaivenAlarm.Min = 0;
            intVaivenAlarm.Max = 1;
            intVaivenAlarm.Speed = 2;
            intVaivenAlarm.reset();
            
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

            pOldRT = d3dDevice.GetRenderTarget(0);
            Surface pSurf = renderTarget2D.GetSurfaceLevel(0);
            d3dDevice.SetRenderTarget(0, pSurf);
            d3dDevice.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);


            drawSceneToRenderTarget(d3dDevice);

            pSurf.Dispose();

            d3dDevice.SetRenderTarget(0, pOldRT);
            
            drawPostProcess(d3dDevice);
        }

        public override void drawSceneToRenderTarget(Device d3dDevice)
        {
            d3dDevice.BeginScene();
            foreach (TgcMesh m in meshes)
            {
                m.render();
            }
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

        public TgcBox cajaNegra;

        public void updateFondo()
        {
            cajaNegra.Position = CamaraFPS.Instance.camaraFramework.Position + CamaraFPS.Instance.camaraFramework.viewDir * (renderDistance / 2);
        }
    }
}
