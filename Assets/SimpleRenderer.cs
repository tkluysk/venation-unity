using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class SimpleRenderer
{
	private VenationAlgorithm venationAlgorithm;
    private float size;

	public SimpleRenderer ( VenationAlgorithm venationAlgorithm, int size )
    {
		this.venationAlgorithm = venationAlgorithm;
        this.size = size;
    }

    public void draw ( Material lineMaterial )
	{
		PVector p;
		List<VeinNode> veinNodes = venationAlgorithm.getVeinNodes();
		List<Edge> veinEdges = venationAlgorithm.getVeinEdges();
		
//		Debug.Log ( "drawing nodes " + veinNodes.Count );
        
        lineMaterial.SetPass ( 0 );
		GL.PushMatrix();
		GL.LoadPixelMatrix();
		GL.Begin ( GL.LINES );
		GL.Color ( Color.red );
		
		foreach ( VeinNode veinNode in veinNodes ) {
			p = veinNode.getPosition() * size;
//			Debug.Log ( "drawing node " + p.x + " " + p.y );
			MiscUtil.GLDrawCircle ( p, 2.0f );
		}
		
		GL.Color ( Color.black );
		
//		foreach ( Edge edge in veinEdges ) {
//			MiscUtil.GLDrawLine ( edge.v1.position, edge.v2.position );
//        }

		foreach ( Auxin auxin in venationAlgorithm.auxins ) {
			p = auxin.getPosition() * size;
//			Debug.Log ( "drawing auxin " + p.x + " " + p.y );
			MiscUtil.GLDrawCross ( p, 1.0f );
//			MiscUtil.GLDrawDot ( p );
        }
        
        GL.End();
		GL.PopMatrix();
    }
}
