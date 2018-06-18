using System;

namespace Snake
{
    public abstract class DrawableShape
    {
        public int zValue;
        public abstract bool OverlapsTile(int w, int h);
        public abstract char TileAt(int w, int h);
    }
}