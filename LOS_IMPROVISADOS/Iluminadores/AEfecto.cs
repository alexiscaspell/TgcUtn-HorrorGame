using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores
{
    abstract class AEfecto
    {
        public TgcScene tgcEscena { set; get; }
        public CamaraFPS camaraFPS { get; set; }

        abstract public void init();
        abstract public void render();
    }
}
