using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
    
public class MiscUtil
{
	public static float zee = .9f;

	public static void GLDrawPoint ( Vector3 position, float radius )
    {
        float x = Mathf.Round ( position.x );
        float y = Mathf.Round ( position.y );
        GL.Vertex3 ( x - radius, y - radius - 1, zee );
        GL.Vertex3 ( x - radius, y + radius, zee );
        GL.Vertex3 ( x - radius, y + radius, zee );
        GL.Vertex3 ( x + radius, y + radius, zee );
        GL.Vertex3 ( x + radius, y + radius, zee );
        GL.Vertex3 ( x + radius, y - radius, zee );
        GL.Vertex3 ( x + radius, y - radius, zee );
        GL.Vertex3 ( x - radius, y - radius - 1, zee );
	}
	
	public static void GLDrawEdge ( Edge edge, float thickness )
	{
		float perpX = thickness * edge.perpendicular.x;
		float perpY = thickness * edge.perpendicular.y;
		GL.Vertex3 ( edge.v1.screenPosition.x + perpX, edge.v1.screenPosition.y + perpY, zee );
		GL.Vertex3 ( edge.v2.screenPosition.x + perpX, edge.v2.screenPosition.y + perpY, zee );
		GL.Vertex3 ( edge.v2.screenPosition.x - perpX, edge.v2.screenPosition.y - perpY, zee );
		GL.Vertex3 ( edge.v1.screenPosition.x - perpX, edge.v1.screenPosition.y - perpY, zee );
	}
	
	public static void GLDrawLine ( Vector3 from, Vector3 to )
	{
		GL.Vertex3 ( from.x, from.y, zee );
		GL.Vertex3 ( to.x, to.y, zee );
    }
    
    public static void GLDrawDot ( Vector3 position )
	{
		GL.Vertex3 ( position.x - .5f, position.y - .5f, zee );
		GL.Vertex3 ( position.x + .5f, position.y + .5f, zee );
	}

    public static void GLDrawCross ( Vector3 position, float radius )
    {
        float x = Mathf.Round ( position.x );
        float y = Mathf.Round ( position.y );
        GL.Vertex3 ( x - radius, y - radius, zee );
        GL.Vertex3 ( x + radius, y + radius, zee );
        GL.Vertex3 ( x - radius, y + radius, zee );
        GL.Vertex3 ( x + radius, y - radius, zee );
    }
    
    public static void GLDrawSnowflake ( Vector3 position, float radius )
    {
        float x = Mathf.Round ( position.x );
        float y = Mathf.Round ( position.y );
        GL.Vertex3 ( x - radius, y - radius, zee );
        GL.Vertex3 ( x + radius, y + radius, zee );
        GL.Vertex3 ( x - radius, y + radius, zee );
        GL.Vertex3 ( x + radius, y - radius, zee );
        GL.Vertex3 ( x - radius, y, zee );
        GL.Vertex3 ( x + radius, y, zee );
        GL.Vertex3 ( x, y + radius, zee );
        GL.Vertex3 ( x, y - radius, zee );
    }
    
    public static void GLDrawCircle ( Vector3 position, float radius )
    {
        float x = Mathf.Round ( position.x );
        float y = Mathf.Round ( position.y );
        int divisions = 8;
        float increment = 2 * Mathf.PI / divisions;
        
        for ( int i = 0; i < divisions; i++ ) {
            GL.Vertex3 ( x + radius * Mathf.Sin ( i * increment ), y + radius * Mathf.Cos ( i * increment ), zee );
            GL.Vertex3 ( x + radius * Mathf.Sin ( ( i + 1 ) *increment ), y + radius * Mathf.Cos ( ( i + 1 ) *increment ), zee );
        }
    }
    
    public static void GLDrawDiamond ( Vector3 position, float radius )
    {
        float x = Mathf.Round ( position.x );
        float y = Mathf.Round ( position.y );
        GL.Vertex3 ( x - radius, y, zee );
        GL.Vertex3 ( x, y + radius, zee );
        GL.Vertex3 ( x, y + radius, zee );
        GL.Vertex3 ( x + radius, y, zee );
        GL.Vertex3 ( x + radius, y, zee );
        GL.Vertex3 ( x, y - radius, zee );
        GL.Vertex3 ( x, y - radius, zee );
        GL.Vertex3 ( x - radius, y, zee );
    }
    
}