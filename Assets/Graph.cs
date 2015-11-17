using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph
{
	public List<VeinNode> vertices;
	public List<Edge> edges;

    public Graph()
    {
		vertices = new List<VeinNode>();
		edges = new List<Edge>();
    }
	
	public void AddVertex ( Point vertex )
	{
//		Debug.Log ( "Point " + vertex.ToString() );
		VeinNode node = new VeinNode ( vertex.getPosition() );
//		Debug.Log ( "AddVertex " + node.ToString() );
		vertices.Add ( node );
	}
	public void AddEdge ( VeinNode vertex1, VeinNode vertex2 )
	{
		edges.Add ( new Edge ( vertex1, vertex2 ) );
    }
}


public struct Edge {
	public VeinNode v1, v2;
	public int timeStamp;
	
	public Edge ( VeinNode v1, VeinNode v2 )
	{
		this.v1 = v1;
		this.v2 = v2;
		this.timeStamp = Time.frameCount - Venation.startFrame;
    }
}