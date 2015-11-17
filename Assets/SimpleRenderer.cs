using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class SimpleRenderer
{
    private VenationAlgorithm _va;
    private int _size;

    public SimpleRenderer ( VenationAlgorithm va, int size )
    {
        _va = va;
        _size = size;
    }

    public void draw ( Material lineMaterial )
	{
		PVector p;
		List<VeinNode> veinNodes = _va.getVeinNodes();
        
        lineMaterial.SetPass ( 0 );
		GL.PushMatrix();
		GL.LoadPixelMatrix();
		GL.Begin ( GL.LINES );
		GL.Color ( Color.black );
		
		foreach ( VeinNode veinNode in veinNodes ) {
			p = veinNode.getPositionRef();
			MiscUtil.GLDrawDot ( p );
		}

		GL.End();
		GL.PopMatrix();
    }
}
