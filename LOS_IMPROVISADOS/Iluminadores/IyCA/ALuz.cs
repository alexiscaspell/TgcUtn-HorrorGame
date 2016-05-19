using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA
{
    abstract class ALuz
    {

        public TgcScene tgcEscena { set; get; }
        public CamaraFPS camaraFPS { get; set; }
        
        public ALuz(TgcScene tgcEscena, CamaraFPS camaraFPS)
        {
            this.tgcEscena = tgcEscena;
            this.camaraFPS = camaraFPS;
        }

        abstract public void init();
        abstract public void render();
    }
}
