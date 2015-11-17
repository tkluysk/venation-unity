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
//		size ( 800, 800 );
		
		glLineMaterial = Resources.Load ( "GlLineMat", typeof ( Material ) ) as Material;

        Reset();
		draw = true;
    }
    
	void Start ()
	{
	}
	
	void Reset()
	{
		va = new VenationAlgorithm();
		//	renderer = new VenationRenderer ( va, this.g, width );
		renderer = new SimpleRenderer ( va, Screen.height );
	}
	
	void Update ()
	{
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