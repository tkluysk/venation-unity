using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class SimpleRenderer
{
	private VenationAlgorithm venationAlgorithm;
	private Venation venation;
	private float scale;

	public SimpleRenderer ( Venation venation, VenationAlgorithm venationAlgorithm )
	{
		this.venationAlgorithm = venationAlgorithm;
		this.venation = venation;
		scale = Screen.width * venation.auxinRadius;  // assumes square aspect
    }

	public void draw ( Material lineMaterial, float veinThickness, Color veinColor )
    {
		PVector p;
		List<VeinNode> veinNodes = venationAlgorithm.getVeinNodes();
		List<Edge> veinEdges = venationAlgorithm.getVeinEdges();
		
//		Debug.Log ( "drawing nodes " + veinNodes.Count );
        
        lineMaterial.SetPass ( 0 );
		GL.PushMatrix();
		GL.LoadPixelMatrix();
		GL.Begin ( GL.LINES );
		
		GL.Color ( new Color( 0, 0, 0, .2f ) );
		foreach ( Auxin auxin in venationAlgorithm.auxins ) {
			MiscUtil.GLDrawCircle ( auxin.screenPosition, scale );
			//			MiscUtil.GLDrawCross ( auxin.screenPosition, 1.0f );
            //			MiscUtil.GLDrawDot ( p );
        }

//		GL.Color ( Color.red );
//		foreach ( VeinNode veinNode in veinNodes ) {
//			MiscUtil.GLDrawCross ( veinNode.screenPosition, 1.0f );
////			MiscUtil.GLDrawCircle ( p, 2.0f );
//		}

		GL.End();
        GL.Begin ( GL.QUADS );
        float frame = Time.frameCount - Venation.startFrame;
		foreach ( Edge edge in veinEdges ) {
			GL.Color ( new Color( veinColor.r, veinColor.g, veinColor.b, 1 - edge.timeStamp / frame ) );
//			MiscUtil.GLDrawLine ( edge.v1.screenPosition, edge.v2.screenPosition );
			MiscUtil.GLDrawEdge ( edge, veinThickness*(frame - edge.timeStamp)/50 );
        }

        
        GL.End();
		GL.PopMatrix();
    }
}
