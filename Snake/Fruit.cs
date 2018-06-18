
namespace Snake
{
    public class Fruit : Point
    {
        private System.Random rng = new System.Random();
        public Fruit() : base(0, 0, 'O', 4)
        {
            FindEmptySpot();
        }

        public void FindEmptySpot()
        {
            DrawableShape shape = null;
            int tempx;
            int tempy;
            do
            {
                tempx = rng.Next(Program.ScreenWidth);
                tempy = rng.Next(Program.ScreenHeight);
            } while ((shape = Program.GameScreen.FindTopmostShapeForTile(tempx, tempy)) != null );
            x = tempx;
            y = tempy;
        }
    }
}