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
        private static readonly int _numberOfRequestsToSend = 10000;

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start . . .");
            Console.ReadKey();

            Run();

            Console.ReadKey();
        }

        private static async void Run()
        {
            await TestServiceStack();
            await TestWebApi();
            Console.WriteLine("Press any key to exit . . .");
        }

        private static async Task ExecuteAction(string description, Func<Task> actionToExecute)
        {
            var sw = new Stopwatch();
            Console.Write(description + " . . . ");
            sw.Start();
            await actionToExecute();
            sw.Stop();
            Console.WriteLine(string.Format("{0} ms", sw.ElapsedMilliseconds));
        }

        private static async Task TestServiceStack()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("ServiceStack");
            Console.WriteLine("------------------------------------------------------------");


            await ExecuteAction(_numberOfRequestsToSend.ToString() + " requests to item/id", async () =>
            {
                var tasks = new List<Task>();
                for (int i = 0; i < _numberOfRequestsToSend; i++)
                {
                    var t = new WebClient().DownloadStringTaskAsync("http://localhost:16227/item/" + Guid.NewGuid());
                    tasks.Add(t);
                }
                await Task.WhenAll(tasks.ToArray());
            });
        }

        private static async Task TestWebApi()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Web API");
            Console.WriteLine("------------------------------------------------------------");


            await ExecuteAction(_numberOfRequestsToSend.ToString() + " requests to api/item/id", async () =>
            {
                var tasks = new List<Task>();
                for (int i = 0; i < _numberOfRequestsToSend; i++)
                {
                    var t = new WebClient().DownloadStringTaskAsync("http://localhost:14851/api/item/" + Guid.NewGuid());
                    tasks.Add(t);
                }
                await Task.WhenAll(tasks.ToArray());
            });
        }
    }
}
