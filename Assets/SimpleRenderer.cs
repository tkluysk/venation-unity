using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class SimpleRenderer
{
    private VenationAlgorithm _va;
    private int _size;

    public SimpleRenderer ( VenationAlgorithm va, int size )
    {
        _va = va;
        _size = size;
    }

    public void draw()
    {
		
		
		//		mmMatrix4 gridMatrix = mmMatrix4.identity;
		//		gridMatrix.SetTranslation ( gridOrigin.x, gridOrigin.y, gridOrigin.z );
		//		gridMatrix *= rotationMatrix;
		//		lineMaterial = scene.settings.glLineMaterial;
		//		lineMaterial.SetPass ( 0 );
		//		GL.PushMatrix();
		//		GL.MultMatrix ( gridMatrix );
		//		GL.Begin ( GL.LINES );
		//		GL.Color ( scene.settings.gridColorMain );
		//		
		//		for ( int nr = 0; nr < 6; nr++ ) {
		//			for ( float i = 0; i <= divisions; i++ ) {
		//				// circle for fading
		//				float circle = Mathf.Sqrt ( 1 - Mathf.Pow ( 2f * i / divisions - 1f, 2 ) );
		//				GL.Vertex ( new Vector3 ( size / 2f - i * unit, 0, -size / 2f * circle ) );
		//				GL.Vertex ( new Vector3 ( size / 2f - i * unit, 0, size / 2f * circle ) );
		//				GL.Vertex ( new Vector3 ( -size / 2f * circle, 0, size / 2f - i * unit ) );
		//				GL.Vertex ( new Vector3 ( size / 2f * circle, 0, size / 2f - i * unit ) );
        //			}
        //			
        //			size -= unit * 6;
        //			divisions -= 6;
        //		}
		//		
		
		lineMaterial.SetPass ( 0 );
		GL.PushMatrix();
		GL.LoadPixelMatrix();
		GL.Begin ( GL.LINES );
		Vector3 point;
		GL.Color ( scene.settings.pointColor );

		//		GL.End();
		//		GL.PopMatrix();


		
		
        PVector p;
        List<VeinNode> veinNodes = _va.getVeinNodes();
        _g.stroke ( 128 );
        _g.strokeWeight ( 1 );
        _g.noFill();

        foreach ( VeinNode veinNode in veinNodes ) {
            p = veinNode.getPositionRef();
            drawPoint ( _size * p.x, _size * p.y );
        }
    }

    public void drawPoint ( float x, float y )
    {
        _g.line ( x, y, x, y + 4 );
    }
}
