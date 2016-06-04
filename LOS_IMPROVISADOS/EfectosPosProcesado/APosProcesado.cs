using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.Interpolation;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.EfectosPosProcesado
{
    public abstract class APosProcesado
    {
        public Mapa mapa { get; set; }

        public List<TgcMesh> meshes;
        public VertexBuffer screenQuadVB;
        public Texture renderTarget2D;
        public Surface pOldRT;
        public Effect effect;
        public TgcTexture alarmTexture;
        public InterpoladorVaiven intVaivenAlarm;
        

        public APosProcesado(Mapa mapa)
        {
            this.mapa = mapa;
        }

        public abstract void init();
        public abstract void render(float elapsedTime);
        public abstract void drawSceneToRenderTarget(Device d3dDevice);
        public abstract void drawPostProcess(Device d3dDevice);
        public abstract void close();

    }
}
