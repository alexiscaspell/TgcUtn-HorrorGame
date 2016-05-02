using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class Boss
    {
        private TgcScene cuerpo;

        public Boss()
        {
            TgcSceneLoader loader = new TgcSceneLoader();

            cuerpo = loader.loadSceneFromFile(
        GuiController.Instance.AlumnoEjemplosDir + "Media\\boss\\BOSS-TgcScene.xml",
        GuiController.Instance.AlumnoEjemplosDir + "Media\\boss\\");

        }

        public void render()
        {
            cuerpo.renderAll();
        }


    }
}
