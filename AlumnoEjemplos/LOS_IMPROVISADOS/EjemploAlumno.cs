using TgcViewer.Example;
using TgcViewer;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.Input;

using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using TgcViewer.Utils.Shaders;
using System.Drawing;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.MiGrupo
{
    public class EjemploAlumno : TgcExample
    {
        /////VARIABLES GLOBALES/////
        private TgcScene tgcEscena;
                
        /////CAJA DE LUZ/////
        private TgcBox cajaDeLuz;

        public override string getCategory()
        {
            return "AlumnoEjemplos";
        }
        
        public override string getName()
        {
            return "Los Improvisados";
        }
        
        public override string getDescription()
        {
            return "Juego de Terror - Juego de terror en primera persona basado en juegos famosos como Amnesia, Outlast, Penumbra, etc";
        }
        



        public override void init()
        {
            Device d3dDevice = GuiController.Instance.D3dDevice;

            ///////////////MODIFIERS//////////////////
            GuiController.Instance.Modifiers.addFloat("velocidadCaminar", 0f, 400f, 100f);

            ///////////////USERVARS//////////////////
            GuiController.Instance.UserVars.addVar("camaraX");
            GuiController.Instance.UserVars.addVar("camaraY");
            GuiController.Instance.UserVars.addVar("camaraZ");

            GuiController.Instance.UserVars.addVar("vistaX");
            GuiController.Instance.UserVars.addVar("vistaY");
            GuiController.Instance.UserVars.addVar("vistaZ");

            ///////////////CONFIGURAR CAMARA PRIMERA PERSONA///////////////
            GuiController.Instance.FpsCamera.Enable = true;
            GuiController.Instance.FpsCamera.setCamera(new Vector3(280,25,95), new Vector3(180,25,95));
            GuiController.Instance.FpsCamera.JumpSpeed = 200f;


            //////////////////CARGAR MAPA//////////////////
            TgcSceneLoader loader = new TgcSceneLoader();
            tgcEscena = loader.loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosDir + "Media\\habitacionMiedo\\habitacionMiedo-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosDir + "Media\\habitacionMiedo\\");


            

            //////////////////////////////////////////////
            /*****************CAJA DE LUZ***************/
            ////////////////////////////////////////////         
            cajaDeLuz = TgcBox.fromSize(new Vector3(10, 10, 10), Color.Red);
            cajaDeLuz.Position = GuiController.Instance.FpsCamera.Position;

            //Modifiers de la luz
            GuiController.Instance.Modifiers.addBoolean("lightEnable", "lightEnable", true);
            GuiController.Instance.Modifiers.addVertex3f("lightPos", new Vector3(-200, -100, -200), new Vector3(200, 200, 300), new Vector3(-60, 90, 175));
            GuiController.Instance.Modifiers.addVertex3f("lightDir", new Vector3(-1, -1, -1), new Vector3(1, 1, 1), new Vector3(-1, -0.1f, 0));
            GuiController.Instance.Modifiers.addColor("lightColor", Color.White);
            GuiController.Instance.Modifiers.addFloat("lightIntensity", 0, 150, 9f);
            GuiController.Instance.Modifiers.addFloat("lightAttenuation", 0.1f, 2, 0.34f);
            GuiController.Instance.Modifiers.addFloat("specularEx", 0, 20, 5f);
            GuiController.Instance.Modifiers.addFloat("spotAngle", 0, 180, 51f);
            GuiController.Instance.Modifiers.addFloat("spotExponent", 0, 20, 5f);

            //Modifiers de material
            GuiController.Instance.Modifiers.addColor("mEmissive", Color.Black);
            GuiController.Instance.Modifiers.addColor("mAmbient", Color.White);
            GuiController.Instance.Modifiers.addColor("mDiffuse", Color.White);
            GuiController.Instance.Modifiers.addColor("mSpecular", Color.White);


        }








        public override void render(float elapsedTime)
        {
            TgcD3dInput d3dInput = GuiController.Instance.D3dInput;
            Device d3dDevice = GuiController.Instance.D3dDevice;

            //////////////////////////////////////////////
            /*****************BOTONES*******************/
            //////////////////////////////////////////// 

            if (GuiController.Instance.D3dInput.keyPressed(Microsoft.DirectX.DirectInput.Key.F))
            {
                cajaDeLuz.Position = GuiController.Instance.FpsCamera.Position;
            }

            //////////////////USO VALORES DE LOS MODIFICADORES//////////////////
            GuiController.Instance.FpsCamera.MovementSpeed = (float)GuiController.Instance.Modifiers["velocidadCaminar"];

            //////////////////MUESTRO VARIABLES USER//////////////////
            GuiController.Instance.UserVars.setValue("camaraX", GuiController.Instance.FpsCamera.getPosition().X);
            GuiController.Instance.UserVars.setValue("camaraY", GuiController.Instance.FpsCamera.getPosition().Y);
            GuiController.Instance.UserVars.setValue("camaraZ", GuiController.Instance.FpsCamera.getPosition().Z);

            GuiController.Instance.UserVars.setValue("vistaX", GuiController.Instance.FpsCamera.getLookAt().X);
            GuiController.Instance.UserVars.setValue("vistaY", GuiController.Instance.FpsCamera.getLookAt().Y);
            GuiController.Instance.UserVars.setValue("vistaZ", GuiController.Instance.FpsCamera.getLookAt().Z);



            


            //////////////////////////////////////////////
            /*****************CAJA DE LUZ***************/
            //////////////////////////////////////////// 
            //Habilitar luz
            bool lightEnable = (bool)GuiController.Instance.Modifiers["lightEnable"];
            Effect currentShader;
            if (lightEnable)
            {
                //Con luz: Cambiar el shader actual por el shader default que trae el framework para iluminacion dinamica con SpotLight
                currentShader = GuiController.Instance.Shaders.TgcMeshSpotLightShader;
            }
            else
            {
                //Sin luz: Restaurar shader default
                currentShader = GuiController.Instance.Shaders.TgcMeshShader;
            }

            //Aplicar a cada mesh el shader actual
            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                mesh.Effect = currentShader;
                //El Technique depende del tipo RenderType del mesh
                mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);
            }


            //Actualzar posición de la luz
            //Vector3 lightPos = (Vector3)GuiController.Instance.Modifiers["lightPos"];
            Vector3 lightPos = cajaDeLuz.Position;
            //cajaDeLuz.Position = lightPos;
            cajaDeLuz.Position = GuiController.Instance.FpsCamera.Position;

            //Normalizar direccion de la luz
            Vector3 lightDir = (Vector3)GuiController.Instance.Modifiers["lightDir"];
            lightDir.Normalize();
            //Vector3 lightDir = GuiController.Instance.FpsCamera.LookAt;

            //Renderizar meshes
            foreach (TgcMesh mesh in tgcEscena.Meshes)
            {
                if (lightEnable)
                {
                    //Cargar variables shader de la luz
                    mesh.Effect.SetValue("lightColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["lightColor"]));
                    mesh.Effect.SetValue("lightPosition", TgcParserUtils.vector3ToFloat4Array(lightPos));
                    mesh.Effect.SetValue("eyePosition", TgcParserUtils.vector3ToFloat4Array(GuiController.Instance.FpsCamera.getPosition()));
                    mesh.Effect.SetValue("spotLightDir", TgcParserUtils.vector3ToFloat3Array(lightDir));
                    mesh.Effect.SetValue("lightIntensity", (float)GuiController.Instance.Modifiers["lightIntensity"]);
                    mesh.Effect.SetValue("lightAttenuation", (float)GuiController.Instance.Modifiers["lightAttenuation"]);
                    mesh.Effect.SetValue("spotLightAngleCos", FastMath.ToRad((float)GuiController.Instance.Modifiers["spotAngle"]));
                    mesh.Effect.SetValue("spotLightExponent", (float)GuiController.Instance.Modifiers["spotExponent"]);

                    //Cargar variables de shader de Material. El Material en realidad deberia ser propio de cada mesh. Pero en este ejemplo se simplifica con uno comun para todos
                    mesh.Effect.SetValue("materialEmissiveColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["mEmissive"]));
                    mesh.Effect.SetValue("materialAmbientColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["mAmbient"]));
                    mesh.Effect.SetValue("materialDiffuseColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["mDiffuse"]));
                    mesh.Effect.SetValue("materialSpecularColor", ColorValue.FromColor((Color)GuiController.Instance.Modifiers["mSpecular"]));
                    mesh.Effect.SetValue("materialSpecularExp", (float)GuiController.Instance.Modifiers["specularEx"]);
                }

                //Renderizar modelo
                mesh.render();
            }


            //Renderizar mesh de luz, en realidad no se tendria que ver
            //cajaDeLuz.render();



            //////////////////MUESTRO LOS OBJETOS//////////////////
            //tgcEscena.renderAll();
                        
        }





        public override void close()
        {
            tgcEscena.disposeAll();
            cajaDeLuz.dispose();
        }

    }
}
