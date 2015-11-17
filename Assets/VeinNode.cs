using UnityEngine;

public class VeinNode : Point
{
    VeinNode()
    {
		position = new PVector();
		screenPosition = Vector3.zero;
    }

    public VeinNode ( PVector p )
    {
		position = p;
		screenPosition = new Vector3 ( p.x * Screen.width, p.y * Screen.height, 0 );
    }

    public VeinNode ( float x, float y )
    {
		position = new PVector ( x, y );
		screenPosition = new Vector3 ( x * Screen.width, y * Screen.height, 0 );
    }

}
