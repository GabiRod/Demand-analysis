using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using back_end.GoogleSearchConsole;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace back_end.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GSCController : ControllerBase
    {
        // GET: api/GSC
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GSC/5
        [HttpGet("{siteUrl}", Name = "Get")]
        public string Get([FromBody] RequestObject request)
        {
            var queryData = ApiController.GetData(request);
            var json = JsonConvert.SerializeObject(queryData);

            return json;
        }

        // POST: api/GSC
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/GSC/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
