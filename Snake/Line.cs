using System.Drawing;
namespace Snake
{
    internal class Line : DrawableShape
    {
        private int StartX;
        private int StartY;
        private int EndX;
        private int EndY;
        private char c;
        private float slope;
        public Line(int StartX, int StartY, int EndX, int EndY, char c) : this(StartX, StartY, EndX, EndY, c, 0)
        {

        }

        public Line(int StartX, int StartY, int EndX, int EndY, char c, int zValue)
        {
            if(StartX > EndX)
            {
                this.StartX = EndX;
                this.EndX = StartX;

                this.StartY = EndY;
                this.EndY = StartY;

            }
            else
            {
                this.StartX = StartX;
                this.EndX = EndX;
            
                this.StartY = StartY;
                this.EndY = EndY;

            }
            
            this.c = c;
            this.zValue = zValue;
            if(EndX != StartX)
                slope = (EndY - StartY) / (float)(EndX - StartX);
        }

        public override bool OverlapsTile(int w, int h)
        {
            if (!((StartX <= w && w <= EndX) && (StartY <= h && h <= EndY) ||
                (StartX <= w && w <= EndX) && (EndY <= h && h <= StartY) ||
                (EndX <= w && w <= StartX) && (StartY <= h && h <= EndY) ||
                (EndX <= w && w <= StartX) && (EndY <= h && h <= StartY)))
                    return false;
            if (EndX != StartX)
            {
                return System.Math.Round((w - StartX) * slope, 0) == h - StartY;
            }
            return StartY <= h && h <= EndY; 
            
        }

        public override char TileAt(int w, int h)
        {
            return OverlapsTile(w, h) ? c : '\0';
        }
    }
}