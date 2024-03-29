﻿using PPCompulsory1.Core;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var pg = new PrimeGenerator();
            var int_pairs = new int_pair[]
            {
                //new int_pair { Int1 = 0, Int2 = 1_000 },
                new int_pair { Int1 = 0, Int2 = 1_000_000 },
                new int_pair { Int1 = 1_000_000, Int2 = 2_000_000 },
                new int_pair { Int1 = 0, Int2 = 10_000_000 },
                new int_pair { Int1 = 10_000_000, Int2 = 20_000_000 },
                new int_pair { Int1 = 0, Int2 = 100_000_000 },
                new int_pair { Int1 = 100_000_000, Int2 = 200_000_000 },
                new int_pair { Int1 = 0, Int2 = 1_000_000_000 },
            };
            foreach (int_pair ip in int_pairs)
            {
                CompareFunctions(
                    //a1: () => Task.WaitAll( pg.GetPrimesParallel(ip.Int1,ip.Int2) ),
                    a1: () => { var a = pg.GetPrimesParallel(ip.Int1, ip.Int2).Result; },
                    a2: () => { var b = pg.GetPrimesSequential(ip.Int1, ip.Int2).Result; },
                    string.Format("GetPrimesParallel({0:n0},{1:n0})", ip.Int1, ip.Int2),
                    string.Format("GetPrimesSequential({0:n0},{1:n0})", ip.Int1, ip.Int2)
                );
                //var parallel = pg.GetPrimesParallel(ip.Int1, ip.Int2);
                //var sequentiel = pg.GetPrimesSequential(ip.Int1, ip.Int2);
            }
        }
        static void CompareFunctions(Action a1, Action a2,string a1name,string a2name)
        {
            var sw = new Stopwatch();
            sw.Start();
            a1.Invoke();
            sw.Stop();
            Console.WriteLine("{0} executed in {1:n0}ms",a1name,sw.ElapsedMilliseconds);
            var sw2 = new Stopwatch();
            sw2.Start();
            a2.Invoke();
            sw2.Stop();
            Console.WriteLine("{0} executed in {1:n0}ms", a2name, sw2.ElapsedMilliseconds);
            Console.WriteLine();

        }
    }
    class int_pair
    {
        public int Int1 { get; set; }
        public int Int2 { get; set; }
    }
}
