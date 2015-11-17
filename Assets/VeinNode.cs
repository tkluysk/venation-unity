public class VeinNode : Point
{
    VeinNode()
    {
        position = new PVector();
    }

    public VeinNode ( PVector p )
    {
        position = p;
    }

    public VeinNode ( float x, float y )
    {
        position = new PVector ( x, y );
    }
}
