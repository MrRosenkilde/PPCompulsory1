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
   
    }
}
