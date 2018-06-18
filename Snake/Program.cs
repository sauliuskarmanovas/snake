using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Snake
{
    class Program
    {
        public static int ScreenWidth = 80;
        public static int ScreenHeight = 30;
        public static DrawableShapeList OnScreenShapes;
        public static DrawableShapeList MenuScreenShapes = new DrawableShapeList();
        private static Point cursor;
        enum GameState { MAIN_MENU, EXIT, GAME_RUNNING}
        static GameState state = GameState.MAIN_MENU;
        public static DrawableShapeList GameScreen = new DrawableShapeList();
        private static bool mapInitialized = false;

        static void Main(string[] args)
        {
            InitMainMenu();
            DrawScreen();
            while (state != GameState.EXIT)
            {
                int time = -DateTime.Now.Millisecond;
                ConsoleKey button = default(ConsoleKey);
                while (Console.KeyAvailable)
                {
                    button = Console.ReadKey(true).Key;
                }
                switch (button)
                {
                    case ConsoleKey.W:
                        cursor.y = (cursor.y - 15 - 1 + 3) % 3 + 15;
                        break;
                    case ConsoleKey.S:
                        cursor.y = (cursor.y - 15 + 1 + 3) % 3 + 15;
                        break;
                    case ConsoleKey.Enter:
                        switch ((cursor.y - 15) % 3)
                        {
                            case 0:
                                RunGame();
                                break;
                            case 1:
                                ShowInstructions();
                                break;
                            case 2:
                                state = GameState.EXIT;
                                break;
                        }
                        break;
                }
                if (button != default(ConsoleKey))
                    DrawScreen();
                time = +DateTime.Now.Millisecond;
                if (state != GameState.EXIT)
                    System.Threading.Thread.Sleep(200);
            }
        }

        private static void ShowInstructions()
        {
            MenuScreenShapes.Add(new Rect(0, 0, ScreenWidth, ScreenHeight, '?', ' ', 10));
            MenuScreenShapes.Add(new Text(10, 7, " _____          _                   _   _                 ", 11));
            MenuScreenShapes.Add(new Text(10, 8, "|_   _|        | |                 | | (_)                ", 11));
            MenuScreenShapes.Add(new Text(10, 9, "  | | _ __  ___| |_ _ __ _   _  ___| |_ _  ___  _ __  ___ ", 11));
            MenuScreenShapes.Add(new Text(10, 10, "  | || '_ \\/ __| __| '__| | | |/ __| __| |/ _ \\| '_ \\/ __|", 11));
            MenuScreenShapes.Add(new Text(10, 11, " _| || | | \\__ \\ |_| |  | |_| | (__| |_| | (_) | | | \\__ \\", 11));
            MenuScreenShapes.Add(new Text(10, 12, " \\___/_| |_|___/\\__|_|   \\__,_|\\___|\\__|_|\\___/|_| |_|___/", 11));
            MenuScreenShapes.Add(new Text(30, 15, "Use WASD keys to move", 11));
            MenuScreenShapes.Add(new Text(30, 16, "Press Esc to return to main menu", 11));
            MenuScreenShapes.Add(new Text(30, 17, "Press any key to continue...", 11));
            DrawScreen();
            Console.ReadKey(true);
            MenuScreenShapes.RemoveRange(0, 10);
        }

        private static void InitMainMenu()
        {
            Console.SetWindowSize(ScreenWidth + 1, ScreenHeight + 1);
            Console.SetBufferSize(ScreenWidth + 1, ScreenHeight + 1);
            MenuScreenShapes.Add(new Rect(0, 0, ScreenWidth, ScreenHeight, '#', ' ', 0));
            MenuScreenShapes.Add(new Text(30, 15, "Start Game", 1));
            MenuScreenShapes.Add(new Text(30, 16, "Instructions", 1));
            MenuScreenShapes.Add(new Text(30, 17, "Exit", 1));
            MenuScreenShapes.Add(cursor = new Point(28, 15, '>', 1));
            MenuScreenShapes.Add(new Text(25, 7, " _____             _        ", 1));
            MenuScreenShapes.Add(new Text(25, 8, "/  ___|           | |       ", 1));
            MenuScreenShapes.Add(new Text(25, 9, "\\ `--. _ __   __ _| | _____ ", 1));
            MenuScreenShapes.Add(new Text(25, 10, " `--. \\ '_ \\ / _` | |/ / _ \\", 1));
            MenuScreenShapes.Add(new Text(25, 11, "/\\__/ / | | | (_| |   <  __/", 1));
            MenuScreenShapes.Add(new Text(25, 12, "\\____/|_| |_|\\__,_|_|\\_\\___|", 1));
            OnScreenShapes = MenuScreenShapes;
        }

        private static void RunGame()
        {
            state = GameState.GAME_RUNNING;
            OnScreenShapes = GameScreen;
            if (!mapInitialized)
                InitGameScreen();
            Snake snake = new Snake(25, 9, 4);
            GameScreen.Add(snake);
            Fruit fruit = new Fruit();
            GameScreen.Add(fruit);
            while (state != GameState.MAIN_MENU)
            {
                int time = -DateTime.Now.Millisecond;
                ConsoleKey button = default(ConsoleKey);
                while (Console.KeyAvailable)
                {
                    button = Console.ReadKey(true).Key;
                }
                switch (button)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.A:
                    case ConsoleKey.S:
                    case ConsoleKey.D:
                        snake.Move(button);
                        break;
                    case ConsoleKey.Escape:
                        state = GameState.MAIN_MENU;
                        break;
                    default:
                        snake.Move();
                        break;
                }
                if (fruit.OverlapsTile(snake.Body[0].x, snake.Body[0].y))
                {
                    snake.Expand();
                    fruit.FindEmptySpot();
                    if(snake.Body.Count > 15)
                    {
                        state = GameState.MAIN_MENU;
                        ShowGameWonScreen();
                    }

                }
                if(GameScreen.GetRange(2, GameScreen.Count - 2).
                    FindTopmostShapeForTile(snake.Body[0].x, snake.Body[0].y) != null ||
                    snake.OverlapsSelf())
                {
                    state = GameState.MAIN_MENU;
                    ShowGameLostScreen();
                }
                DrawScreen();
                time = +DateTime.Now.Millisecond;
                if (state != GameState.MAIN_MENU)
                    System.Threading.Thread.Sleep(200);
            }
            GameScreen.Remove(snake);
            GameScreen.Remove(fruit);
            OnScreenShapes = MenuScreenShapes;
        }

        private static void ShowGameLostScreen()
        {
            GameScreen.Add(new Rect(0, 0, ScreenWidth, ScreenHeight, ' ', ' ', 10));
            GameScreen.Add(new Text(10, 7, " _____                        _           _   ", 11));
            GameScreen.Add(new Text(10, 8, "|  __ \\                      | |         | |  ", 11));
            GameScreen.Add(new Text(10, 9, "| |  \\/ __ _ _ __ ___   ___  | | ___  ___| |_ ", 11));
            GameScreen.Add(new Text(10, 10, "| | __ / _` | '_ ` _ \\ / _ \\ | |/ _ \\/ __| __|", 11));
            GameScreen.Add(new Text(10, 11, "| |_\\ \\ (_| | | | | | |  __/ | | (_) \\__ \\ |_ ", 11));
            GameScreen.Add(new Text(10, 12, " \\____/\\__,_|_| |_| |_|\\___| |_|\\___/|___/\\__|", 11));
            DrawScreen();
            Console.ReadKey(true);
            GameScreen.RemoveRange(0, 7);
        }
        private static void ShowGameWonScreen()
        {
            GameScreen.Add(new Rect(0, 0, ScreenWidth, ScreenHeight, ' ', ' ', 10));
            GameScreen.Add(new Text(10, 7, " _____                                            ", 11));
            GameScreen.Add(new Text(10, 8, "|  __ \\                                           ", 11));
            GameScreen.Add(new Text(10, 9, "| |  \\/ __ _ _ __ ___   ___  __      _____  _ __  ", 11));
            GameScreen.Add(new Text(10, 10, "| | __ / _` | '_ ` _ \\ / _ \\ \\ \\ /\\ / / _ \\| '_ \\ ", 11));
            GameScreen.Add(new Text(10, 11, "| |_\\ \\ (_| | | | | | |  __/  \\ V  V / (_) | | | |", 11));
            GameScreen.Add(new Text(10, 12, " \\____/\\__,_|_| |_| |_|\\___|   \\_/\\_/ \\___/|_| |_|", 11));
            DrawScreen();
            Console.ReadKey(true);
            GameScreen.RemoveRange(0, 7);
        }

        private static void InitGameScreen()
        {
            GameScreen.Add(new Line(0, 0, 0, ScreenHeight - 1, '█', 1));
            GameScreen.Add(new Line(ScreenWidth - 1, 0, ScreenWidth - 1, ScreenHeight - 1, '█',1));
            GameScreen.Add(new Line(1, 0, ScreenWidth - 2, 0, '█',1));
            GameScreen.Add(new Line(1, ScreenHeight - 1, ScreenWidth - 2, ScreenHeight - 1, '█',1));
            GameScreen.Add(new Line(10, 10, ScreenWidth - 10, ScreenHeight - 10, '█', 1));
            GameScreen.Add(new Line(10, ScreenHeight - 10, ScreenWidth - 10, 10, '█', 1));
            mapInitialized = true;
            DrawScreen();
        }

        private static void DrawScreen()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ScreenHeight; i++)
            {
                for (int j = 0; j < ScreenWidth; j++)
                {
                    char c = OnScreenShapes.FindTopmostTile(j, i);
                    sb.Append(c == '\0' ? ' ' : c);
                }
                sb.Append('\n');
            }
            Console.Clear();
            Console.Write(sb.ToString());
        }
    }
}
