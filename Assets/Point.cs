public class Point
{
    protected PVector position;

    public Point()
    {
        position = new PVector();
    }

	public Point ( PVector p )
    {
        position = p;
    }

	public Point ( float x, float y )
    {
        position = new PVector ( x, y );
    }

	public PVector getPosition()
    {
        return new PVector ( position );
    }

	public PVector getPositionRef()
    {
        return position;
    }

	public void setPosition ( PVector p )
	{
		position.x = p.x;
		position.y = p.y;
    }

	public void setPosition ( float x, float y )
	{
		position.x = x;
		position.y = y;
    }
}
