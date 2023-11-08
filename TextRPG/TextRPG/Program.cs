using System.Xml.Linq;

namespace TextRPG
{
    internal class Program
    {
        public delegate void KeyInputHandler(ConsoleKey key);

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            GameManager gameManager = new GameManager();
            GameScene scene = new GameScene();
            //Player player = new Player();

            gameManager.Scene = scene;

            KeyInputHandler keyInputHandle = gameManager.GetInput;
            gameManager.RunGame();

            while (gameManager.isPlay)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    keyInputHandle(key);
                }
                Thread.Sleep(100);
            }
        }
    }
}