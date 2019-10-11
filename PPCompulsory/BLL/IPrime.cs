using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPCompulsory.BLL
{
    public interface IPrime
    {
        IEnumerable<long> GetPrimeSequential(int from, int to);
        Task<List<long>> GetPrimesParallel(int from, int to);
    }
}
