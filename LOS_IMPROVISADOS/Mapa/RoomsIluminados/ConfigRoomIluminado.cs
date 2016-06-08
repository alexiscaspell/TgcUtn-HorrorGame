using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class ConfigRoomIluminado
    {
        public List<ARoomLuz> roomsIluminados { get; set; }

        public ConfigRoomIluminado()
        {
            //tendria que crear varios objetos, esto es solo para probar
            RoomFarol r9 = new RoomFarol(new Vector3(3298, 26, 2108));

            roomsIluminados = new List<ARoomLuz> {r9};
        }
        
        public void init()
        {/*
        	foreach(ARoomLuz luz in roomsIluminados){
        		luz.init();
        	}*/
        }

        public void render()
        {
            foreach (ARoomLuz roomLuz in roomsIluminados)
            {
                roomLuz.render();
            }
        }
    }
}
