using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils._2D;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA
{
    abstract class APantalla
    {
        public TgcSprite sprite { get; set; }

        abstract public void init();
        abstract public void render();
    }
}
