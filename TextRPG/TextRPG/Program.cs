using System.Xml.Linq;

namespace TextRPG
{
    internal class Program
    {
        public delegate void KeyInputHandler(ConsoleKey key);

        static void Main(string[] args)
        {
            // GameManager
            
            GameScene scene = new GameScene();
            KeyInputHandler keyInputHandle = scene.GetInput;

            scene.DrawScene();

            while (true)
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