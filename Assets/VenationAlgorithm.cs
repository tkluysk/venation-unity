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
	Dictionary<Auxin,List<VeinNode>> influencedNodes;
	
	public List<Auxin> auxins;
	int nrOfSeeds;
	int maxAuxins;
    float auxinRadius;
    float veinNodeRadius;
    float killRadius;
    float neighborhoodRadius;
    Graph graph;
	Texture2D texture;
	
	public VenationAlgorithm ( int nrOfSeeds, int maxAuxins, float auxinRadius, float veinNodeRadius, float killRadius, float neighborhoodRadius )
	{		
		this.nrOfSeeds = nrOfSeeds;
		this.maxAuxins = maxAuxins;
		this.auxinRadius = auxinRadius;
		this.veinNodeRadius = veinNodeRadius;
		this.killRadius = killRadius;
		this.neighborhoodRadius = neighborhoodRadius;
		
		auxins = new List<Auxin>();
		
		graph = new Graph();
        seedVeinNodes();
        seedAuxins();
	}

	// Using mouse without texture
	public VenationAlgorithm ( int maxAuxins, float auxinRadius, float veinNodeRadius, float killRadius, float neighborhoodRadius )
	{		
		this.nrOfSeeds = 1;
		this.maxAuxins = maxAuxins;
		this.auxinRadius = auxinRadius;
		this.veinNodeRadius = veinNodeRadius;
		this.killRadius = killRadius;
		this.neighborhoodRadius = neighborhoodRadius;
		
		auxins = new List<Auxin>();
		
		graph = new Graph();
		
		float mouseX = Input.mousePosition.x/Screen.width;
		float mouseY = Input.mousePosition.y/Screen.height;
		
		graph.AddVertex ( new VeinNode ( mouseX, mouseY ) );
        seedAuxins();
    }
    
    // Using mouse with texture
	public VenationAlgorithm ( Texture2D texture, int maxAuxins, float auxinRadius, float veinNodeRadius, float killRadius, float neighborhoodRadius )
	{	
		this.texture = texture;
		
		float mouseX = Input.mousePosition.x/Screen.width;
		float mouseY = Input.mousePosition.y/Screen.height;
		
		
		this.maxAuxins = maxAuxins;
		this.auxinRadius = auxinRadius;
		this.veinNodeRadius = veinNodeRadius;
		this.killRadius = killRadius;
		this.neighborhoodRadius = neighborhoodRadius;
		
		auxins = new List<Auxin>();
		
		graph = new Graph();
		
		graph.AddVertex ( new VeinNode ( mouseX, mouseY ) );
        
        Vector3 startColor = SampleColorVector ( mouseX, mouseY );
        seedAuxins ( startColor );
    }


	Vector3 SampleColorVector ( float x, float y )
	{
		Color tmpColor = texture.GetPixel( Mathf.FloorToInt ( texture.width * x ), Mathf.FloorToInt ( texture.height * y ) );
		return new Vector3 ( tmpColor.r, tmpColor.g, tmpColor.b );
	}
    
    /// Accessors
    
    float getAuxinRadius()
    {
        return auxinRadius;
    }

    float getVeinNodeRadius()
    {
        return veinNodeRadius;
    }

    float getKillRadius()
    {
        return killRadius;
    }

    Auxin getAuxin ( int index )
    {
        return auxins[ index ];
    }

    List<Auxin> getAuxins()
    {
        return auxins;
    }

    int numAuxins()
    {
        return auxins.Count;
    }
	
	public List<VeinNode> getVeinNodes()
	{
		return graph.vertices;
	}

	public List<Edge> getVeinEdges()
	{
		return graph.edges;
	}
    
    int numVeinNodes()
    {
        return graph.vertices.Count;
    }

    List<Auxin> getNeighborAuxins ( float x, float y )
    {
        float dx, dy, r = 4.0f * neighborhoodRadius * neighborhoodRadius;
        PVector p;
        List<Auxin> neighborAuxins = new List<Auxin>();

        foreach ( Auxin auxin in auxins ) {
            p = auxin.position;
            dx = p.x - x;
            dy = p.y - y;

            if ( dx * dx + dy * dy < r )
                neighborAuxins.Add ( auxin );
        }

        return neighborAuxins;
    }

    List<VeinNode> getNeighborVeinNodes ( float x, float y )
    {
        float dx, dy, r = 4.0f * neighborhoodRadius * neighborhoodRadius;
        PVector p;
        List<VeinNode> neighborVeinNodes = new List<VeinNode>();
        List<VeinNode> veinNodes = graph.vertices;

        foreach ( VeinNode veinNode in veinNodes ) {
            p = veinNode.position;
            dx = p.x - x;
            dy = p.y - y;

            if ( dx * dx + dy * dy < r )
                neighborVeinNodes.Add ( veinNode );
        }

        return neighborVeinNodes;
    }

    List<Auxin> getInfluencerAuxins ( VeinNode veinNode )
    {
        PVector veinNodePos = veinNode.position;
        List<Auxin> neighborAuxins = getNeighborAuxins ( veinNodePos.x, veinNodePos.y );
        List<Auxin> influencerAuxins = new List<Auxin>();

        foreach ( Auxin auxin in neighborAuxins ) {
			if ( influencedNodes[ auxin ].Contains ( veinNode ) )
                influencerAuxins.Add ( auxin );
        }

        return influencerAuxins;
    }

    List<VeinNode> getInfluencedVeinNodes ( Auxin auxin )
    {
        VeinNode veinNode;
        PVector veinNodePos, auxinPos = auxin.position;
        List<VeinNode> veinNodes = getRelativeNeighborVeinNodes ( auxin );

        for ( int i = 0; i < veinNodes.Count; i++ ) {
            veinNode = veinNodes[i];
            veinNodePos = veinNode.position;

            if ( PVector.sub ( veinNodePos, auxinPos ).mag() < killRadius ) {
                veinNodes.RemoveAt ( i );
                i--;
            }
        }

        return veinNodes;
    }

    PVector getAuxinInfluenceDirection ( VeinNode veinNode, List<Auxin> auxinInfluencers )
    {
		PVector p;
		PVector result = new PVector();

        foreach ( Auxin auxin in auxinInfluencers ) {
            p = auxin.Clone();
            p.sub ( veinNode.position );
            p.normalize();

            result.add ( p );
        }

//		Debug.Log ( auxinInfluencers.Count );

		if ( auxinInfluencers.Count > 0 ) {
            if ( result.mag() < 1 ) {
                Auxin auxin = auxinInfluencers[0];
                p = auxin.Clone();
                p.sub ( veinNode.position );
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
        PVector p0, p1, auxinPos = auxin.position;
        PVector auxinToP0, auxinToP1, p0ToP1;
        // Limit search to the neighborhood of the auxin.
        List<VeinNode> neighborVeinNodes = getNeighborVeinNodes ( auxinPos.x, auxinPos.y );
        // p0 is a relative neighbor of auxinPos iff
        // for any point p1 that is closer to auxinPos than is p0,
        // p0 is closer to auxinPos than to p1.
        List<VeinNode> relNeighborVeinNodes = new List<VeinNode>();

        foreach ( VeinNode vn0 in neighborVeinNodes ) {
            p0 = vn0.position;
            auxinToP0 = PVector.sub ( p0, auxinPos );
            fail = false;

            foreach ( VeinNode vn1 in neighborVeinNodes ) {
                if ( vn0 == vn1 ) continue;

                p1 = vn1.position;
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
        List<VeinNode> veinNodes = graph.vertices;

        foreach ( VeinNode veinNode in veinNodes ) {
            p = veinNode.position;
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

    public void step()
    {
        placeVeinNodes();
        killAuxins();
    }

    void seedVeinNodes()
    {
        float x, y;
        VeinNode veinNode;

		for ( int i = 0; i < nrOfSeeds; i++ ) {
			x = UnityEngine.Random.Range(0.0f,1.0f);
			y = UnityEngine.Random.Range(0.0f,1.0f);
            veinNode = new VeinNode ( x, y );
            graph.AddVertex ( veinNode );
        }
    }
	
	void seedAuxins()
	{
		float x, y;
		
		for ( int i = 0; i < 100000 && auxins.Count < maxAuxins; i++ ) {
			x = UnityEngine.Random.Range(0.0f,1.0f);
			y = UnityEngine.Random.Range(0.0f,1.0f);			
            if ( !hitTestPotentialAuxin ( x, y ) )
                auxins.Add ( new Auxin ( x, y ) );
        }
    }

	void seedAuxins( Vector3 startColor )
	{
		float x, y;
		
		for ( int i = 0; i < 100000 && auxins.Count < maxAuxins; i++ ) {
			x = UnityEngine.Random.Range(0.0f,1.0f);
			y = UnityEngine.Random.Range(0.0f,1.0f);

			if ( ( SampleColorVector ( x, y ) - startColor ).sqrMagnitude > (.3f*.3f) )
				continue;

			if ( !hitTestPotentialAuxin ( x, y ) )
				auxins.Add ( new Auxin ( x, y ) );
        }
    }
    
    void placeVeinNodes()
    {
        // Make sure we don't iterate newly-placed vein nodes.
        System.Object[] veinNodes = graph.vertices.ToArray();
        int count = veinNodes.Length;

		influencedNodes = new Dictionary<Auxin, List<VeinNode>>();
		foreach ( Auxin auxin in auxins )
			influencedNodes.Add ( auxin, getInfluencedVeinNodes ( auxin ) );
                
        for ( int i = 0; i < count; i++ ) {
            VeinNode veinNode = ( VeinNode ) veinNodes[i];
//			Debug.Log ( "placeVeinNode " + i );
            placeVeinNode ( veinNode );
        }
    }

    void placeVeinNode ( VeinNode seedVeinNode )
    {
        VeinNode veinNode;
		List<Auxin> influencerAuxins = getInfluencerAuxins ( seedVeinNode );
//		Debug.Log ( "# influencerAuxins " + influencerAuxins.Count );
		PVector p = getAuxinInfluenceDirection ( seedVeinNode, influencerAuxins );
		
//		Debug.Log ( "getAuxinInfluenceDirection " + p.ToString() );

        if ( p != PVector.zero ) {
            if ( p.mag() <= 0 ) {
                p.x = 1;
                p.y = 0;
				p.rotate ( UnityEngine.Random.Range(0.0f,1.0f) * 2 * Mathf.PI );
            }

            p.mult ( 2 * veinNodeRadius );
			//p.rotate((2 * UnityEngine.Random.Range(0.0f,1.0f) - 1) * 2 * PI * 0.05); // jitter
            p.add ( seedVeinNode.position );
            veinNode = new VeinNode ( p );
            graph.AddVertex ( veinNode );
            graph.AddEdge ( seedVeinNode, veinNode );
        }
    }

    void killAuxins()
    {
        Auxin auxin;
        VeinNode veinNode;
        PVector auxinPos, veinNodePos;
        float dist;

        for ( int i = 0; i < auxins.Count; i++ ) {
            auxin = auxins[i];
            auxinPos = auxin.position;

            if ( auxin.isDoomed() ) {
                List<VeinNode> influencedVeinNodes = getInfluencedVeinNodes ( auxin );
                List<VeinNode> taggedVeinNodes = auxin.getTaggedVeinNodesRef();

                for ( int j = 0; j < taggedVeinNodes.Count; j++ ) {
                    veinNode = taggedVeinNodes[j];
                    veinNodePos = veinNode.position;
                    // FIXME: Inefficient because of PVector instantiation.
                    dist = PVector.sub ( veinNodePos, auxinPos ).mag();

                    if ( dist < killRadius || !influencedVeinNodes.Contains ( veinNode ) ) {
                        taggedVeinNodes.RemoveAt ( j );
                        j--;
                    }
                }

                if ( taggedVeinNodes.Count <= 0 ) {
                    auxins.RemoveAt ( i );
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
                        auxins.RemoveAt ( i );
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
        r = killRadius * killRadius;
        List<VeinNode> veinNodes = graph.vertices;

        foreach ( VeinNode veinNode in veinNodes ) {
            p = veinNode.position;
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
        r = 4.0f * auxinRadius * auxinRadius;

        foreach ( Auxin auxin in auxins ) {
            p = auxin.position;
            dx = p.x - x;
            dy = p.y - y;

            if ( dx * dx + dy * dy < r )
                return true;
        }

        r = killRadius * killRadius;
        List<VeinNode> veinNodes = graph.vertices;

        foreach ( VeinNode veinNode in veinNodes ) {
            p = veinNode.position;
            dx = p.x - x;
            dy = p.y - y;

            if ( dx * dx + dy * dy < r )
                return true;
        }

        return false;
    }
}
