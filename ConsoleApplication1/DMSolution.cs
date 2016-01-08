using System;
using System.Diagnostics;
using Xunit;

namespace ConsoleApplication1
{
    // In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:
    // 1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
    // It is possible to make £2 in the following way:
    // 1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
    // How many different ways can £2 be made using any number of coins?

    // Current Perf to find answer of 73,682 combinations:
    // 2,886,725 iterations.. 40ms

    // Ideas for improving the code
    // - performance is good at 40ms
    // - code looks confusing!

    // is it possible to get the answer with combinations left? eg if 0*onePencePiece then can't make 3 pence
    // can we get to the answer (ie min)
    // [done]are we over the answer (ie max)
    // reverse the algorithm, start with larger coins?

    public class DMSolution
    {
        static void Main1()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine(NumberOfCombinations(200));
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            var elapsedTime = $"{ts.TotalMilliseconds:n0}"; // commas but no decimal places.  String interpolation
            Console.WriteLine(elapsedTime + "ms");
        }

        static int NumberOfCombinations(int goalAmountOfMoneyInPence)
        {
            int combinationsFound = 0;
            ulong totalIterations = 0;
            for (int onePencePieces = 0; onePencePieces <= goalAmountOfMoneyInPence; onePencePieces++)
            {
                for (int twoPencePieces = 0; twoPencePieces <= goalAmountOfMoneyInPence / 2; twoPencePieces++)
                {
                    if (AreCurrentFixedCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces)) continue;
                    for (int fivePencePieces = 0; fivePencePieces <= goalAmountOfMoneyInPence / 5; fivePencePieces++)
                    {
                        if (AreCurrentFixedCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces, fivePencePieces)) continue;
                        for (int tenPencePieces = 0; tenPencePieces <= goalAmountOfMoneyInPence / 10; tenPencePieces++)
                        {
                            if (AreCurrentFixedCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces, fivePencePieces, tenPencePieces)) continue;
                            for (int twentyPencePieces = 0; twentyPencePieces <= goalAmountOfMoneyInPence / 20; twentyPencePieces++)
                            {
                                if (AreCurrentFixedCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces, fivePencePieces, tenPencePieces, twentyPencePieces)) continue;
                                for (int fifetyPencePieces = 0; fifetyPencePieces <= goalAmountOfMoneyInPence / 50; fifetyPencePieces++)
                                {
                                    if (AreCurrentFixedCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces, fivePencePieces, tenPencePieces, twentyPencePieces,
                                        fifetyPencePieces)) continue;

                                    for (int poundCoins = 0; poundCoins <= goalAmountOfMoneyInPence / 100; poundCoins++)
                                    {
                                        if (AreCurrentFixedCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces, fivePencePieces, tenPencePieces, twentyPencePieces,
                                            fifetyPencePieces, poundCoins)) continue;
                                        if ((onePencePieces * 1) + (twoPencePieces * 2) + (fivePencePieces * 5) + (tenPencePieces * 10) + (twentyPencePieces * 20) +
                                            (fifetyPencePieces * 50) + (poundCoins * 100) == goalAmountOfMoneyInPence)
                                            combinationsFound++;
                                        //Console.WriteLine(onePencePieces + " " + twoPencePieces + " " + fivePencePieces);
                                        totalIterations++;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("totalIterations: " + $"{totalIterations:n0}");
            if (goalAmountOfMoneyInPence == 200) combinationsFound += 1; // 2 pound coin check.  Function only designed to check up to 200p
            return combinationsFound;
        }

        private static bool AreCurrentFixedCoinCombinationsOverTheGoalAmount(int goalAmountOfMoneyInPence, int onePencePieces, int twoPencePieces = 0, int fivePencePieces = 0, int tenPencePieces = 0, 
            int twentyPencePieces = 0, int fifetyPencePieces = 0, int poundCoins = 0)
        {
            return (onePencePieces * 1) + (twoPencePieces * 2) + (fivePencePieces * 5) + (tenPencePieces * 10) + (twentyPencePieces * 20)
                + (fifetyPencePieces * 50) + (poundCoins * 100) > goalAmountOfMoneyInPence;
        }

        // AreCurrentFixedCoinCombinationsOverTheGoalAmount...max check over - goal is to cut down total iterations
        [Fact]
        public void Given3pAsGoal_AndWeAreOn2OnePencesAnd1TwoPencePiecesIE4pence_ShouldReturnTrue()
        {
            var result = AreCurrentFixedCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence: 3, onePencePieces: 2, twoPencePieces: 1);
            Assert.True(result);
        }

        // 111
        // 21
        [Fact]
        public void Given3pence_ShouldReturn2DifferentWaysToGetThatAmount()
        {
            int result = NumberOfCombinations(3);
            Assert.Equal(2, result);
        }

        //1111
        //22
        //211
        [Fact]
        public void Given4pence_ShouldReturn3DifferentWaysToGetThatAmount()
        {
            int result = NumberOfCombinations(4);
            Assert.Equal(3, result);
        }

        //11111
        //221
        //2111
        //5
        [Fact]
        public void Given5pence_ShouldReturn4DifferentWaysToGetThatAmount()
        {
            int result = NumberOfCombinations(5);
            Assert.Equal(4, result);
        }

        [Fact]
        public void Given200pence_ShouldReturn73682DifferentWaysToGetThatAmount()
        {
            int result = NumberOfCombinations(200);
            Assert.Equal(73682, result);
        }
    }
}
