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
            string elapsedTime = string.Format("{0:n0}", ts.TotalMilliseconds); // commas but no decimal places
            // 13.2s
            Console.WriteLine(elapsedTime + "ms");
        }

        static int NumberOfCombinations(int amountOfMoneyInPence)
        {
            // brute force and try every combination up to max possible for highest
            int combinationsFound = 0;
            for (int onePencePieces = 0; onePencePieces <= amountOfMoneyInPence; onePencePieces++)
            {
                for (int twoPencePieces = 0; twoPencePieces <= amountOfMoneyInPence/2+1; twoPencePieces++)
                {
                    for (int fivePencePieces = 0; fivePencePieces < amountOfMoneyInPence/5+1; fivePencePieces++)
                    {
                        for (int tenPencePieces = 0; tenPencePieces < amountOfMoneyInPence/10+1; tenPencePieces++)
                        {
                            for (int twentyPencePieces = 0; twentyPencePieces < amountOfMoneyInPence/20+1; twentyPencePieces++)
                            {
                                for (int fiftyPencePieces = 0; fiftyPencePieces < amountOfMoneyInPence/50+1; fiftyPencePieces++)
                                {
                                    for (int poundCoins = 0; poundCoins < amountOfMoneyInPence/100+1; poundCoins++)
                                    {
                                        if ((onePencePieces * 1) + (twoPencePieces * 2) + (fivePencePieces * 5) + (tenPencePieces * 10) + 
                                            (twentyPencePieces * 20) + (fiftyPencePieces * 50) + (poundCoins * 100) == amountOfMoneyInPence)
                                            combinationsFound++;
                                    }
                                }
                            }
                            
                        }
                       
                    }
                    
                }
            }
            if (amountOfMoneyInPence == 200) combinationsFound += 1; // 2 pound coin check.  Function only designed to check up to 200p
            return combinationsFound;
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
