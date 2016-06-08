using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using System.Drawing;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class RoomLinterna : ARoomLuz
    {
        public Vector3 direccionVista { get; set; }

        public RoomLinterna(Vector3 posicion, Vector3 direccionVista) : base()
        {
            this.posicion = posicion;
            this.direccionVista = direccionVista;

            TgcSceneLoader loader = new TgcSceneLoader();
            escenaLampara = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\RoomsIluminados\\roomLinterna-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\Objetos\\RoomsIluminados\\");

            foreach (TgcMesh mesh in escenaLampara.Meshes)
            {
                mesh.Position = this.posicion;
            }

            init();
        }

        public override void render()
        {
            foreach (TgcMesh mesh in meshesRoom)
            {
                mesh.Effect.SetValue("lightColor", ColorValue.FromColor(Color.White));
                mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(posicion));
                mesh.Effect.SetValue("eyePosition", TgcParserUtils.vector3ToFloat4Array(posicion));
                mesh.Effect.SetValue("spotLightDir", TgcParserUtils.vector3ToFloat3Array(direccionVista));
                mesh.Effect.SetValue("lightIntensity", 150f);
                mesh.Effect.SetValue("lightAttenuation", 0.4f);
                mesh.Effect.SetValue("spotLightAngleCos", 90f);
                mesh.Effect.SetValue("spotLightExponent", 0f);

                mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor(Color.Black));
                mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor(Color.White));
                mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor(Color.White));
                mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor(Color.LightGray));
                mesh.Effect.SetValue("materialSpecularExp", 18f);

                mesh.render();
            }
        }
    }
}