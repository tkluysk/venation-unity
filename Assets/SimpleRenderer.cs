using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class SimpleRenderer
{
    private VenationAlgorithm va;
    private float size;

    public SimpleRenderer ( VenationAlgorithm _va, int _size )
    {
        va = _va;
        size = _size;
    }

    public void draw ( Material lineMaterial )
	{
		PVector p;
		List<VeinNode> veinNodes = va.getVeinNodes();
		
		Debug.Log ( "drawing nodes " + veinNodes.Count );
        
        lineMaterial.SetPass ( 0 );
		GL.PushMatrix();
		GL.LoadPixelMatrix();
		GL.Begin ( GL.LINES );
		GL.Color ( Color.red );
		
		foreach ( VeinNode veinNode in veinNodes ) {
			p = veinNode.getPositionRef() * size;
			Debug.Log ( "drawing node " + p.x + " " + p.y );
			MiscUtil.GLDrawCross ( p, 2.0f );
		}

		GL.End();
		GL.PopMatrix();
    }
}
