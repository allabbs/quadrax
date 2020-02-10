using System;
namespace mastermind
{
    class MasterMindUserInterface
    {
        public static void printSplashAndConfiguration(int _numOfSpots, int _spotVariationSize, int _maxGuessesAllowed)
        {
            Console.WriteLine("Mastermind Game");
            Console.WriteLine("");
            Console.WriteLine("In this game, there are " + _numOfSpots + " pegs and " + _spotVariationSize + " peg types (0-" + (_spotVariationSize - 1) + ").");
            Console.WriteLine("You will have " + _maxGuessesAllowed + " turns to guess the correct pattern.");
            Console.WriteLine("");
            Console.WriteLine("Enter guesses in this format --> 5 1 3 1");
            Console.WriteLine("Guess results follow this format... exact match = O; partial matches = x; miss = -");
            Console.WriteLine("");
        }

        public static int[] getGuessPattern(int _numOfSpots, int _spotVariationSize)
        {
            Console.Write("Enter your guess...");

            string s = Console.ReadLine();
            string[] sArray = s.Split(' ');
            int[] result = Array.ConvertAll<string, int>(sArray, int.Parse);

            return result;
        }

        public static string patternToString(int[] _pattern)
        {
            string s = string.Empty;
            foreach( int i in _pattern)
            {
                s += (" " + i.ToString()).Trim();
            }

            return s;
        }

        public static void postFeedback(int[] _matchResults, int _guessCount, int _numOfSpots)
        {
            string result = convertResultToString(_matchResults, _numOfSpots);

            Console.WriteLine(result + " = Guess " + _guessCount + " results.");
            Console.WriteLine();
        }

        static string convertResultToString(int[] _matchResults, int _numOfSpots)
        {
            string s = string.Empty;
            int count = 0;

            // add exact matches
            for (int i = 0; i < _matchResults[0]; i++)
            {
                s += "O";
                count++;
            }

            // add partial matches
            for (int i = 0; i < _matchResults[1]; i++)
            {
                s += "x";
                count++;
            }

            // add misses
            for (; count < _numOfSpots;)
            {
                s += "-";
                count++;
            }

            return s;
        }

        public static void printLoseResult(string _winPattern)
        {
            Console.WriteLine("YOU LOSE! - Pattern was " + _winPattern);
        }
        public static void printWinResult()
        {
            Console.WriteLine("YOU WIN!");
        }

    }
}
