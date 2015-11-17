using System;
using UnityEngine;

public class Main: MonoBehaviour
{
    // this boots the app, which creates the JsonRPC server,
    // which in turn creates the Scene instances (one for every browser session)
    void Awake()
    {
	}
    
    
    void Start ()
	{
	}


	void Update ()
	{
    }

	
	public void OnPostRender()
	{
		return;
		if ( !scene.settings.gridVisible ) return;
		
		size = ( float ) scene.settings.gridSize;
		divisions = 2 * scene.settings.gridDivisions; // even so the grid passes through origin - FIXME: we should use unit size, not divisions
		float unit = size / divisions;
		mmVector3 gridOrigin;
		
		// snap origin to closest grid unit
		if ( activeAxis == 2 ) // YZ plane
			gridOrigin = new mmVector3 ( position.x, position.y - position.y % unit, position.z - position.z % unit ); else if ( activeAxis == 0 ) // XZ plane
			
			gridOrigin = new mmVector3 ( position.x - position.x % unit, position.y, position.z - position.z % unit ); else // XY plane
			
			gridOrigin = new mmVector3 ( position.x - position.x % unit, position.y - position.y % unit, position.z );
		
		mmMatrix4 gridMatrix = mmMatrix4.identity;
		gridMatrix.SetTranslation ( gridOrigin.x, gridOrigin.y, gridOrigin.z );
		gridMatrix *= rotationMatrix;
		lineMaterial = scene.settings.glLineMaterial;
		lineMaterial.SetPass ( 0 );
		GL.PushMatrix();
		GL.MultMatrix ( gridMatrix );
		GL.Begin ( GL.LINES );
		GL.Color ( scene.settings.gridColorMain );
		
		for ( int nr = 0; nr < 6; nr++ ) {
			for ( float i = 0; i <= divisions; i++ ) {
				// circle for fading
				float circle = Mathf.Sqrt ( 1 - Mathf.Pow ( 2f * i / divisions - 1f, 2 ) );
				GL.Vertex ( new Vector3 ( size / 2f - i * unit, 0, -size / 2f * circle ) );
				GL.Vertex ( new Vector3 ( size / 2f - i * unit, 0, size / 2f * circle ) );
				GL.Vertex ( new Vector3 ( -size / 2f * circle, 0, size / 2f - i * unit ) );
				GL.Vertex ( new Vector3 ( size / 2f * circle, 0, size / 2f - i * unit ) );
			}
			
			size -= unit * 6;
			divisions -= 6;
		}
		
		GL.End();
		GL.PopMatrix();
	}
	

}