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
		vertices.Add ( (VeinNode)vertex );
	}
	public void AddEdge ( Point vertex1, Point vertex2 )
	{
		// nothing here yet
    }
}