using UnityEngine;

public class Venation: MonoBehaviour
{
	VenationAlgorithm va;
//	VenationRenderer renderer;
	SimpleRenderer renderer;
	bool draw;
	
	public Material glLineMaterial;
    
	// this boots the app, which creates the JsonRPC server,
	// which in turn creates the Scene instances (one for every browser session)
	void Awake()
	{
//		glLineMaterial = Resources.Load ( "GlLineMat", typeof ( Material ) ) as Material;
        Reset();
    }
    
	void Reset()
	{
		va = new VenationAlgorithm();
		//	renderer = new VenationRenderer ( va, this.g, width );
		renderer = new SimpleRenderer ( va, Screen.height );
		draw = true;
	}
	
	void Update ()
	{
		draw = true;
//		switch ( key ) {
//		case 'e':
//			reset();
//			draw = true;
//			break;
//			
//		case ' ':
//			va.step();
//			draw = true;
//			break;
//			
//		case 'r':
//			save ( "render.png" );
//            break;
//        }
    }
    
    public void OnPostRender()
    {
		if ( !draw ) return;

//        background ( 255 );
		renderer.draw ( glLineMaterial );
        
        draw = false;
	}
	
}