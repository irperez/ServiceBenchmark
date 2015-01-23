using ServiceBenchmark.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceBenchmark.WebApi.Controllers
{
    public class ItemController : ApiController
    {
        public async Task<Item> Get(Guid id)
        {
            // simulate call to slow service
            var s = await new WebClient().DownloadStringTaskAsync("http://localhost:21057/api/slow");

            return new Item(id, s);
        }
    }
}
