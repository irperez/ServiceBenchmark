using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Net.Http;
using ServiceBenchmark.MVC6.Core.Models;

namespace ServiceBenchmark.MVC6.Core.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Item> Get(Guid id)
        {
            // simulate call to slow service

            var s = await (new HttpClient()).GetStringAsync("http://localhost:21057/api/slow");

            return new Item(id, s);
        }


    }
}
