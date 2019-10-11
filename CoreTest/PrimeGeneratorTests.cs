using PPCompulsory1.Logic;
using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CoreTest
{
    public class PrimeGeneratorTests
    {
        [Fact]
        public async Task PrimesBelow10Async()
        {
            var expected = new long[] { 2, 3, 5, 7 }.AsEnumerable();
            var actual = await new PrimeGenerator().GetPrimesParallel(0, 10);
            Assert.True(expected.SequenceEqual(actual));

        }
        [Theory]
        [InlineData(0,1_000_000)]
        [InlineData(1_000_000, 2_000_000)]
        [InlineData(0,10_000_000)]
        [InlineData(10_000_000, 20_000_000)]
        public async Task SequentialAndParallelAgreesAsync(int from,int to)
        {
            var pg = new PrimeGenerator();
            var sequential =await pg.GetPrimesSequential(from, to);
            var parallel = await pg.GetPrimesParallel(from, to);
            Assert.Equal(sequential.Count, parallel.Count);
        }
    }
    class int_pair
    {
        public int Int1 { get; set; }
        public int Int2 { get; set; }
    }
}
