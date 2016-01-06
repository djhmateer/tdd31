using System;
using Xunit;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");
        }

        static int NumberOfCombinations(int pence)
        {
            return 2;
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
    }
}
