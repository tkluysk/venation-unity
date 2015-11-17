using System.Collections;
using System.Collections.Generic;

class Auxin : Point
{
    private boolean _isDoomed;
    private List<VeinNode> _taggedVeinNodes;

    Auxin()
    {
        pos = new PVector();
        _isDoomed = false;
        _taggedVeinNodes = new List<VeinNode>();
    }

    Auxin ( PVector p )
    {
        pos = p;
        _isDoomed = false;
        _taggedVeinNodes = new List<VeinNode>();
    }

    Auxin ( float x, float y )
    {
        pos = new PVector ( x, y );
        _isDoomed = false;
        _taggedVeinNodes = new List<VeinNode>();
    }

    boolean isDoomed()
    {
        return _isDoomed;
    }

    void setDoomed ( boolean b )
    {
        _isDoomed = b;
    }

    List<VeinNode> getTaggedVeinNodesRef()
    {
        return _taggedVeinNodes;
    }

    boolean hasTaggedVeinNodes()
    {
        return _taggedVeinNodes.size() > 0;
    }

    void setTaggedVeinNodes ( List<VeinNode> veinNodes )
    {
        _taggedVeinNodes = veinNodes;
    }
}
