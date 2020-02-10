using System;

namespace mastermind
{
    // Playthrough for mastermind game.
    


    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MasterMindGame mm = new MasterMindGame();
                mm.playGame();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured. Exiting...");
                Console.WriteLine(e);
            }
        }
    }
}
