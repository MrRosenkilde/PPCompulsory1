using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompulsoryAssignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PrimesController : Controller
    {
        private readonly IMemoryCache _cache;
        public PrimesController(IMemoryCache cache) {
            _cache = cache;
            
        }
        // GET api/values
        [HttpGet("isPrime")]
        public async Task<JsonResult> IsPrime(int n) {
            
            var isPrime = await IsPrimePAsync(n);

            return Json(new { value = n, result = isPrime });    
        }
        private async Task<bool> IsPrimePAsync(int n)
            => await Task.Run(() => {
                if (_cache.TryGetValue(n, out object cachedResult))
                    return (bool)cachedResult;
                if (n < 2) return false;

                bool isPrime = true;
                Parallel.ForEach(
                    Partitioner.Create(
                        //only necesary to check 2, odd numbers and numbers that are below half of n
                        Enumerable.Range(2, n).Where(x => x % 2 != 0 || x == 2 || x < n / 2)
                    ),
                    (i,state) =>
                    {
                            if (n % i == 0)
                            {
                                isPrime = false;
                                state.Stop();
                            }
                    }
                );
                _cache.Set(n, isPrime);
                return isPrime;
        });
        

    }
}
