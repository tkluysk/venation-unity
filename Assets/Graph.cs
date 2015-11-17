using System.Collections;
using System.Collections.Generic;

public class Graph
{
	public List<Point> vertices;

    public Graph()
    {
		vertices = new List<Point>();
    }
	
	public void AddVertex ( Point vertex )
	{
		vertices.Add ( vertex );
	}
	public void AddEdge ( Point vertex1, Point vertex2 )
	{
		// nothing here yet
    }
}