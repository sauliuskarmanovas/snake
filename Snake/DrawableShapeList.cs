using System.Collections.Generic;
namespace Snake
{
    public class DrawableShapeList : List<DrawableShape>
    {
        //Adds to the list based on shape's zValue
        public new void Add(DrawableShape shape)
        {
            int index = FindIndex(s => shape.zValue >= s.zValue);
            if (index != -1)
                Insert(index, shape);
            else
                Insert(0, shape);
        }

        public DrawableShape FindTopmostShapeForTile(int w, int h)
        {
            foreach (var shape in this)
            {
                if (shape.OverlapsTile(w, h))
                    return shape;
            }
            return null;
        }
        public char FindTopmostTile(int w, int h)
        {
            foreach (var shape in this)
            {
                if (shape.OverlapsTile(w, h))
                    return shape.TileAt(w,h);
            }
            return '\0';
        }
        public new DrawableShapeList GetRange(int index, int Count)
        {
            DrawableShapeList shapes = new DrawableShapeList();
            for (int i = 0; i < Count; i++)
            {
                shapes.Add(this[index + i]);
            }
            return shapes;
        }
    }
}