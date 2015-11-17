// KDTree.cs - A Stark, September 2009.

//	This class implements a data structure that stores a list of points in space.
//	A common task in game programming is to take a supplied point and discover which
//	of a stored set of points is nearest to it. For example, in path-plotting, it is often
//	useful to know which waypoint is nearest to the player's current
//	position. The kd-tree allows this "nearest neighbour" search to be carried out quickly,
//	or at least much more quickly than a simple linear search through the list.

//	At present, the class only allows for construction (using the MakeFromPoints static method)
//	and nearest-neighbour searching (using FindNearest). More exotic kd-trees are possible, and
//	this class may be extended in the future if there seems to be a need.

//	The nearest-neighbour search returns an integer index - it is assumed that the original
//	array of points is available for the lifetime of the tree, and the index refers to that
//	array.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MM.Types;

namespace MM.Util
{

    public class KDTree
    {
        public KDTree[] lr;
        public mmVector3 pivot;
        public int pivotIndex;
        public int axis;

//	Change this value to 2 if you only need two-dimensional X,Y points. The search will
//	be quicker in two dimensions.
        const int numDims = 3;


        public KDTree()
        {
            lr = new KDTree[ 2 ];
        }


//	Make a new tree from a list of points.
        public static KDTree MakeFromPoints ( params mmVector3[] points )
        {
            int[] indices = Iota ( points.Length );
            return MakeFromPointsInner ( 0, 0, points.Length - 1, points, indices );
        }


//	Recursively build a tree by separating points at plane boundaries.
        static KDTree MakeFromPointsInner (
            int depth,
            int stIndex, int enIndex,
            mmVector3[] points,
            int[] inds
        )
        {
            KDTree root = new KDTree();
            root.axis = depth % numDims;
            int splitPoint = FindPivotIndex ( points, inds, stIndex, enIndex, root.axis );
            root.pivotIndex = inds[ splitPoint ];
            root.pivot = points[ root.pivotIndex ];
            int leftEndIndex = splitPoint - 1;

            if ( leftEndIndex >= stIndex )
                root.lr[ 0 ] = MakeFromPointsInner ( depth + 1, stIndex, leftEndIndex, points, inds );

            int rightStartIndex = splitPoint + 1;

            if ( rightStartIndex <= enIndex )
                root.lr[ 1 ] = MakeFromPointsInner ( depth + 1, rightStartIndex, enIndex, points, inds );

            return root;
        }


        static void SwapElements ( int[] arr, int a, int b )
        {
            int temp = arr[ a ];
            arr[ a ] = arr[ b ];
            arr[ b ] = temp;
        }


//	Simple "median of three" heuristic to find a reasonable splitting plane.
        static int FindSplitPoint ( mmVector3[] points, int[] inds, int stIndex, int enIndex, int axis )
        {
            double a = points[ inds[ stIndex ] ][ axis ];
            double b = points[ inds[ enIndex ] ][ axis ];
            int midIndex = ( stIndex + enIndex ) / 2;
            double m = points[ inds[ midIndex ] ][ axis ];

            if ( a > b ) {
                if ( m > a )
                    return stIndex;

                if ( b > m )
                    return enIndex;

                return midIndex;
            }
            else {
                if ( a > m )
                    return stIndex;

                if ( m > b )
                    return enIndex;

                return midIndex;
            }
        }


//	Find a new pivot index from the range by splitting the points that fall either side
//	of its plane.
        public static int FindPivotIndex ( mmVector3[] points, int[] inds, int stIndex, int enIndex, int axis )
        {
            int splitPoint = FindSplitPoint ( points, inds, stIndex, enIndex, axis );
            // int splitPoint = Random.Range(stIndex, enIndex);
            mmVector3 pivot = points[ inds[ splitPoint ] ];
            SwapElements ( inds, stIndex, splitPoint );
            int currPt = stIndex + 1;
            int endPt = enIndex;

            while ( currPt <= endPt ) {
                mmVector3 curr = points[ inds[ currPt ] ];

                if ( ( curr[ axis ] > pivot[ axis ] ) ) {
                    SwapElements ( inds, currPt, endPt );
                    endPt--;
                }
                else {
                    SwapElements ( inds, currPt - 1, currPt );
                    currPt++;
                }
            }

            return currPt - 1;
        }


