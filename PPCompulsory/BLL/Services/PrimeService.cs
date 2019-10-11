using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPCompulsory.BLL.Services
{
    public class PrimeService : IPrime
    {
        //Complexity O( n^2 )
        public IEnumerable<long> GetPrimeSequential(int from, int to)
            => SieveOfEratosthenes(from, to);
        private IEnumerable<long> SieveOfEratosthenes(int from, int to)
        {
            bool[] pno = new bool[to + 1];

            for (int i = 0; i < pno.Count(); i++) pno[i] = true;

            for (int i = 2; i * i <= to; i++)
            {
                if (pno[i] == true)
                {
                    for (int j = i * 2; j <= to; j += i)
                        pno[j] = false;
                }
            }
            for (long i = 2; i <= to; i++)
                if (pno[i])
                    yield return i;
        }

        public async Task<List<long>> GetPrimesParallel(int from, int to)
        => await Task.Run(() =>
        {
            if (to < from || to <= 0 || from < 0)
                return new List<long> { 0 };


            List<long> primes = new List<long>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 27, 29, 31 };
            for (long i = 37; i < to; i += 2) //only odd numbers can be prime
             {
                var isPrime = false;
                Parallel.ForEach(
                    Partitioner.Create(primes),
                    (value, state) => // check if i is prime, run it in parallel beacuse results are independent
                     {
                        if (!state.IsStopped)
                            if (i % value == 0)
                            {
                                isPrime = false;
                                state.Stop();
                            }
                    }
                );
                if (isPrime)
                {
                    primes.Add(i);
                }
            }
            var results = primes.Where(x => x >= from && x <= to);
            return results.ToList();
        });
    }
}
