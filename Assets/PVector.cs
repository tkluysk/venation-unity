using UnityEngine;

public class PVector
{
	public const float kEpsilon = float.Epsilon;

    public float x;
    public float y;

    public PVector()
    {
        x = 0;
        y = 0;
    }
	
	public PVector ( PVector _value )
	{
		this.x = _value.x;
		this.y = _value.y;
	}
	
	public PVector ( Vector2 _value )
	{
		this.x = _value.x;
		this.y = _value.y;
	}

	public PVector ( Vector3 _value )
	{
		this.x = _value.x;
		this.y = _value.y;
	}
	
	public PVector ( float x, float y )
	{
		this.x = x;
		this.y = y;
	}

    public void Set ( float _x, float _y )
    {
        this.x = _x;
        this.y = _y;
    }

    public void Set ( PVector vector )
    {
        this.x = vector.x;
        this.y = vector.y;
    }

    public float this[ int index ]
    {
        get {
            switch ( index ) {
                case 0: {
                    return this.x;
                }

                case 1: {
                    return this.y;
                }

                default: {
					return 0;
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

				default: {
					break;
                }
            }
        }
    }

	public float mag()
	{
		return Mathf.Sqrt ( this.x * this.x + this.y * this.y );
	}

    public float sqrMagnitude
    {
        get {
            return this.x * this.x + this.y * this.y;
        }
    }

    public static PVector zero
    {
        get {
            return new PVector ( 0, 0 );
        }
    }

    public static PVector one
    {
        get {
            return new PVector ( 1, 1 );
        }
    }

    public static PVector up
    {
        get {
            return new PVector ( 0, 1 );
        }
    }
    public static PVector down
    {
        get {
            return new PVector ( 0, -1 );
        }
    }
    public static PVector left
    {
        get {
            return new PVector ( -1, 0 );
        }
    }
    public static PVector right
    {
        get {
            return new PVector ( 1, 0 );
        }
    }

    public static PVector Scale ( PVector a, PVector b )
    {
        return new PVector ( a.x * b.x, a.y * b.y );
    }

    public PVector Lerp ( PVector a, float t )
    {
        return this + t * ( a - this );
    }

	public void add ( PVector vector )
	{
		this.x += vector.x;
		this.y += vector.y;
	}

	public void sub ( PVector vector )
	{
		this.x -= vector.x;
		this.y -= vector.y;
	}
	
	public void mult ( float other )
	{
		this.x *= other;
		this.y *= other;
	}
	
	public void rotate ( float angle )
	{
		Vector3 vector = new Vector3 ( x, y, 0 );
		angle *= 180.0f/Mathf.PI; 
		vector = Quaternion.AngleAxis(angle, Vector3.forward) * vector;
		this.x = vector.x;
		this.y = vector.y;
	}

    public override bool Equals ( object other )
    {
        if ( ! ( other is PVector ) )
            return false;

        PVector vector = ( PVector ) other;
        return this.x.Equals ( vector.x ) && this.y.Equals ( vector.y );
    }
	
    public static PVector Normalize ( PVector value )
    {
        float num = PVector.Magnitude ( value );

        if ( num > kEpsilon )
            return value / num;

        return PVector.zero;
    }

    public void normalize ()
    {
        float num = PVector.Magnitude ( this );

        if ( num > kEpsilon ) {
            var tmp = PVector.Normalize ( this );
			x = tmp.x;
			y = tmp.y;
        }
        else {
            x = 0;
            y = 0;
        }
    }

    public float[] ToArray ()
    {
        return new float[] {this.x, this.y};
    }
	
	public override string ToString ()
	{
		return string.Format ( "({0:F1}, {1:F1})", this.x, this.y );
	}

	public string ToString ( int precision )
	{
		return string.Format ( "({0:F" + precision + "}, {1:F" + precision + "})", this.x, this.y );
	}

    public string ToString ( string format )
    {
        return string.Format ( "({0}, {1})", this.x.ToString ( format ), this.y.ToString ( format ) );
    }
	
    public static float Dot ( PVector lhs, PVector rhs )
    {
        return lhs.x * rhs.x + lhs.y * rhs.y;
    }
    public float Dot ( PVector other )
    {
        return this.x * other.x + this.y * other.y;
    }

    public static PVector Project ( PVector vector, PVector onNormal )
    {
        float num = PVector.Dot ( onNormal, onNormal );

        if ( num < kEpsilon )
            return PVector.zero;

        return onNormal * PVector.Dot ( vector, onNormal ) / num;
    }

    public static PVector Exclude ( PVector excludeThis, PVector fromThat )
    {
        return fromThat - PVector.Project ( fromThat, excludeThis );
    }


    public static float Distance ( PVector a, PVector b )
    {
        PVector vector = new PVector ( a.x - b.x, a.y - b.y );
        return Mathf.Sqrt ( vector.x * vector.x + vector.y * vector.y );
    }
	
    public static float Magnitude ( PVector a )
    {
        return Mathf.Sqrt ( a.x * a.x + a.y * a.y );
    }

    public static float SqrMagnitude ( PVector a )
    {
        return a.x * a.x + a.y * a.y;
    }
	
	public static PVector operator + ( PVector a, PVector b )
	{
		return new PVector ( a.x + b.x, a.y + b.y );
	}
	
    public static PVector add ( PVector a, PVector b )
    {
        return new PVector ( a.x + b.x, a.y + b.y );
    }

	public static PVector operator - ( PVector a, PVector b )
	{
		return new PVector ( a.x - b.x, a.y - b.y );
	}

	public static PVector sub ( PVector a, PVector b )
	{
		return new PVector ( a.x - b.x, a.y - b.y );
	}

    public static PVector operator - ( PVector a )
    {
        return new PVector ( -a.x, -a.y );
	}
	
    public PVector Negated ()
    {
        return new PVector ( -this.x, -this.y );
    }

    public static PVector operator * ( PVector a, float d )
    {
        return new PVector ( a.x * d, a.y * d );
    }

    public static PVector operator * ( float d, PVector a )
    {
        return new PVector ( a.x * d, a.y * d );
    }

    public static PVector operator * ( PVector a, PVector b )
    {
        return new PVector ( a.x * b.x, a.y * b.y );
    }

    public static PVector operator / ( PVector a, float d )
    {
        return new PVector ( a.x / d, a.y / d );
    }

    public PVector DividedBy ( float other )
    {
        return new PVector ( this.x / other, this.y / other );
    }

    public static bool operator == ( PVector lhs, PVector rhs )
    {
        if ( Mathf.Abs ( lhs.x - rhs.x ) > kEpsilon ) return false;

        if ( Mathf.Abs ( lhs.y - rhs.y ) > kEpsilon ) return false;

        return true;
//		return Vector.SqrMagnitude (lhs - rhs) < 9.99999944E-11f;
    }

    public static bool operator != ( PVector lhs, PVector rhs )
    {
        return ! ( lhs == rhs );
//		return Vector.SqrMagnitude (lhs - rhs) >= 9.99999944E-11f;
    }
	
	// internal conversions
	public static implicit operator Vector2 ( PVector myParam )
	{
		return new Vector2 ( myParam.x, myParam.y );
	}
	public static implicit operator PVector ( Vector2 vec2 )
	{
		return new PVector ( vec2 );
	}

	public static implicit operator Vector3 ( PVector myParam )
	{
		return new Vector3 ( myParam.x, myParam.y, 0 );
	}
	public static implicit operator PVector ( Vector3 vec3 )
	{
		return new PVector ( vec3 );
	}

}