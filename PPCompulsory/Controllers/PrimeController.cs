using Microsoft.AspNetCore.Mvc;
using PPCompulsory.BLL;
using PPCompulsory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPCompulsory.Controllers
{
    [Route("")]
    [Controller]
    public class PrimeController : Controller
    {
        private readonly IPrime _primeService;
        Prime primeModel = new Prime();

        public List<int> SequentialResults { get; set; }
        public PrimeController(IPrime primeService)
        {
            _primeService = primeService;
        }
        [HttpGet]
        public ViewResult Index() => View();
        [HttpPost("Primes/Sequentiel")]
        public async Task<JsonResult> GetPrimesSequential(int from, int to)
         => Json(await _primeService.GetPrimeSequentialAsync(from, to));
        [HttpPost("Primes/Parallel")]
        public async Task<JsonResult> GetPrimesParallel(int from, int to)
         => Json(await _primeService.GetPrimesParallel(from, to));

    }
}
