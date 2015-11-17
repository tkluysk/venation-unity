using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph
{
	public List<VeinNode> vertices;

    public Graph()
    {
		vertices = new List<VeinNode>();
    }
	
	public void AddVertex ( Point vertex )
	{
		Debug.Log ( "Point " + vertex.ToString() );
		VeinNode node = new VeinNode ( vertex.getPosition() );
		Debug.Log ( "AddVertex " + node.ToString() );
		vertices.Add ( node );
	}
	public void AddEdge ( Point vertex1, Point vertex2 )
	{
		// nothing here yet
    }
}