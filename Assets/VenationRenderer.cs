using System.Collections;
using System.Collections.Generic;

class VenationRenderer
{
//    VenationAlgorithm _va;
//    PGraphics _g;
//    int _size;
//
//    VenationRenderer ( VenationAlgorithm va, PGraphics g, int size )
//    {
//        _va = va;
//        _g = g;
//        _size = size;
//    }
//
//    void draw()
//    {
//        drawKillRadii();
//        drawAuxins();
//        drawNeighborAuxins();
//        drawInfluencerAuxins();
//        drawVeinNodes();
//        drawAuxinInfluenceDirections();
//        drawInfluentialAuxins();
//    }
//
//    void drawKillRadii()
//    {
//        float r = _va.getKillRadius() * _size;
//        PVector p;
//        _g.noStroke();
//        _g.fill ( 244 );
//        Set<VeinNode> veinNodes = _va.getVeinNodes();
//
//        foreach ( VeinNode veinNode in veinNodes ) {
//            p = veinNode.position;
//            _g.ellipse ( _size * p.x, _size * p.y, 2 * r, 2 * r );
//        }
//    }
//
//    void drawAuxins()
//    {
//        float r = 0.025 * _size;
//        PVector p;
//        _g.stroke ( 255, 192, 192 );
//        _g.strokeWeight ( 1 );
//        List<Auxin> auxins = _va.getAuxins();
//
//        foreach ( Auxin auxin in auxins ) {
//            p = auxin.position;
//
//            if ( auxin.isDoomed() )
//                _g.fill ( 255, 128, 128 );
//            else
//                _g.fill ( 255, 224, 224 );
//
//            _g.ellipse ( _size * p.x, _size * p.y, 2 * r, 2 * r );
//        }
//    }
//
//    void drawVeinNodes()
//    {
//        float r = _va.getVeinNodeRadius() * _size;
//        PVector p;
//        _g.stroke ( 0 );
//        _g.strokeWeight ( 1 );
//        _g.fill ( 255 );
//        Set<VeinNode> veinNodes = _va.getVeinNodes();
//
//        foreach ( VeinNode veinNode in veinNodes ) {
//            p = veinNode.position;
//            _g.ellipse ( _size * p.x, _size * p.y, 2 * r, 2 * r );
//        }
//    }
//
//    void drawNeighborAuxins()
//    {
//        foreach ( VeinNode veinNode in _va.getVeinNodes() )
//            drawNeighborAuxins ( veinNode );
//    }
//
//    void drawNeighborAuxins ( VeinNode veinNode )
//    {
//        float r = _va.getAuxinRadius() * _size;
//        PVector p;
//        PVector veinNodePos = veinNode.position;
//        List<Auxin> neighborAuxins = _va.getNeighborAuxins ( veinNodePos.x, veinNodePos.y );
//
//        foreach ( Auxin auxin in neighborAuxins ) {
//            p = auxin.position;
//            _g.stroke ( 255, 192, 192 );
//            _g.strokeWeight ( 1 );
//            _g.noFill();
//            _g.line ( _size * p.x, _size * p.y, _size * veinNodePos.x, _size * veinNodePos.y );
//        }
//    }
//
//    void drawInfluencerAuxins()
//    {
//        foreach ( VeinNode veinNode in _va.getVeinNodes() )
//            drawInfluencerAuxins ( veinNode );
//    }
//
//    void drawInfluencerAuxins ( VeinNode veinNode )
//    {
//        float r = _va.getAuxinRadius() * _size * 0.6;
//        PVector p;
//        PVector veinNodePos = veinNode.position;
//        List<Auxin> influencerAuxins = _va.getInfluencerAuxins ( veinNode );
//
//        foreach ( Auxin auxin in influencerAuxins ) {
//            p = auxin.position;
//            _g.stroke ( 255, 128, 128 );
//            _g.strokeWeight ( 2 );
//            _g.noFill();
//            _g.line ( _size * p.x, _size * p.y, _size * veinNodePos.x, _size * veinNodePos.y );
//            _g.noStroke();
//            _g.fill ( 255 );
//            _g.ellipse ( _size * p.x, _size * p.y, 2 * r, 2 * r );
//        }
//    }
//
//    void drawAuxinInfluenceDirections()
//    {
//        foreach ( VeinNode veinNode in _va.getVeinNodes() )
//            drawAuxinInfluenceDirection ( veinNode );
//    }
//
//    void drawAuxinInfluenceDirection ( VeinNode veinNode )
//    {
//        PVector veinNodePos = veinNode.getPosition();
//        PVector p = _va.getAuxinInfluenceDirection ( veinNode, _va.getInfluencerAuxins ( veinNode ) );
//
//        if ( p != null ) {
//            veinNodePos.mult ( _size );
//            p.mult ( 20 );
//            p.add ( veinNodePos );
//            _g.stroke ( 0 );
//            _g.strokeWeight ( 2 );
//            _g.noFill();
//            _g.line ( veinNodePos.x, veinNodePos.y, p.x, p.y );
//        }
//    }
//
//    void drawInfluentialAuxins()
//    {
//        PVector auxinPos;
//
//        foreach ( Auxin auxin in _va.getAuxins() ) {
//            if ( _va.getRelativeNeighborVeinNodes ( auxin ).Count > 1 ) {
//                auxinPos = auxin.position;
//                _g.noStroke();
//                _g.fill ( 128 );
//                _g.ellipse ( _size * auxinPos.x, _size * auxinPos.y, 10, 10 );
//            }
//        }
//    }
}
