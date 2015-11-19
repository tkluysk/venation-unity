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
	public float veinThickness;
	public Camera camera;
	public Color[] backgroundColors;
	public Color[] veinColors;
	Color veinColor;
	public GameObject background;
	public Material material;
	public Texture2D texture;

	public static int startFrame;
    
	// this boots the app, which creates the JsonRPC server,
	// which in turn creates the Scene instances (one for every browser session)
	void Start()
	{
//		glLineMaterial = Resources.Load ( "GlLineMat", typeof ( Material ) ) as Material;
		draw = false;
		material.SetTexture ( "_EmissionMap", texture );
    }

	void Reset()
	{
		renderer = new SimpleRenderer ( this, venationAlgorithm );
		startFrame = Time.frameCount;
		int colorIndex = Random.Range(0,4);
		camera.backgroundColor = backgroundColors[colorIndex];
		var tmpColor = backgroundColors[colorIndex];
		material.color = new Color ( tmpColor.r, tmpColor.g, tmpColor.b, 0.1f );
		veinColor = veinColors[colorIndex];
		background.GetComponent<Renderer>().material = material;
		draw = true;
    }
    
    void RandomSeeds()
	{
		venationAlgorithm = new VenationAlgorithm ( nrOfSeeds, maxAuxins, auxinRadius, veinNodeRadius, killRadius, neighborhoodRadius );
		Reset ();
	}
	
	void MouseSeed()
	{
		venationAlgorithm = new VenationAlgorithm ( maxAuxins, auxinRadius, veinNodeRadius, killRadius, neighborhoodRadius );
		Reset ();
	}

	void MouseSeedWithTexture()
	{
		venationAlgorithm = new VenationAlgorithm ( texture, maxAuxins, auxinRadius, veinNodeRadius, killRadius, neighborhoodRadius );
		Reset ();
    }
    
    void Update ()
	{
		if ( Input.GetKeyDown ( KeyCode.Space ) )
			RandomSeeds ();

		if ( Input.GetKeyDown ( KeyCode.Mouse0 ) ) {
			if ( texture == null )
				MouseSeed ();
			else
				MouseSeedWithTexture ();
		}

		if ( draw ) {
			venationAlgorithm.step();
			if ( venationAlgorithm.auxins.Count == 0 )
                startFrame++;
		}
    }
    
    public void OnPostRender()
    {
		if ( !draw ) return;

		renderer.draw ( glLineMaterial, veinThickness, veinColor );
	}
	
}