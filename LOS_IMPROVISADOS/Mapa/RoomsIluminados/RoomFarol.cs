using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class RoomFarol : ARoomLuz
    {
        public RoomFarol(Vector3 posicion) : base(posicion)
        {
            TgcSceneLoader loader = new TgcSceneLoader();
            escenaLampara = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\RoomsIluminados\\roomFarol-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\RoomsIluminados\\");

            foreach (TgcMesh mesh in escenaLampara.Meshes)
            {
                mesh.Scale *= 0.5f;
                mesh.Position = this.posicion;
            }

            init();
        }

        public override void render()
        {
            foreach (TgcMesh mesh in meshesRoom)
            {
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor(Color.LightYellow));
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(posicion));
                mesh.Effect.SetValue("lightIntensity", 15f);
                mesh.Effect.SetValue("lightAttenuation", 0.1f);

                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor(Color.Black));
                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor(Color.LightYellow));
                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor(Color.Gray));
                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor(Color.LightYellow));
                mesh.Effect.SetValue("materialSpecularExp", 4f);

                mesh.render();
            }
        }
    }
}