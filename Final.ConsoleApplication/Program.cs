using System;
using Final.Logic;

namespace Final.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame game = new Game();

            game.Run();
        }
    }
}
