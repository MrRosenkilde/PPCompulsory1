using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PPCompulsory1.Logic
{
    public class PrimeGenerator
    {
        //Complexity O(n)
        //public async Task<List<long>> GetPrimesParallel(int from, int to)
        // => await Task.Run(() =>
        // {
        //     if (to < from || to <= 0 || from < 0)
        //         return new List<long> { 0 };


        //     List<long> primes = new List<long>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 27, 29, 31 };
        //     for (long i = 37; i < to; i += 2) //only odd numbers can be prime
        //     {
        //         var isPrime = false;
        //         Parallel.ForEach(
        //             Partitioner.Create(primes),
        //             (value, state) => // check if i is prime, run it in parallel beacuse results are independent
        //             {
        //                 if (!state.IsStopped)
        //                     if (i % value == 0)
        //                     {
        //                         isPrime = false;
        //                         state.Stop();
        //                     }
        //             }
        //         );
        //         if (isPrime)
        //         {
        //             primes.Add(i);
        //         }
        //     }
        //     var results = primes.Where(x => x >= from && x <= to);
        //     return results.ToList();
        // });
        //Complexity O( n^2 )
        public async Task<List<long>> GetPrimesSequential(int from, int to)
        => await Task.Factory.StartNew(() => {
            return PrimesSequential(from, to).ToList();
        });
        private IEnumerable<long> PrimesSequential(int from, int to)
        {
            bool[] pno = new bool[to + 1];

            for (int i = 0; i < pno.Count(); i++) pno[i] = true;
            int sqrt = (int)Math.Sqrt(to);
            for (int i = 2; i <= sqrt; i++)
            {
                if (pno[i] == true)
                {
                    for (int j = i * 2; j <= to; j += i)
                        pno[j] = false;
                }
            }
            for (long i = from < 2 ? 2 : from; i <= to; i++)
                if (pno[i])
                    yield return i;
        }
        public async Task<List<long>> GetPrimesParallel(int from, int to)
        => await Task.Factory.StartNew(() =>
        {
            return PrimesParallAsync(from, to).ToList();
        });
        private IEnumerable<long> PrimesParallAsync(int from, int to)
        {
            bool[] pno = new bool[to + 1];
            for (int i = 0; i < pno.Count(); i++) pno[i] = true;
            int sqrt = (int)Math.Sqrt(to);
            //Parallel.ForEach(
            //    Partitioner.Create(Enumerable.Range(2, sqrt)),
            //    (i) =>
            //    {
            //    if (pno[i] == true)
            //    {
            //        for (int j = i*2 ; j <= to; j += i)
            //            {
            //                pno[j] = false;
            //            }
            //        }
            //    });
            //for (int i = 2; i <= sqrt; i += 4)
            //{
            //    var t = Task.Factory.StartNew(() =>
            //    {
            //        MarkNotPrimes(ref pno, i, to);
            //    });
            //    var t2 = Task.Factory.StartNew(() =>
            //    {
            //        MarkNotPrimes(ref pno, i + 1, to);
            //    });
            //    var t3 = Task.Factory.StartNew(() =>
            //    {
            //        MarkNotPrimes(ref pno, i + 2, to);
            //    });
            //    Task.WaitAll(t, t2, t3);
            //}
            int threads = 24;
            for (int i = 2; i <= sqrt; i += threads)
            {
                Task[] tasks = new Task[threads];
                for (int j = 0; j < threads; j++)
                {
                    int h = i + j;
                    var t = Task.Factory.StartNew(() =>
                    {
                        MarkNotPrimes(pno, h, to);
                    });
                    tasks[j] = t;
                }
                Task.WaitAll(tasks);
            }
            for (long i = from > 2 ? from : 2; i <= to; i++)
                if (pno[i])
                    yield return i;
            
        }
        private void MarkNotPrimes(bool[] pno, int i,int to) 
        {
            if (pno[i] == true)
            {
                for (int j = i * 2; j <= to; j += i)
                    pno[j] = false;
            }
        }
        //private IEnumerable<int> SieveOfEathosSequence(int i, int j, int num)
        //{
        //    yield return j;
        //    while ((j += i) <= num)
        //        yield return j;

        //}
    }
}
