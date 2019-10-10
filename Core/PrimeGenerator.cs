using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPCompulsory1.Logic
{
    public class PrimeGenerator
    {
        //Complexity O(n)
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
        //Complexity O( n^2 )
        public async Task<List<long>> GetPrimesSequential(int from, int to)
         => await Task.Run(() =>
         {
             List<long> results = new List<long>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 27, 29, 31 };

             for (long i = 37; i < to; i += 2)
                 if (isPrime(i, results))
                     results.Add(i);
             return results.Where(x => x <= to && x >= from).ToList();
         });
        private bool isPrime(long index, List<long> primesBelowIndex)
        {

            foreach (long j in primesBelowIndex)
                if (index % j == 0)
                    return false;
            return true;
        }
    }
}
