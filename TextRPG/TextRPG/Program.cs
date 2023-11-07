using System.Xml.Linq;

namespace TextRPG
{
    internal class Program
    {
        public delegate void KeyInputHandler(ConsoleKey key);

        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            GameScene scene = new GameScene();
            Player player = new Player();

            gameManager.Scene = scene;
            gameManager.Player = player;

            KeyInputHandler keyInputHandle = scene.GetInput;
            keyInputHandle += gameManager.GetInput;

            scene.DrawScene();
            
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