using UnityEngine;

public class Venation: MonoBehaviour
{
	VenationAlgorithm va;
//	VenationRenderer renderer;
	SimpleRenderer renderer;

	// this boots the app, which creates the JsonRPC server,
	// which in turn creates the Scene instances (one for every browser session)
	void Awake()
	{
		size ( 800, 800 );
		reset();
        redraw();
    }
    
	void Start ()
	{
	}
	
	void Reset()
	{
		va = new VenationAlgorithm();
		//	renderer = new VenationRenderer ( va, this.g, width );
		renderer = new SimpleRenderer ( va, this.g, width );
	}
	
	void Update ()
	{
		switch ( key ) {
		case 'e':
			reset();
			redraw();
			break;
			
		case ' ':
			va.step();
			redraw();
			break;
			
		case 'r':
			save ( "render.png" );
            break;
        }
    }
    
    
    public void OnPostRender()
    {
        
        background ( 255 );
        renderer.draw();
        
        return;

//		mmMatrix4 gridMatrix = mmMatrix4.identity;
//		gridMatrix.SetTranslation ( gridOrigin.x, gridOrigin.y, gridOrigin.z );
//		gridMatrix *= rotationMatrix;
//		lineMaterial = scene.settings.glLineMaterial;
//		lineMaterial.SetPass ( 0 );
//		GL.PushMatrix();
//		GL.MultMatrix ( gridMatrix );
//		GL.Begin ( GL.LINES );
//		GL.Color ( scene.settings.gridColorMain );
//		
//		for ( int nr = 0; nr < 6; nr++ ) {
//			for ( float i = 0; i <= divisions; i++ ) {
//				// circle for fading
//				float circle = Mathf.Sqrt ( 1 - Mathf.Pow ( 2f * i / divisions - 1f, 2 ) );
//				GL.Vertex ( new Vector3 ( size / 2f - i * unit, 0, -size / 2f * circle ) );
//				GL.Vertex ( new Vector3 ( size / 2f - i * unit, 0, size / 2f * circle ) );
//				GL.Vertex ( new Vector3 ( -size / 2f * circle, 0, size / 2f - i * unit ) );
//				GL.Vertex ( new Vector3 ( size / 2f * circle, 0, size / 2f - i * unit ) );
//			}
//			
//			size -= unit * 6;
//			divisions -= 6;
//		}
//		
//		GL.End();
//		GL.PopMatrix();
	}
	
}