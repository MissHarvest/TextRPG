namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                for (int i = 1; i < 50; ++i)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("\u2501");
                }

                for (int i = 1; i < 49; ++i)
                {
                    Console.SetCursorPosition(i, 25);
                    Console.Write("\u2501");
                }

                for(int i = 1; i < 25; ++i)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("\u2503");
                }

                for (int i = 1; i < 25; ++i)
                {
                    Console.SetCursorPosition(50, i);
                    Console.Write("\u2503");
                }

                Console.SetCursorPosition(0, 0);
                Console.Write("\u250F");

                Console.SetCursorPosition(50, 0);
                Console.Write("\u2513");

                Console.SetCursorPosition(0, 25);
                Console.Write("\u2517");

                Console.SetCursorPosition(50, 25);
                Console.Write("\u251B");

                Thread.Sleep(100);
            }
        }
    }
}