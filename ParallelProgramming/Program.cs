using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Parallel;

namespace Parallel
{
    internal class TaskParallelLin
    {
        internal class DataParallelism
        {
            internal class ParallelForClass
            {
                private void DummyFor()
                {
                    for (int i = 1; i < 100000; i++)
                    {
                        Console.WriteLine(i);
                    }
                }

                private void ParallelFor()
                {
                    System.Threading.Tasks.Parallel.For(1, 100000,
                        Console.WriteLine
                    );
                }

                public void Runner()
                {
                    //ParallelFor(); 
                    //DummyFor();

                }
            }

            internal class ParallelForEachClass
            {
                private void ParallelForEach()
                {
                    var list = Enumerable.Range(1, 10).ToList();

                    System.Threading.Tasks.Parallel.ForEach(list,
                        i => { Console.WriteLine("{0}, {1}", i, ExpensiveThing()); });

                }

                private long ExpensiveThing()
                {
                    long total = 0;
                    for (int i = 1; i < 100000000; i++)
                    {
                        total += i;
                    }

                    return total;
                }

                public void Runner()
                {
                    ParallelForEach();
                }
            }

            internal class ParallelForInvoke
            {
                public void DummyOperation()
                {
                    Thread.Sleep(200);
                }

                public void Foo()
                {
                    System.Threading.Tasks.Parallel.Invoke(
                        DummyOperation, DummyOperation, DummyOperation
                    );
                }

                public void Foo2()
                {
                    System.Threading.Tasks.Parallel.Invoke(
                        DummyOperation,
                        delegate() { Console.WriteLine("Method2"); },
                        () => { Console.WriteLine("Method2"); },
                        () => DummyOperation(),
                        () => DummyOperation()
                    );
                }


                public void Runner()
                {
                    Foo();
                    Foo2();
                }

            }

            internal class UsingMaxDegreeOfParallelism
            {
                public void Foo()
                {
                    var options = new ParallelOptions()
                    {
                        MaxDegreeOfParallelism = Environment.ProcessorCount - 1
                    };
                    //A maximum of three threads are going to execute the code parallelly
                    System.Threading.Tasks.Parallel.For(1, 11, options, i =>
                    {
                        Thread.Sleep(500);
                        Console.WriteLine($"Value of i = {i}, Thread = {Thread.CurrentThread.ManagedThreadId}");
                    });
                }

                private static object lockObject = new object();

                private void DataRace()
                {
                    var valueWithoutInterlocked = 0;
                    var valueWithInterlocked = 0;
                    var valueWithLocked = 0;
                    System.Threading.Tasks.Parallel.For(0, 100000, _ =>
                    {
                        //Incrementing the value
                        valueWithoutInterlocked++;
                        Interlocked.Increment(ref valueWithInterlocked);

                        lock (lockObject)
                        {
                            valueWithLocked++;
                        }
                    });
                    Console.WriteLine("Expected Result: 100000");
                    Console.WriteLine($"Actual Result: {valueWithoutInterlocked}");
                    Console.WriteLine($"Actual Result: {valueWithInterlocked}");
                    Console.WriteLine($"Actual Result: {valueWithLocked}");


                }

                public void Runner()
                {
                    DataRace();
                }

            }
        }
    }

    internal class ParallelLinq
    {
        internal class linq
        {
            public void Runner()
            {
                
                                
            }
        }
    }
    
}

public class Program
{
    static void Main(string[] args)
    {
        var m = new Parallel.ParallelLinq.linq();
        
        m.Runner();
        Console.ReadLine();
    }
}