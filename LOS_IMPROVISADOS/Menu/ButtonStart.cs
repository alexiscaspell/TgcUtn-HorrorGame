using AlumnoEjemplos.MiGrupo;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Menu
{
    class ButtonStart : GameButton
    {
        public void init()
        {
            base.init("botonStart3",new Vector2(7.8f,1.6f));
        }

        public override void execute(EjemploAlumno app, GameMenu menu)
        {
            app.playing = true;
        }
    }
}
