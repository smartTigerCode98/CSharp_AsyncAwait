using System;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static  void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            ShowCurrentTimeAsync(token);
            Console.ReadKey();
            cancellationTokenSource.Cancel();
            ConfirmExit(token);
            Console.ReadKey();
            cancellationTokenSource.Cancel();
            Console.ReadKey();
        }

        private static void ShowCurrentTime()
        {
            while (true)
            { 
                Console.WriteLine($"Current date and time is {DateTime.Now}");
                Thread.Sleep(8000);
            }
        }

        private static async Task ShowCurrentTimeAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => ShowCurrentTime(), cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Cancellation request");
            }
        }

        private static async Task ConfirmExit(CancellationToken cancellationToken)
        {
            await Task.Run(() => Console.WriteLine("thanks a lot for using our service," +
                                                   " press any key to confirm exit"), cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Cancellation request");
            }
        }
    }
}
