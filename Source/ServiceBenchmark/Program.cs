using ServiceBenchmark.Common;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBenchmark
{
    class Program
    {
        private const int NumberOfRequestsPerBatch = 100;
        private const int NumberBatches = 10;

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start . . .");
            Console.ReadKey();

            Run();


            Console.ReadKey();
        }

        private static async void Run()
        {
            Console.WriteLine("Please wait . . . the results will show up in about 20-ish seconds");

            long serviceStack = 0;
            long webApi = 0;
            long mvc6 = 0;
            // let's mix up ServiceStack and WebApi to minimize effects of thread availability fluctuation
            foreach (var i in Enumerable.Range(0, NumberBatches))
            {
                serviceStack += await TestServiceStack();
                webApi += await TestWebApi();
                mvc6 += await TestMVC6();
            }

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("ServiceStack");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine(serviceStack + "ms ");

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Web API");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine(webApi + "ms ");

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("MVC6 (.Net Core)");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine(mvc6 + "ms ");

            Console.WriteLine("Press any key to exit . . .");
        }

        private static async Task<long> ExecuteAction(Func<Task> actionToExecute)
        {
            var sw = new Stopwatch();
            sw.Start();
            await actionToExecute();
            sw.Stop();

            return sw.ElapsedMilliseconds;
        }

        private static async Task<long> TestServiceStack()
        {
            var ms = await ExecuteAction(async () =>
            {
                var tasks = new List<Task>();
                for (int i = 0; i < NumberOfRequestsPerBatch; i++)
                {
                    var c = new WebClient();
                    c.Headers["Content-Type"] = "application/json";
                    var t = c.DownloadStringTaskAsync("http://localhost:16227/item/" + Guid.NewGuid() + "?format=json");
                        //.ContinueWith(rt => Task.Factory.StartNew(() => Console.WriteLine("SS:"+rt.Result)));
                    tasks.Add(t);
                }
                await Task.WhenAll(tasks.ToArray());
            });
            return ms;
        }

        private static async Task<long> TestWebApi()
        {
            var ms = await ExecuteAction(async () =>
            {
                var tasks = new List<Task>();
                for (int i = 0; i < NumberOfRequestsPerBatch; i++)
                {
                    var c = new WebClient();
                    c.Headers["Content-Type"] = "application/json";
                    var t = c.DownloadStringTaskAsync("http://localhost:14851/api/item/" + Guid.NewGuid());
                        //.ContinueWith(rt => Task.Factory.StartNew(() => Console.WriteLine("WA:"+rt.Result)));
                    tasks.Add(t);
                }
                await Task.WhenAll(tasks.ToArray());
            });
            return ms;
        }

        private static async Task<long> TestMVC6()
        {
            var ms = await ExecuteAction(async () =>
            {
                var tasks = new List<Task>();
                for (int i = 0; i < NumberOfRequestsPerBatch; i++)
                {
                    var c = new WebClient();
                    c.Headers["Content-Type"] = "application/json";
                    var t = c.DownloadStringTaskAsync("http://localhost:47670/api/item/" + Guid.NewGuid());
                    //.ContinueWith(rt => Task.Factory.StartNew(() => Console.WriteLine("WA:"+rt.Result)));
                    tasks.Add(t);
                }
                await Task.WhenAll(tasks.ToArray());
            });
            return ms;
        }

    }
}
