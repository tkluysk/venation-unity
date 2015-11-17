using System.Collections;
using System.Collections.Generic;

public class Auxin : Point
{
    private bool _isDoomed;
    private List<VeinNode> _taggedVeinNodes;

	public Auxin()
    {
        position = new PVector();
        _isDoomed = false;
        _taggedVeinNodes = new List<VeinNode>();
    }

	public Auxin ( PVector p )
    {
        position = p;
        _isDoomed = false;
        _taggedVeinNodes = new List<VeinNode>();
    }

    public Auxin ( float x, float y )
    {
        position = new PVector ( x, y );
        _isDoomed = false;
        _taggedVeinNodes = new List<VeinNode>();
    }

	public bool isDoomed()
    {
        return _isDoomed;
    }

	public void setDoomed ( bool b )
    {
        _isDoomed = b;
    }

	public List<VeinNode> getTaggedVeinNodesRef()
    {
        return _taggedVeinNodes;
    }

	public bool hasTaggedVeinNodes()
    {
        return _taggedVeinNodes.Count > 0;
    }

	public void setTaggedVeinNodes ( List<VeinNode> veinNodes )
    {
        _taggedVeinNodes = veinNodes;
    }
}
