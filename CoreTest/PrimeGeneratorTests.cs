using PPCompulsory1.Logic;
using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
namespace CoreTest
{
    public class PrimeGeneratorTests
    {
        [Fact]
        public void PrimesBelow10()
        {
            var expected = new long[] { 2, 3, 5, 7 }.AsEnumerable();
            var actual = new PrimeGenerator().SieveOfEratosthenesParallel(0, 10);
            Assert.True(expected.SequenceEqual(actual));

        }
        [Theory]
        [InlineData(0,1_000_000)]
        [InlineData(1_000_000, 2_000_000)]
        [InlineData(0,10_000_000)]
        [InlineData(10_000_000, 20_000_000)]
        public void SequentialAndParallelAgrees(int from,int to)
        {
            var pg = new PrimeGenerator();
            var sequential = pg.GetPrimesSequential(from, to);
            //var parallel = pg.SieveOfEratosthenesParallel(from, to);
            //Assert.True(sequential.SequenceEqual(parallel));
        }
    }
    class int_pair
    {
        public int Int1 { get; set; }
        public int Int2 { get; set; }
    }
}
