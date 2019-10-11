using PPCompulsory1.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PPCompulsory.BLL.Services
{
    public class PrimeService : IPrime
    {
        private readonly PrimeGenerator pg;
        public PrimeService() { pg = new PrimeGenerator(); }
        public async Task<IEnumerable<long>> GetPrimeSequentialAsync(int from, int to)
         => await pg.GetPrimesSequential(from, to);

        public async Task<List<long>> GetPrimesParallel(int from, int to)
        => await pg.GetPrimesParallel(from, to);
    }
}
