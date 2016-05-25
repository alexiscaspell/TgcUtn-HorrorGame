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
        string selectedMesh;
        string selectedAnim;
        TgcSkeletalMesh mesh;
        TgcSkeletalBoneAttach attachment;
        string mediaPath;
        string[] animationsPath;

        public void init()
        {
            crearEsqueleto();
            mesh.Position = new Vector3(50, 0, 100);
            mesh.rotateY(FastMath.ToRad(180));
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

            //Cargar mesh inicial
            selectedAnim = "StandBy";
            changeMesh("CS_Arctic");

        }

        private void changeAnimation(string animation)
        {
            if (selectedAnim != animation)
            {
                selectedAnim = animation;
                mesh.playAnimation(selectedAnim, true);
            }
        }

        private void changeMesh(string meshName)
        {
            if (selectedMesh == null || selectedMesh != meshName)
            {
                if (mesh != null)
                {
                    mesh.dispose();
                    mesh = null;
                }

                selectedMesh = meshName;

                //Cargar mesh y animaciones
                TgcSkeletalLoader loader = new TgcSkeletalLoader();
                mesh = loader.loadMeshAndAnimationsFromFile(mediaPath + selectedMesh + "-TgcSkeletalMesh.xml", mediaPath, animationsPath);

                //Crear esqueleto a modo Debug
                mesh.buildSkletonMesh();

                //Elegir animacion inicial
                mesh.playAnimation(selectedAnim, true);

                //Crear caja como modelo de Attachment del hueos "Bip01 L Hand"
                attachment = new TgcSkeletalBoneAttach();
                TgcBox attachmentBox = TgcBox.fromSize(new Vector3(2, 40, 2), Color.Red);
                attachment.Mesh = attachmentBox.toMesh("attachment");
                attachment.Bone = mesh.getBoneByName("Bip01 L Hand");
                attachment.Offset = Matrix.Translation(3, -15, 0);
                attachment.updateValues();


                //Configurar camara
                GuiController.Instance.RotCamera.targetObject(mesh.BoundingBox);
            }
        }

        public void render()
        {
            //Actualizar animacion
            /* mesh.updateAnimation();

             //Solo malla o esqueleto, depende lo seleccionado
             mesh.RenderSkeleton = renderSkeleton;
             mesh.render();*/

            //Se puede renderizar todo mucho mas simple (sin esqueleto) de la siguiente forma:
            mesh.animateAndRender();
        }
    }
}
