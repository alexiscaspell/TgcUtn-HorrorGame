using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSkeletalAnimation;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class AnimatedBoss
    {
        private string selectedMesh;
        private string selectedAnim;
        private TgcSkeletalBoneAttach attachment;
        private string mediaPath;
        private string[] animationsPath;
        private TgcSkeletalMesh cuerpo;
        private float velocidadMovimiento;
        private Vector3 direccionVista;
        CamaraFPS camara;

        public AnimatedBoss()
        {
            crearEsqueleto();
            camara = CamaraFPS.Instance;
            selectedAnim = "Walk";//String con la animacion seleccionada
            changeMesh("CS_Arctic");//String con el mesh seleccionado (Basic Human tiene varios)
            cuerpo.AutoTransformEnable = true;
            cuerpo.AutoUpdateBoundingBox = true;
        } 

        public void init(float velocidadMovimiento, Vector3 posicion)
        {
            cuerpo.Position = posicion;
            cuerpo.Scale = new Vector3(1.2f, 1f, 1.2f);
            this.velocidadMovimiento = velocidadMovimiento;
            direccionVista = new Vector3(0, 0, -1);
    }

        private void crearEsqueleto()
        {
            //Path para carpeta de texturas de la malla
            mediaPath = GuiController.Instance.AlumnoEjemplosDir + "Media\\SkeletalAnimations\\BasicHuman\\";

            //Cargar dinamicamente todos los Mesh animados que haya en el directorio
            DirectoryInfo dir = new DirectoryInfo(mediaPath);
            FileInfo[] meshFiles = dir.GetFiles("*-TgcSkeletalMesh.xml", SearchOption.TopDirectoryOnly);
            string[] meshList = new string[meshFiles.Length];
            for (int i = 0; i < meshFiles.Length; i++)
            {
                string name = meshFiles[i].Name.Replace("-TgcSkeletalMesh.xml", "");
                meshList[i] = name;
            }

            //Cargar dinamicamente todas las animaciones que haya en el directorio "Animations"
            DirectoryInfo dirAnim = new DirectoryInfo(mediaPath + "Animations\\");
            FileInfo[] animFiles = dirAnim.GetFiles("*-TgcSkeletalAnim.xml", SearchOption.TopDirectoryOnly);
            string[] animationList = new string[animFiles.Length];
            animationsPath = new string[animFiles.Length];
            for (int i = 0; i < animFiles.Length; i++)
            {
                string name = animFiles[i].Name.Replace("-TgcSkeletalAnim.xml", "");
                animationList[i] = name;
                animationsPath[i] = animFiles[i].FullName;
            }

        }

        private void changeAnimation(string animation)
        {
            if (selectedAnim != animation)
            {
                selectedAnim = animation;
                cuerpo.playAnimation(selectedAnim, true);
            }
        }

        private void changeMesh(string meshName)
        {
            if (selectedMesh == null || selectedMesh != meshName)
            {
                if (cuerpo != null)
                {
                    cuerpo.dispose();
                    cuerpo = null;
                }

                selectedMesh = meshName;

                //Cargar mesh y animaciones
                TgcSkeletalLoader loader = new TgcSkeletalLoader();
                cuerpo = loader.loadMeshAndAnimationsFromFile(mediaPath + selectedMesh + "-TgcSkeletalMesh.xml", mediaPath, animationsPath);

                //Crear esqueleto a modo Debug
                cuerpo.buildSkletonMesh();

                //Elegir animacion inicial
                cuerpo.playAnimation(selectedAnim, true);

                //Crear caja como modelo de Attachment del hueos "Bip01 L Hand"
                attachment = new TgcSkeletalBoneAttach();
                TgcBox attachmentBox = TgcBox.fromSize(new Vector3(2, 40, 2), Color.Red);
                attachment.Mesh = attachmentBox.toMesh("attachment");
                attachment.Bone = cuerpo.getBoneByName("Bip01 L Hand");
                attachment.Offset = Matrix.Translation(3, -15, 0);
                attachment.updateValues();

                //Configurar camara
                //GuiController.Instance.RotCamera.targetObject(mesh.BoundingBox);
            }
        }

        public void render()
        {
            cuerpo.animateAndRender();
        }

        private void seguirPersonaje()
        {
            float elapsedTime = GuiController.Instance.ElapsedTime;

            Vector3 movement = camara.posicion;

                movement.Subtract(cuerpo.Position);
                movement.Subtract(new Vector3(0, movement.Y, 0));
                movement.Normalize();

                float angulo = FastMath.Acos(Vector3.Dot(movement, direccionVista));

                Vector3 normal = Vector3.Cross(direccionVista, movement);

                direccionVista = movement;

                if (!float.IsNaN(angulo))
                {
                    if (normal.Y > 0)
                    {
                        cuerpo.rotateY(angulo);
                    }
                    else
                        cuerpo.rotateY(-angulo);
                }

                movement *= velocidadMovimiento * elapsedTime;
                cuerpo.move(movement);
        }

        public void update()
        {
            seguirPersonaje();
        }

        public void dispose()
        {
            cuerpo.dispose();
        }
    }
}
