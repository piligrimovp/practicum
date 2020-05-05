using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace lab4.Controllers
{
    class Result
    {
        public string result { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://www.mocky.io/v2/5c7db5e13100005a00375fda");
            string result = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Result>(result);
            return result1.result.Replace(' ', '_');
        }

    }
}
