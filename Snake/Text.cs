using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Text : DrawableShape
    {
        private int x;
        private int y;
        private string text;

        public Text(int x, int y, string text) : this(x, y, text, 0)
        {

        }

        public Text(int x, int y, string text, int zValue)
        {
            this.x = x;
            this.y = y;
            this.text = text;
            this.zValue = zValue;
        }

        public override bool OverlapsTile(int w, int h)
        {
            return (h == y && x <= w && w < (x + text.Length));
        }

        public override char TileAt(int w, int h)
        {
            if (!OverlapsTile(w, h))
                return '\0';
            return text[w - x];
        }
    }
}
