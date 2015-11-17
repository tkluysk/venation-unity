/**
    @see http://algorithmicbotany.org/papers/venation.sig2005.pdf
*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


//import java.util.Set;

public class VenationAlgorithm
{
    List<Auxin> _auxins;
    float _auxinRadius;
    float _veinNodeRadius;
    float _killRadius;
    float _neighborhoodRadius;
    Graph _graph;

    public VenationAlgorithm()
    {
        _auxins = new List<Auxin>();
        _auxinRadius = 0.025f;
        _veinNodeRadius = 0.0125f;
        _killRadius = 0.025f;
        _neighborhoodRadius = 0.1f;
        _graph = new Graph();
        seedVeinNodes();
        seedAuxins();
    }

    /// Accessors

    float getAuxinRadius()
    {
        return _auxinRadius;
    }

    float getVeinNodeRadius()
    {
        return _veinNodeRadius;
    }

    float getKillRadius()
    {
        return _killRadius;
    }

    Auxin getAuxin ( int index )
    {
        return _auxins[ index ];
    }

    List<Auxin> getAuxins()
    {
        return _auxins;
    }

    int numAuxins()
    {
        return _auxins.Count;
    }

    public List<VeinNode> getVeinNodes()
    {
        return _graph.vertices;
	}

	public List<Edge> getVeinEdges()
	{
		return graph.edges;
    }

    int numVeinNodes()
    {
        return _graph.vertices.Count;
    }

    List<Auxin> getNeighborAuxins ( float x, float y )
    {
        float dx, dy, r = 4.0f * _neighborhoodRadius * _neighborhoodRadius;
        PVector p;
        List<Auxin> neighborAuxins = new List<Auxin>();

        foreach ( Auxin auxin in _auxins ) {
            p = auxin.getPositionRef();
            dx = p.x - x;
            dy = p.y - y;

            if ( dx * dx + dy * dy < r )
                neighborAuxins.Add ( auxin );
        }

        return neighborAuxins;
    }

    List<VeinNode> getNeighborVeinNodes ( float x, float y )
    {
        float dx, dy, r = 4.0f * _neighborhoodRadius * _neighborhoodRadius;
        PVector p;
        List<VeinNode> neighborVeinNodes = new List<VeinNode>();
        List<VeinNode> veinNodes = _graph.vertices;

        foreach ( VeinNode veinNode in veinNodes ) {
            p = veinNode.getPositionRef();
            dx = p.x - x;
            dy = p.y - y;

            if ( dx * dx + dy * dy < r )
                neighborVeinNodes.Add ( veinNode );
        }

        return neighborVeinNodes;
    }

    List<Auxin> getInfluencerAuxins ( VeinNode veinNode )
    {
        PVector veinNodePos = veinNode.getPositionRef();
        List<Auxin> neighborAuxins = getNeighborAuxins ( veinNodePos.x, veinNodePos.y );
        List<Auxin> influencerAuxins = new List<Auxin>();

        foreach ( Auxin auxin in neighborAuxins ) {
            // FIXME: getInfluencedVeinNodes gets called multiple times per auxin. Cache.
            if ( getInfluencedVeinNodes ( auxin ).Contains ( veinNode ) )
                influencerAuxins.Add ( auxin );
        }

        return influencerAuxins;
    }

    List<VeinNode> getInfluencedVeinNodes ( Auxin auxin )
    {
        VeinNode veinNode;
        PVector veinNodePos, auxinPos = auxin.getPositionRef();
        List<VeinNode> veinNodes = getRelativeNeighborVeinNodes ( auxin );

        for ( int i = 0; i < veinNodes.Count; i++ ) {
            veinNode = veinNodes[i];
            veinNodePos = veinNode.getPositionRef();

            if ( PVector.sub ( veinNodePos, auxinPos ).mag() < _killRadius ) {
                veinNodes.RemoveAt ( i );
                i--;
            }
        }

        return veinNodes;
    }

    PVector getAuxinInfluenceDirection ( VeinNode veinNode, List<Auxin> auxinInfluencers )
    {
        PVector p, result = null;

        foreach ( Auxin auxin in auxinInfluencers ) {
            p = auxin.getPosition();
            p.sub ( veinNode.getPositionRef() );
            p.normalize();

            if ( result == null )
                result = new PVector();

            result.add ( p );
        }

        if ( result != null ) {
            if ( result.mag() < 1 ) {
                Auxin auxin = auxinInfluencers[0];
                p = auxin.getPosition();
                p.sub ( veinNode.getPositionRef() );
                p.normalize();
                result = p;
            }
            else
                result.normalize();
        }

        return result;
    }

    List<VeinNode> getRelativeNeighborVeinNodes ( Auxin auxin )
    {
        // FIXME: Inefficient because of instantiation of PVectors.
        bool fail;
        PVector p0, p1, auxinPos = auxin.getPositionRef();
        PVector auxinToP0, auxinToP1, p0ToP1;
        // Limit search to the neighborhood of the auxin.
        List<VeinNode> neighborVeinNodes = getNeighborVeinNodes ( auxinPos.x, auxinPos.y );
        // p0 is a relative neighbor of auxinPos iff
        // for any point p1 that is closer to auxinPos than is p0,
        // p0 is closer to auxinPos than to p1.
        List<VeinNode> relNeighborVeinNodes = new List<VeinNode>();

        foreach ( VeinNode vn0 in neighborVeinNodes ) {
            p0 = vn0.getPositionRef();
            auxinToP0 = PVector.sub ( p0, auxinPos );
            fail = false;

            foreach ( VeinNode vn1 in neighborVeinNodes ) {
                if ( vn0 == vn1 ) continue;

                p1 = vn1.getPositionRef();
                auxinToP1 = PVector.sub ( p1, auxinPos );

                if ( auxinToP1.mag() > auxinToP0.mag() ) continue;

                p0ToP1 = PVector.sub ( p1, p0 );

                if ( auxinToP0.mag() > p0ToP1.mag() ) {
                    fail = true;
                    break;
                }
            }

            if ( !fail )
                relNeighborVeinNodes.Add ( vn0 );
        }

        return relNeighborVeinNodes;
    }

    VeinNode getNearestVeinNode ( float x, float y )
    {
        VeinNode candidate = null;
        PVector p;
        float dx, dy, distSq, candidateDistSq = 0;
        List<VeinNode> veinNodes = _graph.vertices;

        foreach ( VeinNode veinNode in veinNodes ) {
            p = veinNode.getPositionRef();
            dx = p.x - x;
            dy = p.y - y;
            distSq = dx * dx + dy * dy;

            if ( candidate == null || distSq < candidateDistSq ) {
                candidate = veinNode;
                candidateDistSq = distSq;
            }
        }

        return candidate;
    }

    /// Algorithm

    void step()
    {
        placeVeinNodes();
        killAuxins();
    }

    void seedVeinNodes()
    {
        float x, y;
        VeinNode veinNode;

        for ( int i = 0; i < 3; i++ ) {
			x = UnityEngine.Random.Range(0.0f,1.0f);
			y = UnityEngine.Random.Range(0.0f,1.0f);
            veinNode = new VeinNode ( x, y );
            _graph.AddVertex ( veinNode );
        }
    }

    void seedAuxins()
    {
        float x, y;

        for ( int i = 0; i < 1000 && _auxins.Count < 200; i++ ) {
			x = UnityEngine.Random.Range(0.0f,1.0f);
			y = UnityEngine.Random.Range(0.0f,1.0f);

            if ( !hitTestPotentialAuxin ( x, y ) )
                _auxins.Add ( new Auxin ( x, y ) );
        }
    }

    void placeVeinNodes()
    {
        // Make sure we don't iterate newly-placed vein nodes.
        System.Object[] veinNodes = _graph.vertices.ToArray();
        int count = veinNodes.Length;

        for ( int i = 0; i < count; i++ ) {
            VeinNode veinNode = ( VeinNode ) veinNodes[i];
            placeVeinNode ( veinNode );
        }
    }

    void placeVeinNode ( VeinNode seedVeinNode )
    {
        VeinNode veinNode;
        List<Auxin> influencerAuxins = getInfluencerAuxins ( seedVeinNode );
        PVector p = getAuxinInfluenceDirection ( seedVeinNode, influencerAuxins );

        if ( p != null ) {
            if ( p.mag() <= 0 ) {
                p.x = 1;
                p.y = 0;
				p.rotate ( UnityEngine.Random.Range(0.0f,1.0f) * 2 * Mathf.PI );
            }

            p.mult ( 2 * _veinNodeRadius );
			//p.rotate((2 * UnityEngine.Random.Range(0.0f,1.0f) - 1) * 2 * PI * 0.05); // jitter
            p.add ( seedVeinNode.getPositionRef() );
            veinNode = new VeinNode ( p );
            _graph.AddVertex ( veinNode );
            _graph.AddEdge ( seedVeinNode, veinNode );
        }
    }

    void killAuxins()
    {
        Auxin auxin;
        VeinNode veinNode;
        PVector auxinPos, veinNodePos;
        float dist;

        for ( int i = 0; i < _auxins.Count; i++ ) {
            auxin = _auxins[i];
            auxinPos = auxin.getPositionRef();

            if ( auxin.isDoomed() ) {
                List<VeinNode> influencedVeinNodes = getInfluencedVeinNodes ( auxin );
                List<VeinNode> taggedVeinNodes = auxin.getTaggedVeinNodesRef();

                for ( int j = 0; j < taggedVeinNodes.Count; j++ ) {
                    veinNode = taggedVeinNodes[j];
                    veinNodePos = veinNode.getPositionRef();
                    // FIXME: Inefficient because of PVector instantiation.
                    dist = PVector.sub ( veinNodePos, auxinPos ).mag();

                    if ( dist < _killRadius || !influencedVeinNodes.Contains ( veinNode ) ) {
                        taggedVeinNodes.RemoveAt ( j );
                        j--;
                    }
                }

                if ( taggedVeinNodes.Count <= 0 ) {
                    _auxins.RemoveAt ( i );
                    i--;
                }
            }
            else {
                if ( hitTestExistingAuxin ( auxinPos.x, auxinPos.y ) ) {
                    List<VeinNode> influencedVeinNodes = getInfluencedVeinNodes ( auxin );

                    if ( influencedVeinNodes.Count > 1 ) {
                        auxin.setDoomed ( true );
                        auxin.setTaggedVeinNodes ( influencedVeinNodes );
                    }
                    else {
                        _auxins.RemoveAt ( i );
                        i--;
                    }
                }
            }
        }
    }

    /**
        x and y are in [0,1]
    */
    private bool hitTestExistingAuxin ( float x, float y )
    {
        float dx, dy, r;
        PVector p;
        r = _killRadius * _killRadius;
        List<VeinNode> veinNodes = _graph.vertices;

        foreach ( VeinNode veinNode in veinNodes ) {
            p = veinNode.getPositionRef();
            dx = p.x - x;
            dy = p.y - y;

            if ( dx * dx + dy * dy < r )
                return true;
        }

        return false;
    }

    /**
        x and y are in [0,1]
    */
    private bool hitTestPotentialAuxin ( float x, float y )
    {
        float dx, dy, r;
        PVector p;
        r = 4.0f * _auxinRadius * _auxinRadius;

        foreach ( Auxin auxin in _auxins ) {
            p = auxin.getPositionRef();
            dx = p.x - x;
            dy = p.y - y;

            if ( dx * dx + dy * dy < r )
                return true;
        }

        r = _killRadius * _killRadius;
        List<VeinNode> veinNodes = _graph.vertices;

        foreach ( VeinNode veinNode in veinNodes ) {
            p = veinNode.getPositionRef();
            dx = p.x - x;
            dy = p.y - y;

            if ( dx * dx + dy * dy < r )
                return true;
        }

        return false;
    }
}
