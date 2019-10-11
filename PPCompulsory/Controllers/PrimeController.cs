using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PPCompulsory.BLL;
using PPCompulsory.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
        //[HttpPost("Primes/Sequentiel")]
        //public async Task<JsonResult> GetPrimesSequential(int from, int to)
        // => Json(await _primeService.GetPrimeSequentialAsync(from, to));
        //[HttpPost("Primes/Parallel")]
        //public async Task<JsonResult> GetPrimesParallel(int from, int to)
        // => Json(await _primeService.GetPrimesParallel(from, to));
        [HttpGet("primes/sequentiel")]
        public async Task<IActionResult> GetPrimesSequential(int from, int to)
        {
            //var stream = new MemoryStream("primes.txt", FileMode.OpenOrCreate);
            var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot", "primes.txt");
            
            using (StreamWriter writer = new StreamWriter(path,false))
            {
                await writer.WriteAsync(
                    JsonConvert.SerializeObject(
                        await _primeService.GetPrimeSequentialAsync(from, to), Formatting.None)
                    );
                writer.Close();
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }
        [HttpGet("Primes/parallel")]
        public async Task<FileStreamResult> GetPrimesParallel(int from, int to)
        {
            //var stream = new MemoryStream("primes.txt", FileMode.OpenOrCreate);
            var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot", "primes.txt");

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteAsync(
                    JsonConvert.SerializeObject(
                        await _primeService.GetPrimesParallel(from, to), Formatting.None)
                    );
                writer.Close();
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }

    }
}
