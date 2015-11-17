using UnityEngine;

public class Point
{
	public PVector position;
	public Vector3 screenPosition;
    
    public Point()
    {
        position = new PVector();
		screenPosition = Vector3.zero;
    }

	public Point ( PVector p )
    {
        position = p;
		screenPosition = new Vector3 ( p.x * Screen.width, p.y * Screen.height, 0 );
    }

	public Point ( float x, float y )
    {
		position = new PVector ( x, y );
		screenPosition = new Vector3 ( x * Screen.width, y * Screen.height, 0 );
	}

	public PVector Clone()
    {
        return new PVector ( position );
    }
    
    public string ToString ()
	{
		return string.Format ( "({0:F1}, {1:F1})", position.x, position.y );
	}

}
