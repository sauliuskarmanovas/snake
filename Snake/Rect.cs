namespace Snake
{
    internal class Rect : DrawableShape
    {
        private int TopLeftX;
        private int TopLeftY;
        private int Width;
        private int Height;
        private char Border;
        private char Fill;

        public Rect(int TopLeftX, int TopLeftY, int Width, int Height, char Border, char Fill) : this(TopLeftX, TopLeftY, Width, Height, Border, Fill, 0)
        {

        }

        public Rect(int TopLeftX, int TopLeftY, int Width, int Height, char Border, char Fill, int zValue)
        {
            this.TopLeftX = TopLeftX;
            this.TopLeftY = TopLeftY;
            this.Width = Width;
            this.Height = Height;
            this.Border = Border;
            this.Fill = Fill;
            this.zValue = zValue;
        }

        public override bool OverlapsTile(int w, int h)
        {
            return (TopLeftX <= w && w < TopLeftX + Width) && (TopLeftY <= h && h < TopLeftY + Height);
        }

        public override char TileAt(int w, int h)
        {
            if ((w == TopLeftX || w == TopLeftX + Width - 1) || (h == TopLeftY || h == TopLeftY + Height - 1))
                return Border;
            if (OverlapsTile(w, h))
                return Fill;
            return '\0';
        }
    }
}