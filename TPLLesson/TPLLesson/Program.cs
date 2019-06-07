using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TPLLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - выполняет работу");

            //Task task = new Task(() =>
            //{
            //    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - выполняет работу");
            //});
            //task.Wait();
            //task.Start();

            //Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - выполняет работу");
            //});

            var data = Task.Run(() =>
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - выполняет работу");
                return 5;
            }).Result; // для получения рез-та

            Console.WriteLine(data);

            CancellationTokenSource source = new CancellationTokenSource();
            var cancellationToken = source.Token;

            var task = new Task(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Таск завершен");
            }, cancellationToken);

            task.Start();
            source.Cancel();
            source.Dispose();
            Console.ReadLine();
        }

        static Task<string> GetDataAsync()
        {
            // cpu bound operation, как и Factory.Start.New() and task.Start()
            //return Task.Run(() =>
            //{
            //    return "";
            //});

            var data = "asd" + "asdasd";
            return Task.FromResult(data);
        }

        static Task GetDataSecondAsync()
        {
            // cpu bound operation, как и Factory.Start.New() and task.Start()
            //return Task.Run(() =>
            //{
            //    return "";
            //});

            try
            {
                var data = "asd" + "asdasd";
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                return Task.FromException(exception);
            }
        }
    }
}
