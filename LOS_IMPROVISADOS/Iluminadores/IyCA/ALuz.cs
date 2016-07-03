using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using TgcViewer;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcGeometry;

namespace AlumnoEjemplos.LOS_IMPROVISADOS.Iluminadores.IyCA
{
    abstract class ALuz
    {
    	protected Inventario inv;
    	
        protected TgcBox cajaNegra;
        protected TgcSphere esferaNegra;
        protected const float renderDistance = 5000;
    
        public Mapa mapa { set; get; }
        public CamaraFPS camaraFPS { get; set; }
        
        protected Effect currentShader;

        public void updateFondo()
        {
        	//esferaNegra.Position = camaraFPS.camaraFramework.Position;
        	cajaNegra.Position =  camaraFPS.camaraFramework.Position + camaraFPS.camaraFramework.viewDir * (renderDistance / 2);
        }
        
        virtual public void render()
        {
            inv.render();
            
            //Dibujo el fondo para evitar el azul
            updateFondo();
            //cajaNegra.render();
            //esferaNegra.render();
            
            foreach (TgcMesh mesh in mapa.escena.Meshes)
	        {
				if( TgcCollisionUtils.testAABBAABB(mesh.BoundingBox, cajaNegra.BoundingBox))
				{
					mesh.Effect = currentShader;
                	mesh.Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(mesh.RenderType);

                	configurarEfecto(mesh);
                	
				   	mesh.render();
                }
        	}
            foreach(Accionable a in mapa.objetos)
            {
            	if( TgcCollisionUtils.testAABBAABB(a.getMesh().BoundingBox, cajaNegra.BoundingBox))
				{
            		a.getMesh().Effect = currentShader;
            		a.getMesh().Technique = GuiController.Instance.Shaders.getTgcMeshTechnique(a.getMesh().RenderType);

            		configurarEfecto(a.getMesh() );
                	
				   	a.render();
                }
            }
            
    	}
        
        virtual public void init()
        {
        	configInicial();
        	
        	//inicio inv
        	inv = Inventario.Instance;
            
			//Inicio el fondoNegro
			TgcTexture texturaFondo = TgcTexture.createTexture(GuiController.Instance.AlumnoEjemplosDir +
			                                                   "Media\\mapa\\fondoNegro.png");
            
			//esferaNegra = new TgcSphere(renderDistance, texturaFondo, camaraFPS.camaraFramework.Position);
			cajaNegra = TgcBox.fromSize(new Vector3(renderDistance,renderDistance,renderDistance), texturaFondo);
        }
        abstract public void configInicial();
        abstract public void configurarEfecto(TgcMesh mesh);
    }
}
