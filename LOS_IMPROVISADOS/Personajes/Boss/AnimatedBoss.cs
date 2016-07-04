using AlumnoEjemplos.LOS_IMPROVISADOS.Personajes.Boss;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TgcViewer;
using TgcViewer.Utils._2D;
using TgcViewer.Utils.Sound;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcSkeletalAnimation;

namespace AlumnoEjemplos.LOS_IMPROVISADOS
{
    class AnimatedBoss
    {
        #region Singleton
        private static volatile AnimatedBoss instancia = null;

        public static AnimatedBoss Instance
        {
            get
            {
                if (instancia == null) instancia = new AnimatedBoss();

                return instancia;
            }
        }

        #endregion

        #region variablesAnimadas
        private string selectedMesh;
        private string selectedAnim;
        private TgcSkeletalBoneAttach attachment;
        private string mediaPath;
        private string[] animationsPath;
        #endregion
        private TgcSkeletalMesh cuerpo;
        private float velocidadMovimiento;
        private Vector3 direccionVista;
        private CamaraFPS camara;
        public Comportamiento comportamiento;
        internal bool activado = true;
        private state estado;
        private state estadoAnterior;
        private float tiempoPasadoAturdido = 0;
        private float tiempoDeRecuperacion = 4;
        private TgcStaticSound aturdido = new TgcStaticSound();
        private TgcStaticSound respiracion = new TgcStaticSound();
        private float timerRespiracion;

        private const float aumentoVelocidad = 1.2f;//Se va hardcodeando
        private float velocidadNormal;
 

        enum state { PASEANDO,PERSIGUIENDO,ATURDIDO};

        private float contadorParaTeletransporte = 0f;
        private float TIEMPO_PARA_TELETRANSPORTAR_AL_BOSS = 60f;
        private float PORCION_DE_PUNTOS_QUE_ELIMINO_CUANDO_EL_BOSS_SE_TELETRANSPORTA = 1.6f;

        private AnimatedBoss()
        {
            crearEsqueleto();
            camara = CamaraFPS.Instance;
            selectedAnim = "Walk";//String con la animacion seleccionada
            changeMesh("CS_Arctic");//String con el mesh seleccionado (Basic Human tiene varios)
            cuerpo.AutoTransformEnable = true;
            cuerpo.AutoUpdateBoundingBox = true;
            estado = state.PASEANDO;
            comportamiento = new ComportamientoRandom();
            aturdido.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\zo_pain1.wav");
            respiracion.loadSound(GuiController.Instance.AlumnoEjemplosDir + "Media\\Sonidos\\slower_alert10.wav");  
        } 

        public void init(float velocidadMovimiento, Vector3 posicion)
        {
            cuerpo.Position = posicion;
            cuerpo.Scale = new Vector3(10, 10, 10);
            this.velocidadMovimiento = velocidadMovimiento;
            velocidadNormal = velocidadMovimiento;
            direccionVista = new Vector3(0, 0, -1);
        }

        #region funcionesAnimadas
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
        #endregion

        public void render()
        {
            cuerpo.animateAndRender();
        }

        private void seguirPersonaje()
        {
            float elapsedTime = GuiController.Instance.ElapsedTime;

            Vector3 movement = comportamiento.proximoPunto(cuerpo.Position);
            movement -= cuerpo.Position;
            movement.Normalize();

            if (movement.X!=0||movement.Z!=0)
            {
                girarVistaAPunto(movement);
            }

            movement *= velocidadMovimiento * elapsedTime;

            cuerpo.move(movement);
        }

        private void girarVistaAPunto(Vector3 pointToView)
        {
            float angulo = FastMath.Acos(Vector3.Dot(pointToView, direccionVista));

            Vector3 normal = Vector3.Cross(direccionVista, pointToView);

            direccionVista = pointToView;

            if (!float.IsNaN(angulo))
            {
                if (normal.Y > 0)
                {
                    cuerpo.rotateY(angulo);
                }
                else
                    cuerpo.rotateY(-angulo);
            }
        }

        internal TgcBoundingBox getBoundingBox()
        {
            return cuerpo.BoundingBox;
        }

        public void update()
        {
            if (activado)
            {
                updateEstado();
                updateVelocity();
                updateComportamiento();
                seguirPersonaje();
            }
        }

        private void updateVelocity()
        {
            if (estado==state.PERSIGUIENDO)
            {
                velocidadMovimiento = aumentoVelocidad * velocidadNormal;
            }
            else
            {
                velocidadMovimiento = velocidadNormal;
            }
        }

