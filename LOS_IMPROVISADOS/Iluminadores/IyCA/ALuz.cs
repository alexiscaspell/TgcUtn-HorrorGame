using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA
{
    abstract class ALuz
    {

        public List<TgcMesh> meshes { set; get; }
        public CamaraFPS camaraFPS { get; set; }
        
        public ALuz(List<TgcMesh> meshes, CamaraFPS camaraFPS)
        {
            this.meshes = meshes;
            this.camaraFPS = camaraFPS;
        }

        abstract public void init();
        abstract public void render();
    }
}
