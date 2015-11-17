using System.Collections;
using System.Collections.Generic;

class Auxin : Point
{
    private bool _isDoomed;
    private List<VeinNode> _taggedVeinNodes;

    Auxin()
    {
        position = new PVector();
        _isDoomed = false;
        _taggedVeinNodes = new List<VeinNode>();
    }

    Auxin ( PVector p )
    {
        position = p;
        _isDoomed = false;
        _taggedVeinNodes = new List<VeinNode>();
    }

    Auxin ( float x, float y )
    {
        position = new PVector ( x, y );
        _isDoomed = false;
        _taggedVeinNodes = new List<VeinNode>();
    }

    bool isDoomed()
    {
        return _isDoomed;
    }

    void setDoomed ( bool b )
    {
        _isDoomed = b;
    }

    List<VeinNode> getTaggedVeinNodesRef()
    {
        return _taggedVeinNodes;
    }

    bool hasTaggedVeinNodes()
    {
        return _taggedVeinNodes.size() > 0;
    }

    void setTaggedVeinNodes ( List<VeinNode> veinNodes )
    {
        _taggedVeinNodes = veinNodes;
    }
}
