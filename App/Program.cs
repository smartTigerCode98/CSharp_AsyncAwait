using System;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            PrintCurrentTimeAsync(token);
            Console.ReadKey();
            ConfirmExitAsync(token);
            Console.ReadKey();
            cts.Cancel();
        }

        private static void PrintCurrentTime()
        {
            while (true)
            { 
                Console.WriteLine($"Current date and time is {DateTime.Now}");
                Thread.Sleep(8000);
            }
        }

        private static async Task PrintCurrentTimeAsync(CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            { 
                await Task.Run(() => PrintCurrentTime(), cancellationToken);
            }
        }

        private static async Task ConfirmExitAsync(CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                await Task.Run(() => Console.WriteLine("Thanks a lot for using our service," +
                                                       " press any key to confirm exit"), cancellationToken);
            }
        }
    }
}
