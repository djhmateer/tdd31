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

    public class Program
    {
        static void Main()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine(NumberOfCombinations(200));
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            var elapsedTime = string.Format("{0:n0}", ts.TotalMilliseconds); // commas but no decimal places
            // 13.2s laptop, 7.8s desktop
            // 2,912,616,630 totalIterations
            // with max check now: 2,886,725 iterations
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
                    if (AreCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces)) continue;
                    for (int fivePencePieces = 0; fivePencePieces <= goalAmountOfMoneyInPence / 5; fivePencePieces++)
                    {
                        if (AreCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces, fivePencePieces)) continue;
                        for (int tenPencePieces = 0; tenPencePieces <= goalAmountOfMoneyInPence / 10; tenPencePieces++)
                        {
                            if (AreCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces, fivePencePieces, tenPencePieces)) continue;
                            for (int twentyPencePieces = 0; twentyPencePieces <= goalAmountOfMoneyInPence / 20; twentyPencePieces++)
                            {
                                if (AreCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces, fivePencePieces, tenPencePieces, twentyPencePieces)) continue;
                                for (int fifetyPencePieces = 0; fifetyPencePieces <= goalAmountOfMoneyInPence / 50; fifetyPencePieces++)
                                {
                                    if (AreCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces, fivePencePieces, tenPencePieces, twentyPencePieces,
                                        fifetyPencePieces)) continue;

                                    for (int poundCouns = 0; poundCouns <= goalAmountOfMoneyInPence / 100; poundCouns++)
                                    {
                                        if (AreCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence, onePencePieces, twoPencePieces, fivePencePieces, tenPencePieces, twentyPencePieces,
                                            fifetyPencePieces, poundCouns)) continue;
                                        if ((onePencePieces * 1) + (twoPencePieces * 2) + (fivePencePieces * 5) + (tenPencePieces * 10) + (twentyPencePieces * 20) +
                                            (fifetyPencePieces * 50) + (poundCouns * 100) == goalAmountOfMoneyInPence)
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
            Console.WriteLine("totalIterations: " + string.Format("{0:n0}", totalIterations));
            if (goalAmountOfMoneyInPence == 200) combinationsFound += 1; // 2 pound coin check.  Function only designed to check up to 200p
            return combinationsFound;
        }

        private static bool AreCoinCombinationsOverTheGoalAmount(int goalAmountOfMoneyInPence, int onePencePieces, int twoPencePieces = 0, int fivePencePieces = 0, int tenPencePieces = 0, 
            int twentyPencePieces = 0, int fifetyPencePieces = 0, int poundCoins = 0)
        {
            return (onePencePieces * 1) + (twoPencePieces * 2) + (fivePencePieces * 5) + (tenPencePieces * 10) + (twentyPencePieces * 20)
                + (fifetyPencePieces * 50) + (poundCoins * 100) > goalAmountOfMoneyInPence;
        }

        // max check over? - goal is to cut down total iterations
        [Fact]
        public void Given3pAsGoal_AndWeAreOn2OnePencesAnd1TwoPencePiece_ShouldReturnTrue()
        {
            var result = AreCoinCombinationsOverTheGoalAmount(goalAmountOfMoneyInPence: 3, onePencePieces: 2, twoPencePieces: 1);
            Assert.True(result);
        }

        // is it possible to get the answer with combinations left? eg if 0*onePencePiece then can't make 3 pence
        // can we get to the answer (ie min)
        // [done]are we over the answer (ie max)
        // reverse the algorithm, start with larger coins?

        // 111
        // 21
        [Fact]
        public void Given3pence_ShouldReturn2DifferentWaysToGetThatAmount()
        {
            Debug.WriteLine("test");
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
