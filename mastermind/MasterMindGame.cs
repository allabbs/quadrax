using System;
namespace mastermind
{
    class MasterMindGame
    {

        int maxGuessesAllowed = 12;         // Number of guesses allowed till a gameover.
        int guessCount = 0;                 // How many guess have been submitted.
        int numOfSpots;                     // How many placement positions there are.
        int spotVariationSize;              // Amount of variation allowed per position.
        int[] mastermindAnswerPattern;
        int[] mastermindGuessPattern;
        int[] matchResults = new int[2];

        public MasterMindGame()
        {
            numOfSpots = 4;
            spotVariationSize = 6;
            maxGuessesAllowed = 12;

            mastermindAnswerPattern = new int[numOfSpots];
        }

        public MasterMindGame(int _numOfSpots, int _spotVariationSize, int _maxGuessesAllowed)
        {
            numOfSpots = _numOfSpots;
            spotVariationSize = _spotVariationSize;
            maxGuessesAllowed = _maxGuessesAllowed;

            mastermindAnswerPattern = new int[numOfSpots];
        }

        public void playGame()
        {
            autoGenerateMastermindPattern();

            // print configuration
            MasterMindUserInterface.printSplashAndConfiguration(numOfSpots, spotVariationSize, maxGuessesAllowed);

            for (guessCount++; matchResults[0] < numOfSpots && guessCount <= maxGuessesAllowed; guessCount++)
            {
                // Get guess pattern.
                mastermindGuessPattern = MasterMindUserInterface.getGuessPattern(numOfSpots, spotVariationSize);

                // Match patterns.
                matchResults = matchPatterns(mastermindAnswerPattern, mastermindGuessPattern);

                // Give Results.
                MasterMindUserInterface.postFeedback(matchResults, guessCount, numOfSpots);
            }

            // Give Game Result
            if (matchResults[0] == numOfSpots)
            // win
            {
                MasterMindUserInterface.printWinResult();
            }
            else
            // lose
            {
                MasterMindUserInterface.printLoseResult(MasterMindUserInterface.patternToString(mastermindAnswerPattern)  );
            }
        }

        void autoGenerateMastermindPattern()
        {
            for (int x = 0; x < numOfSpots; x++)
            {
                mastermindAnswerPattern[x] = new Random().Next(1, spotVariationSize);
            }
        }

        int[] matchPatterns(int[] _answerPattern, int[] _guessPattern)
        {
            int exactMatches = 0;                                               // Number of exact matches.
            int partialMatches = 0;                                             // Number of matches not on position.
            int[] talleyMissedMatchesFromAnswer = new int[spotVariationSize];   // Buckets to group Answer positions with same value
            int[] talleyMissedMatchesFromGuess = new int[spotVariationSize];    // Buckets to group Guess positions with same value

            // Count exact matches. Talley non-exact matches in buckets.
            for (int x = 0; x < numOfSpots; x++)
            {
                if (_answerPattern[x] == _guessPattern[x])
                {
                    exactMatches++;
                }
                else
                {
                    talleyMissedMatchesFromAnswer[_answerPattern[x]]++;
                    talleyMissedMatchesFromGuess[_guessPattern[x]]++;
                }
            }

            // Count partial matches.
            for (int x = 0; x < numOfSpots; x++)
            {
                partialMatches += Math.Min(talleyMissedMatchesFromGuess[x], talleyMissedMatchesFromAnswer[x]);
            }

            int[] result = new int[] { exactMatches, partialMatches };
            return result;
        }
    }
}