        public static int[] Iota ( int num )
        {
            int[] result = new int[ num ];

            for ( int i = 0; i < num; i++ )
                result[ i ] = i;

            return result;
        }


//	Find the nearest point in the set to the supplied point.
        public int FindNearest ( mmVector3 pt )
        {
            double bestSqDist = 1000000000f;
            int bestIndex = -1;
            SearchNearest ( pt, ref bestSqDist, ref bestIndex );
            return bestIndex;
        }



//	Recursively search the tree.
        void SearchNearest ( mmVector3 pt, ref double bestSqSoFar, ref int bestIndex )
        {
            double mySqDist = ( pivot - pt ).sqrMagnitude;

            if ( mySqDist < bestSqSoFar ) {
                bestSqSoFar = mySqDist;
                bestIndex = pivotIndex;
            }

            double planeDist = pt[ axis ] - pivot[ axis ];
            int selector = planeDist <= 0 ? 0 : 1;

            if ( lr[ selector ] != null )
                lr[ selector ].SearchNearest ( pt, ref bestSqSoFar, ref bestIndex );

            selector = ( selector + 1 ) % 2;
            double sqPlaneDist = planeDist * planeDist;

            if ( ( lr[ selector ] != null ) && ( bestSqSoFar > sqPlaneDist ) )
                lr[ selector ].SearchNearest ( pt, ref bestSqSoFar, ref bestIndex );
        }

        // find second nearest point in the set to the supplied point (useful to find nearst point in same mesh)
        // fails when the nearest point is immediately found
        // in that case, rebuild a KDtree without the orgininal point, and do a nearestpoint on that
        public int FindSecondNearest ( mmVector3 pt )
        {
            double bestSqDist = 1000000000f;
            int bestIndex = -1;
            int secondBestIndex = -1;
            SearchSecondNearest ( pt, ref bestSqDist, ref bestIndex, ref secondBestIndex );
            return secondBestIndex;
        }

        void SearchSecondNearest ( mmVector3 pt, ref double bestSqSoFar, ref int bestIndex, ref int secondBestIndex )
        {
            double mySqDist = ( pivot - pt ).sqrMagnitude;

            if ( mySqDist < bestSqSoFar ) {
                bestSqSoFar = mySqDist;
                secondBestIndex = bestIndex;
                bestIndex = pivotIndex;
            }

            double planeDist = pt[ axis ] - pivot[ axis ];
            int selector = planeDist <= 0 ? 0 : 1;

            if ( lr[ selector ] != null )
                lr[ selector ].SearchSecondNearest ( pt, ref bestSqSoFar, ref bestIndex, ref secondBestIndex );

            selector = ( selector + 1 ) % 2;
            double sqPlaneDist = planeDist * planeDist;

            if ( ( lr[ selector ] != null ) && ( bestSqSoFar > sqPlaneDist ) )
                lr[ selector ].SearchSecondNearest ( pt, ref bestSqSoFar, ref bestIndex, ref secondBestIndex );
        }


//	Find all points within radius of the supplied point.
        public List<int> FindAllWithinRadius ( mmVector3 pt, double radius )
        {
            List<int> indices = new List<int>();
            SearchWithinRadius ( pt, ref indices, radius * radius );
            return indices;
        }


//	Recursively search the tree for points within radius
        void SearchWithinRadius ( mmVector3 pt, ref List<int> indices, double sqrRadius )
        {
            double mySqDist = ( pivot - pt ).sqrMagnitude;

            if ( mySqDist < sqrRadius )
                indices.Add ( pivotIndex );

            double planeDist = pt[ axis ] - pivot[ axis ];
            int selector = planeDist <= 0 ? 0 : 1;

            if ( lr[ selector ] != null )
                lr[ selector ].SearchWithinRadius ( pt, ref indices, sqrRadius );

            selector = ( selector + 1 ) % 2;
            double sqPlaneDist = planeDist * planeDist;

            if ( ( lr[ selector ] != null ) && ( sqrRadius > sqPlaneDist ) )
                lr[ selector ].SearchWithinRadius ( pt, ref indices, sqrRadius );
        }



//	Simple output of tree structure - mainly useful for getting a rough
//	idea of how deep the tree is (and therefore how well the splitting
//	heuristic is performing).
        public string Dump ( int level )
        {
            string result = pivotIndex.ToString().PadLeft ( level ) + "\n";

            if ( lr[ 0 ] != null )
                result += lr[ 0 ].Dump ( level + 2 );

            if ( lr[ 1 ] != null )
                result += lr[ 1 ].Dump ( level + 2 );

            return result;
        }
    }
}