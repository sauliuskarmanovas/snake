namespace Snake
{
    public class Point : DrawableShape
    {
        public int x;
        public int y;
        public char c;

        public Point(int x, int y, char c) : this(x, y, c, 0)
        {

        }

        public Point(int x, int y, char c, int zValue)
        {
            this.x = x;
            this.y = y;
            this.c = c;
            this.zValue = zValue;
        }

        public override bool OverlapsTile(int w, int h)
        {
            return x == w && y == h;
        }

        public override char TileAt(int w, int h)
        {
            return OverlapsTile(w, h) ? c : '\0';
        }
    }
}