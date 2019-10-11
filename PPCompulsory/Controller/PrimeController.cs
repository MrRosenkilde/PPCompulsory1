using Microsoft.AspNetCore.Mvc;
using PPCompulsory.BLL;
using PPCompulsory.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PPCompulsory.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimeController : ControllerBase
    {
        private readonly IPrime _primeService;
        Prime primeModel = new Prime();

        public List<int> SequentialResults { get; set; }
        public PrimeController(IPrime primeService)
        {
            _primeService = primeService;
        }

        [HttpGet]
        public ActionResult GetPrimesSequential(Prime model)
        {
            if(model != null)
            {
                primeModel.MinimumValue = model.MinimumValue;
                primeModel.MaximumValue = model.MaximumValue;

                //_primeService.GetPrimeSequential(primeModel.MinimumValue, primeModel.MaximumValue);
                
            }
            else
            {
                throw new Exception("something went wrong");
            }
            return null;
        }

       
    }
}
