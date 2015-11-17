class VeinNode : Point
{
    VeinNode()
    {
        position = new PVector();
    }

    VeinNode ( PVector p )
    {
        position = p;
    }

    VeinNode ( float x, float y )
    {
        position = new PVector ( x, y );
    }
}
