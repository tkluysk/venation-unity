using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PVector : ICloneable
{
    public const double kEpsilon = double.Epsilon;
    public double x;
    public double y;
    public double z;

    public PVector()
    {
        x = 0;
        y = 0;
        z = 0;
    }

    public PVector ( PVector _value )
    {
        this.x = _value.x;
        this.y = _value.y;
        this.z = _value.z;
    }

    public PVector ( Vector3 _value )
    {
        this.x = _value.x;
        this.y = _value.y;
        this.z = _value.z;
    }
	
	public PVector ( double x, double y, double z )
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}
	
	public PVector ( Color color )
	{
		this.x = color.r;
		this.y = color.g;
		this.z = color.b;
	}

    // for conversion of json vectors
    public PVector ( Int32[] xyz )
    {
        this.x = System.Convert.ToDouble ( xyz[ 0 ] );
        this.y = System.Convert.ToDouble ( xyz[ 1 ] );
        this.z = System.Convert.ToDouble ( xyz[ 2 ] );
    }

    public object Clone()
    {
        return new PVector ( x, y, z );
    }


    public void Set ( double _x, double _y, double _z )
    {
        this.x = _x;
        this.y = _y;
        this.z = _z;
    }

    public void Set ( PVector vector )
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
    }

    public double this[ int index ]
    {
        get {
            switch ( index ) {
                case 0: {
                    return this.x;
                }

                case 1: {
                    return this.y;
                }

                case 2: {
                    return this.z;
                }

                default: {
                    throw new IndexOutOfRangeException ( "out of range Vector3 index" );
                }
            }
        }
        set {
            switch ( index ) {
                case 0: {
                    this.x = value;
                    break;
                }

                case 1: {
                    this.y = value;
                    break;
                }

                case 2: {
                    this.z = value;
                    break;
                }

                default: {
                    throw new IndexOutOfRangeException ( "out of range Vector3 index" );
                }
            }
        }
    }


    public void MoveTo ( PVector to )
    {
        this.x = to.x;
        this.y = to.y;
        this.z = to.z;
    }

    public float r
    {
        get { return ( float ) this.x; }
        set { x = ( double ) value; }
    }
    public float g
    {
        get { return ( float ) this.y; }
        set { y = ( double ) value; }
    }
    public float b
    {
        get { return ( float ) this.z; }
        set { z = ( double ) value; }
    }

    public PVector normalized
    {
        get {
            return PVector.Normalize ( this );
        }
    }
    public double magnitude
    {
        get {
            return Math.Sqrt ( this.x * this.x + this.y * this.y + this.z * this.z );
        }
    }

    public double sqrMagnitude
    {
        get {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }
    }

    public static PVector zero
    {
        get {
            return new PVector ( 0, 0, 0 );
        }
    }

    public static PVector one
    {
        get {
            return new PVector ( 1, 1, 1 );
        }
    }

    public static PVector forward
    {
        get {
            return new PVector ( 0, 0, 1 );
        }
    }
    public static PVector back
    {
        get {
            return new PVector ( 0, 0, -1 );
        }
    }
    public static PVector up
    {
        get {
            return new PVector ( 0, 1, 0 );
        }
    }
    public static PVector down
    {
        get {
            return new PVector ( 0, -1, 0 );
        }
    }
    public static PVector left
    {
        get {
            return new PVector ( -1, 0, 0 );
        }
    }
    public static PVector right
    {
        get {
            return new PVector ( 1, 0, 0 );
        }
    }

    public static PVector Scale ( PVector a, PVector b )
    {
        return new PVector ( a.x * b.x, a.y * b.y, a.z * b.z );
    }

    public PVector Lerp ( PVector a, double t )
    {
        return this + t * ( a - this );
    }

    public void Scale ( PVector vector )
    {
        this.x *= vector.x;
        this.y *= vector.y;
        this.z *= vector.z;
    }

    public void add ( PVector vector )
    {
        this.x += vector.x;
        this.y += vector.y;
        this.z += vector.z;
    }

    public static PVector Cross ( PVector lhs, PVector rhs )
    {
        return new PVector ( lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x );
    }

    public PVector Cross ( PVector other )
    {
        return new PVector ( this.y * other.z - this.z * other.y, this.z * other.x - this.x * other.z, this.x * other.y - this.y * other.x );
    }

    public override bool Equals ( object other )
    {
        if ( ! ( other is PVector ) )
            return false;

        PVector vector = ( PVector ) other;
        return this.x.Equals ( vector.x ) && this.y.Equals ( vector.y ) && this.z.Equals ( vector.z );
    }


    public static PVector Reflect ( PVector inDirection, PVector inNormal )
    {
        return -2f * PVector.Dot ( inNormal, inDirection ) * inNormal + inDirection;
    }

    public static PVector Normalize ( PVector value )
    {
        double num = PVector.Magnitude ( value );

        if ( num > kEpsilon )
            return value / num;

        return PVector.zero;
    }

    public void Normalize ()
    {
        double num = PVector.Magnitude ( this );

        if ( num > kEpsilon ) {
            PVector.Normalize ( this );
            //this /= num;
        }
        else {
            x = 0;
            y = 0;
            z = 0;
        }
    }

    public double[] ToArray ()
    {
        return new double[] {this.x, this.y, this.z};
    }
	
	public override string ToString ()
	{
		return string.Format ( "({0:F1}, {1:F1}, {2:F1})", this.x, this.y, this.z );
	}

	public string ToString ( int precision )
	{
		return string.Format ( "({0:F" + precision + "}, {1:F" + precision + "}, {2:F" + precision + "})", this.x, this.y, this.z );
	}

    public string ToString ( string format )
    {
        return string.Format ( "({0}, {1}, {2})", this.x.ToString ( format ), this.y.ToString ( format ), this.z.ToString ( format ) );
    }


    public static double Dot ( PVector lhs, PVector rhs )
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }
    public double Dot ( PVector other )
    {
        return this.x * other.x + this.y * other.y + this.z * other.z;
    }

    public static PVector Project ( PVector vector, PVector onNormal )
    {
        double num = PVector.Dot ( onNormal, onNormal );

        if ( num < kEpsilon )
            return PVector.zero;

        return onNormal * PVector.Dot ( vector, onNormal ) / num;
    }

    public static PVector Exclude ( PVector excludeThis, PVector fromThat )
    {
        return fromThat - PVector.Project ( fromThat, excludeThis );
    }


    public static double Distance ( PVector a, PVector b )
    {
        PVector vector = new PVector ( a.x - b.x, a.y - b.y, a.z - b.z );
        return Math.Sqrt ( vector.x * vector.x + vector.y * vector.y + vector.z * vector.z );
    }

    public static PVector ClampMagnitude ( PVector vector, float maxLength )
    {
        if ( vector.sqrMagnitude > maxLength * maxLength )
            return vector.normalized * maxLength;

        return vector;
    }


    public static double Magnitude ( PVector a )
    {
        return Math.Sqrt ( a.x * a.x + a.y * a.y + a.z * a.z );
    }

    public static double SqrMagnitude ( PVector a )
    {
        return a.x * a.x + a.y * a.y + a.z * a.z;
    }


    public static PVector Min ( PVector lhs, PVector rhs )
    {
        return new PVector ( Math.Min ( lhs.x, rhs.x ), Math.Min ( lhs.y, rhs.y ), Math.Min ( lhs.z, rhs.z ) );
    }
    public static PVector Max ( PVector lhs, PVector rhs )
    {
        return new PVector ( Math.Max ( lhs.x, rhs.x ), Math.Max ( lhs.y, rhs.y ), Math.Max ( lhs.z, rhs.z ) );
    }

    public static PVector operator + ( PVector a, PVector b )
    {
        return new PVector ( a.x + b.x, a.y + b.y, a.z + b.z );
    }

    public PVector Plus ( PVector other )
    {
        return new PVector ( this.x + other.x, this.y + other.y, this.z + other.z );
    }

    public static PVector operator - ( PVector a, PVector b )
    {
        return new PVector ( a.x - b.x, a.y - b.y, a.z - b.z );
    }

    public PVector Minus ( PVector other )
    {
        return new PVector ( this.x - other.x, this.y - other.y, this.z - other.z );
    }

    public static PVector operator - ( PVector a )
    {
        return new PVector ( -a.x, -a.y, -a.z );
    }

    public PVector Negated ()
    {
        return new PVector ( -this.x, -this.y, -this.z );
    }

    public static PVector operator * ( PVector a, double d )
    {
        return new PVector ( a.x * d, a.y * d, a.z * d );
    }

    public static PVector operator * ( double d, PVector a )
    {
        return new PVector ( a.x * d, a.y * d, a.z * d );
    }

    public PVector Times ( double other )
    {
        return new PVector ( this.x * other, this.y * other, this.z * other );
    }

    public static PVector operator * ( PVector a, PVector b )
    {
        return new PVector ( a.x * b.x, a.y * b.y, a.z * b.z );
    }

    public static PVector operator / ( PVector a, double d )
    {
        return new PVector ( a.x / d, a.y / d, a.z / d );
    }

    public PVector DividedBy ( double other )
    {
        return new PVector ( this.x / other, this.y / other, this.z / other );
    }

    public static bool operator == ( PVector lhs, PVector rhs )
    {
        if ( Math.Abs ( lhs.x - rhs.x ) > kEpsilon ) return false;

        if ( Math.Abs ( lhs.y - rhs.y ) > kEpsilon ) return false;

        if ( Math.Abs ( lhs.z - rhs.z ) > kEpsilon ) return false;

        return true;
//		return Vector.SqrMagnitude (lhs - rhs) < 9.99999944E-11f;
    }

    public static bool operator != ( PVector lhs, PVector rhs )
    {
        return ! ( lhs == rhs );
//		return Vector.SqrMagnitude (lhs - rhs) >= 9.99999944E-11f;
    }





    /*
    	public Vector3 ToVector3() {
    		return new Vector3((float)x,(float)y,(float)z);
    	}
    */

    // internal conversions
    public static implicit operator Vector3 ( PVector myParam )
    {
        return new Vector3 ( ( float ) myParam.x, ( float ) myParam.y, ( float ) myParam.z );
    }
    public static implicit operator Color ( PVector colorAsVector )
    {
        return new Color ( ( float ) colorAsVector.x, ( float ) colorAsVector.y, ( float ) colorAsVector.z );
	}
	public static implicit operator Color32 ( PVector colorAsVector )
	{
		return new Color32 ( ( byte ) ( colorAsVector.x * 255 ), ( byte ) ( colorAsVector.y * 255 ), ( byte ) ( colorAsVector.z * 255 ), 255 );
	}
	public static implicit operator Single[] ( PVector vector )
	{
		return new Single[] { (Single)vector.x, (Single)vector.y, (Single)vector.z };
	}


    public static implicit operator PVector ( double[] myParam )
    {
        return new PVector ( myParam[ 0 ], myParam[ 1 ], myParam[ 2 ] );
    }
	public static implicit operator PVector ( Vector3 vec3 )
	{
		return new PVector ( vec3 );
	}
	public static implicit operator PVector ( Color color )
	{
		return new PVector ( color );
	}

}