using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake : DrawableShape
    {
        public List<Point> Body;
        ConsoleKey OldDirection = ConsoleKey.D;
        int PreviousTailPositionX;
        int PreviousTailPositionY;
        public Snake(int x, int y, int length)
        {
            if (length < 4)
                length = 4;
            Body = new List<Point>();
            Body.Add(new Point(x, y, 's',3));
            Body.Add(new Point(x, y, 'n',2));
            for (int i = 2; i < length; i++)
            {
                Body.Add(new Point(x, y, i == length - 1?'k':'e',2));
            }
        }

        public override bool OverlapsTile(int w, int h)
        {
            foreach (var item in Body)
            {
                if (item.OverlapsTile(w, h))
                    return true;
            }
            return false;
        }

        public override char TileAt(int w, int h)
        {
            foreach (var item in Body)
            {
                if (item.OverlapsTile(w, h))
                    return item.c;
            }
            return '\0';
        }

        public void Move()
        {
            Move(OldDirection);
        }
        public void Move(ConsoleKey button)
        {
            if (button == ConsoleKey.W && OldDirection == ConsoleKey.S ||
                   OldDirection == ConsoleKey.W && button == ConsoleKey.S ||
                   button == ConsoleKey.D && OldDirection == ConsoleKey.A ||
                   OldDirection == ConsoleKey.D && button == ConsoleKey.A)
                button = OldDirection;
            PreviousTailPositionX = Body.Last().x;
            PreviousTailPositionY = Body.Last().y;
            for (int i = Body.Count - 1; i > 0 ; i--)
            {
                Body[i].x = Body[i - 1].x;
                Body[i].y = Body[i - 1].y;
            }
            switch (button)
            {
                case ConsoleKey.W:
                    Body[0].y = (Body[0].y - 1 + Program.ScreenHeight) % Program.ScreenHeight;
                    break;
                case ConsoleKey.D:
                    Body[0].x = (Body[0].x + 1 + Program.ScreenWidth) % Program.ScreenWidth;
                    break;
                case ConsoleKey.S:
                    Body[0].y = (Body[0].y + 1) % Program.ScreenHeight;
                    break;
                case ConsoleKey.A:
                    Body[0].x = (Body[0].x - 1 + Program.ScreenWidth) % Program.ScreenWidth;
                    break;
            }
            OldDirection = button;
        }

        public void Expand()
        {
            Body.Last().c = 'e';
            Body.Add(new Point(PreviousTailPositionX, PreviousTailPositionY, 'k', 2));
        }

        internal bool OverlapsSelf()
        {
            for (int i = 1; i < Body.Count; i++)
            {
                if (Body[0].x == Body[i].x && Body[0].y == Body[i].y)
                    return true;
            }
            return false;
        }
    }
}
