

// used to create and configure sockets using the system.ney.sockets

using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using AsynchronousPractice;
using TaskBasedAsynchronous;

namespace ComparisonOfPatterns
{
    public class ReadSimpleClass
    {
        public int Read(byte[] buff, int offset, int count)
        {
            return 0;
        }
    }

    public class ReadTaskBased
    {
        public Task<int>? ReadAsync(byte[] buff, int offset, int count)
        {
            return null;
        }
    }

    public class ReadeEventBased
    {
        public IAsyncResult? BeginRead(
            byte[] buffer, int offset, int count,
            AsyncCallback callback, object state)
        {
            return null;
            
        }

        public int EndRead(IAsyncResult asyncResult)
        {
            return 0;
        }
    }
}

namespace TaskBasedAsynchronous
{
    internal class UsageOfAsyncAndAwait
    {
        public class Ex0
        {
            private void Dummy1(string name)
            {
                Console.WriteLine("{0} operation is started", name);
                Task.Delay(1000).Wait(); // expensive operation
                Console.WriteLine("{0} operation is finished", name);
            }
            private async Task Foo()
            {
            
                await Task.Run(
                    () =>
                    {
                        Dummy1("foo");
                    }
            
                );
            }

            private async Task<int> Foo2()
            {
                return await Task.Run((
                    () =>
                    {
                        Dummy1("foo2");
                        return 0;
                    }
                ));
            }

            private void Bar()
            {
                Console.WriteLine("Bar operation is started");
                Task.Delay(100).Wait(); // expensive operation
                Console.WriteLine("Bar operation is finished");
            }

            private async void CallFoo()
            {
                Task<int> foo1 = Foo2();
                await Foo();
                int count = await foo1;
                Console.WriteLine("hello World");
            }
            public void Runner()
            {
                // they will run asynchronously
                //Foo();
                //Bar();
            
                // calling another example
                CallFoo();
            
                Console.ReadKey();

            }
        }
        public class Ex1
        {
            public async Task Foo1()
            {
                await Task.Run((() =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine("Foo1");
                }));
            }
            public async Task Foo2()
            {
                await Task.Run((() =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine("Foo2");
                }));
            }
            public async Task Foo3()
            {
                await Task.Run((() =>
                {
                    Thread.Sleep(100);
                    Console.WriteLine("Foo3");
                }));
            }
            public void FooDummy()
            {
                Thread.Sleep(100);
                Console.WriteLine("Foo3");
            }
            public async void Runner()
            {
                var watch = new Stopwatch();
                watch.Start();

                var task1 = Foo1();
                var task2 = Foo2();
                var task3 = Foo3();
                Task.WaitAll(task1, task2, task3);
                watch.Stop();
                Console.WriteLine(watch.ElapsedMilliseconds);
                
                watch.Reset();
                
                watch.Start();
                FooDummy();
                FooDummy();
                FooDummy();
                watch.Stop();
                Console.WriteLine(watch.ElapsedMilliseconds);
            }
        }
        
        public class Ex2
        {
            private int GetId() => 0;

            private async Task<string> GetEmail(int id)
            {
                return await Task.Run((() => "hel"));
            }
            private User GetUser(string email) => new User();
            public class User { }

            public async Task<User> GetLoggedUsed()
            {
                int id = GetId();
                string email = await GetEmail(id);
                User user = GetUser(email);
                return user;
            }
            
            public async void Runner()
            {
                
            }
        }
        

    }
}

namespace EventBasedAsynchronous
{
    internal class EventBased
    {
        public class Ex0
        {
            public class Handler
            {
                public event EventHandler OnTriggerCompleted;
                public void Start(int timeout)
                {
                    var timer = new Timer(new TimerCallback((state) =>
                    {
                        OnTriggerCompleted?.Invoke(null, null);
                    }));

                    timer.Change(timeout, 0);
                }
            }

            public async void Runner()
            {
                var handler = new Handler();

                handler.OnTriggerCompleted += (sender, e) =>
                {
                    Console.WriteLine($"Triggered at: { DateTime.Now.ToLongTimeString()}");
                };

                handler.Start(3000);

                Console.WriteLine($"Start waiting at {DateTime.Now.ToLongTimeString()}");
                Console.WriteLine($"Processing...");
                
            }
        }
        

    }
}

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

        var hel = new EventBasedAsynchronous.EventBased.Ex0();
        hel.Runner();
        Console.ReadKey();

    }
}