        private void updateEstado()
        {
            if (estado == state.ATURDIDO)
            {
                updateEstadoAturdido();
                return;
            }
            else
            {
                estadoAnterior = estado;
            }

            Personaje pj = Personaje.Instance;

            ColinaAzul colina = ColinaAzul.Instance;

            bool estoyConPj = colina.estoyEn(colina.dondeEstaPesonaje(), cuerpo.Position);

            if (pj.iluminadorEncendido() && estoyConPj)
            {
                estado = state.PERSIGUIENDO;
            }

            if (estado == state.PERSIGUIENDO)
            {
                if (pjEscondido(pj))
                {
                    estado = state.PASEANDO;
                }
            }

            if (pj.fluorActivado() && estoyCercaDePj())
            {
                estado = state.ATURDIDO;
                aturdido.play();
                changeAnimation("Talk");
            }
        }

        private bool estoyCercaDePj()
        {
            //TgcBoundingSphere cuerpoTrucho = new TgcBoundingSphere(Personaje.Instance.cuerpo.Center, 300);

            return (CamaraFPS.Instance.camaraFramework.Position - cuerpo.Position).Length() < 1000;//ColinaAzul.Instance.colisionaEsferaCaja(cuerpoTrucho, cuerpo.BoundingBox);
        }

        private bool pjEscondido(Personaje pj)
        {
            return pj.iluminadorEncendido() && pj.agachado() && pjTapadoPorObjeto();
        }

        private bool pjTapadoPorObjeto()
        {
            Vector3 vectorInutil;
            Vector3 posInicial = cuerpo.Position;
            posInicial.Y = cuerpo.BoundingBox.PMax.Y;
            Vector3 posFinal = CamaraFPS.Instance.camaraFramework.Position;

            foreach (TgcMesh mesh in Mapa.Instance.objetosDeCuarto(ColinaAzul.Instance.dondeEstaPesonaje()))
            {
                bool resultado = TgcCollisionUtils.intersectSegmentAABB(posInicial, posFinal, mesh.BoundingBox, out vectorInutil);
                if (resultado)
                {
                    return true;
                }
            }

            return false;

            //TgcBoundingSphere cuerpoTrucho = new TgcBoundingSphere(Personaje.Instance.cuerpo.Center,150f);
            //TgcBoundingBox b = new TgcBoundingBox();
            //return Mapa.Instance.colisionaPersonajeConAlgunObjeto(cuerpoTrucho, ref b);
        }

        private void updateEstadoAturdido()
        {
            tiempoPasadoAturdido += GuiController.Instance.ElapsedTime;

            if (tiempoPasadoAturdido > tiempoDeRecuperacion)
            {
                estado = estadoAnterior;
                tiempoPasadoAturdido = 0;
                changeAnimation("Walk");
                estadoAnterior = state.ATURDIDO;
            }
        }

        private void updateComportamiento()
        {
            if (estado == estadoAnterior)
            {
                if (estado == state.PASEANDO)
                {
                    contadorParaTeletransporte += GuiController.Instance.ElapsedTime;
                    if (contadorParaTeletransporte > TIEMPO_PARA_TELETRANSPORTAR_AL_BOSS)
                    {
                        contadorParaTeletransporte = 0;
                        teletransportarAlBossAUnaPosicionPasadaPorElPersonaje();
                        comportamiento = new ComportamientoRandom();
                    }
                }
                    return;
            }

            if (estado == state.PASEANDO)
            {
                comportamiento = new ComportamientoRandom();
            }
            else if (estado == state.PERSIGUIENDO)
            {
                comportamiento = new SeguirPersonaje();//ComportamientoSeguir(cuerpo.Position);
            }
            else if (estado == state.ATURDIDO)
            {
                comportamiento = new Aturdido();
            }

        }

        public void dispose()
        {
            cuerpo.dispose();
        }
        
        public void teletransportarAlBossAUnaPosicionPasadaPorElPersonaje()
        {
            float div = /*DiosMapa.Instance.*/PORCION_DE_PUNTOS_QUE_ELIMINO_CUANDO_EL_BOSS_SE_TELETRANSPORTA;

            int cantidad = Convert.ToInt32((DiosMapa.Instance.cantidadDeElementosDeListaPersecucion() / div));
            DiosMapa.Instance.elminarPrimerosPuntosDePersecucion(cantidad);

            Punto nuevoPunto = DiosMapa.Instance.puntoASeguirPorElBoss();

            respiracion.play();

            cuerpo.Position = nuevoPunto.getPosition();
        }

        public Vector3 getPosition()
        {
            return cuerpo.Position;
        }
    }
}
