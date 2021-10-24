using System;

namespace _02_text_rpg_refactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager game = new GameManager();

            while (true)
            {
                game.Process();
            }
        }
    }
}
