using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("/")]
    [ApiController]
    public class DiscoveryController : ControllerBase
    {
        // GET: api/Discovery
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "/WeatherForecast", "/GSC" };
        }

    }
}
