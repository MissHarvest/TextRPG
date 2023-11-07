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

            gameManager.Scene = scene;

            KeyInputHandler keyInputHandle = scene.GetInput;
            keyInputHandle += gameManager.GetInput;

            scene.DrawScene();
            // gameManager.ChangeState()
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