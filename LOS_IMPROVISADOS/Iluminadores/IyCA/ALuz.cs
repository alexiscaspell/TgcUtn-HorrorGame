using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA
{
    abstract class ALuz
    {

        public Mapa mapa { set; get; }
        public CamaraFPS camaraFPS { get; set; }
        
        public ALuz(Mapa mapa, CamaraFPS camaraFPS)
        {
            this.mapa = mapa;
            this.camaraFPS = camaraFPS;
        }

        abstract public void init();
        abstract public void render();
    }
}
