using UnityEngine;

public class Venation: MonoBehaviour
{
	VenationAlgorithm venationAlgorithm;
//	VenationRenderer renderer;
	SimpleRenderer renderer;
	bool draw;
	
	public Material glLineMaterial;
	
	public int nrOfSeeds;
	public int maxAuxins;
	public float auxinRadius;
	public float veinNodeRadius;
	public float killRadius;
	public float neighborhoodRadius;

	public static int startFrame;
    
	// this boots the app, which creates the JsonRPC server,
	// which in turn creates the Scene instances (one for every browser session)
	void Start()
	{
//		glLineMaterial = Resources.Load ( "GlLineMat", typeof ( Material ) ) as Material;
		draw = false;
    }
	
	void RandomSeeds()
	{
		venationAlgorithm = new VenationAlgorithm ( nrOfSeeds, maxAuxins, auxinRadius, veinNodeRadius, killRadius, neighborhoodRadius );
		//	renderer = new VenationRenderer ( va, this.g, width );
		renderer = new SimpleRenderer ( venationAlgorithm, Screen.height );
		startFrame = Time.frameCount;
        draw = true;
	}

	void MouseSeed()
	{
		venationAlgorithm = new VenationAlgorithm ( maxAuxins, auxinRadius, veinNodeRadius, killRadius, neighborhoodRadius );
		//	renderer = new VenationRenderer ( va, this.g, width );
		renderer = new SimpleRenderer ( venationAlgorithm, Screen.height );
		startFrame = Time.frameCount;
        draw = true;
    }
    
    void Update ()
	{
		if ( Input.GetKeyDown ( KeyCode.Space ) )
			RandomSeeds ();

		if ( Input.GetKeyDown ( KeyCode.Mouse0 ) )
			MouseSeed ();

		if ( draw )
			venationAlgorithm.step();
    }
    
    public void OnPostRender()
    {
		if ( !draw ) return;

		renderer.draw ( glLineMaterial );
	}
	
}