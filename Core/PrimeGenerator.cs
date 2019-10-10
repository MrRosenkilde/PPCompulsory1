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
        public IEnumerable<long> GetPrimesSequential(int from, int to)
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
            for (long i =  2; i <= to; i++)
                if (pno[i])
                    yield return i;
        }
        public IEnumerable<long> SieveOfEratosthenesParallel(int from, int to)
        {
            bool[] pno = new bool[to + 1];
            for (int i = 0; i < pno.Count(); i++) pno[i] = true;
            int sqrt = (int)Math.Sqrt(to);
            Parallel.ForEach(
                Partitioner.Create(Enumerable.Range(2,sqrt)),
                (i) =>
                {
                    if (pno[i] == true)
                    {
                        Parallel.ForEach(
                            Partitioner.Create(SieveOfEathosSequence(i, i * 2, to)),
                            (index) => pno[index] = false
                        );
                    }
                });

            List<long> primes = new List<long>();
            for (long i = from > 2 ? from : 2; i <= to; i++)
                if (pno[i])
                    yield return i;

        }
        private IEnumerable<int> SieveOfEathosSequence(int i, int j, int num)
        {
            yield return j;
            while ((j += i) <= num)
                yield return j;

        }
    }
}
