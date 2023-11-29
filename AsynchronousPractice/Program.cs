

// used to create and configure sockets using the system.ney.sockets

using System.Net;
using System.Net.Sockets;
using System.Text;
using AsynchronousPractice;

namespace AsynchronousPractice
{
    internal class Asynchronous1
    { 
        
        private static Action<int, string> _action = (count, name) =>
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(name);
                    Task.Delay(100).Wait();
                }
            };

        private static async Task Foo()
        {
            await Task.Run(() => _action(2, "Foo"));
        }
        private static async Task Bar()
        {
            await Task.Run(() => _action(2, "Bar"));
        }
        public void Runner()
        {
            Foo();
            Bar();
            Console.ReadKey();

        }

    }
    
    internal class Asynchronous2
    { 
        
        private static Func<int, int, int> _action = (count, counter) =>
        {
            for (int i = 0; i < count; i++)
            {
                counter++;
            }
            return counter;
        };

        private static async Task<int> Foo()
        {
            return await Task.Run(() => _action(2000, 10));
        }
        private static async Task<int> Bar()
        {
            return await Task.Run(() => _action(2000, 100));
        }

        private static int Dummy()
        {
            var c = 0;
            for (var i = 0; i < 100; i++)
                c++;
            return c;
        }

        private static void Printer(int counted)
        {
            Console.WriteLine(counted);
        }
        public async void Runner()
        {
            Task<int> task = Foo();
            Task<int> task2 = Bar();
            
            var counter = await task;
            Printer(counter);
            Console.ReadKey();

            var counter2 = await task2;
            Printer(counter2);
            
            Printer(Dummy());
            Console.ReadKey();

        }

    }
    
    internal class Asynchronous3
    {
        private void Function()
        {
            Console.WriteLine("thread 1");
        }
        private void Function2()
        {
            Console.WriteLine("thread 2");
        }

        public void Runner()
        {
            Thread thread = new Thread(new ThreadStart(Function));
            Thread thread2 = new Thread(new ThreadStart(Function2));

            thread.Start();
            thread2.Start();
        }

        public async Task Runner2()
        {
            await Task.Run(Function2);
            await Task.Run(Function);

        }

    }
    
}

namespace RealTimeExample
{
    // reading stream example
    internal class Asynchronous1
    {
        static async void ReadFile(string path)
        {
            Console.WriteLine("File Reading");
            using var reader = new StreamReader(path);
            Console.WriteLine("File reading finished, Lenght: {0}", (await reader.ReadToEndAsync()).Length);
        }

        static async void TaskManager()
        {
            var path = "/home/xamblot/RiderProjects/CsharpPractice/AsynchronousPractice/bigFile.txt";
            ReadFile(path); // will work lately
            Console.WriteLine("Task 1");
            Console.WriteLine("Task 2");
            Console.WriteLine("Task 3");
        }
        public void Runner()
        {
            TaskManager();
            Console.ReadKey();
        }

    }
}


public class Program
{
    static void Main(string[] args)
    {

        var generator = new AsynchronousPractice.Asynchronous3();
        generator.Runner2();
        

    }
}