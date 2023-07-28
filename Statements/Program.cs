// -> Dec, review 
namespace Statements
{
    namespace Declaration
    {
        class Declaration
        {
            private void ImplicitlyTypedLocalVars<T>()
            {
                // implicitly typed!
                var i = 10; // -> use it in lists
                T i2; // -> T uncertain variable
                int i3; // certain vars -> Explicitly typed

                var l = new List<T>();
                
            }

            private void RefAndReadOnly()
            {
                
            }

            public void Runner()
            {
                ImplicitlyTypedLocalVars<int>();
            }
        }
        
    }

    namespace Iteration
    {
        internal class Ite
        {
            void Iteration<T>( List<T> l)
            {
                for (int i = 0; i < 3; i++) // for
                    Console.Write(i);
                
                l = new List<T>();
                foreach (var v in l) // foreach
                    Console.WriteLine($"{v}");

                do // do while
                {
                    Console.WriteLine("{}");

                } while (l.Count() < 5);

                while (l.Count() < 5) //  while
                {
                    Console.WriteLine("{}");
                }

            }

            void Runner()
            {
                var f = new List<int>() { 0, 1, 2, 3, 4 };
                var f2 = new List<char>() {'f','m','s'};
                Iteration<int>(f);
                
            }
            
        }        
    }

    namespace CheckedAndUnchecked
    {
        
        internal class CheckedAndUnchecked
        {
            private static void Foo<T>(T[]? numbers)
            {
                var ui = uint.MaxValue;

                unchecked
                {
                    Console.WriteLine(ui + 1); // 0
                }

                try
                {
                    checked
                    {
                        Console.WriteLine(ui + 1 ); // error Possible overflow in checked context
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                // operators 

                var d = double.MaxValue;

                int b = unchecked((int)d);
                Console.WriteLine(b); // 2147483648

                try
                {
                    b = checked((int)d);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e); // error in an overflow.
                    throw;
                }
                



            }

            public void Runner()
            {
                int[] h = new[] { 1, 2, 3 };
                
                Foo(h);
                
                
            }
        }

        internal class Lock
        {
            private int _data;
            private int _unsafeData;
            private static readonly object _data_block = new object();
            private void Foo<T>()
            {

                for (int i = 0; i < 100; i++)
                {
                    _unsafeData += 1;                    
                }
                lock (_data_block)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        _data += 1;
                    }
                    // important part of code
                }
            }

            public async void Runner()
            {
                var tasks = new Task[100];
                for (int i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = Task.Run(Foo<int>);

                }

                await Task.WhenAll(tasks);
                Console.WriteLine(_data);
                Console.WriteLine(_unsafeData);

            }
        }

        internal class Yield_P
        {
            private static IEnumerable<int> Creator(int n )
            {
                for (int i = 0; i < n; i++)
                {
                    yield return i;
                }
            }

            private static void Foo()
            {
                foreach (var i in Creator(9))
                {
                    Console.WriteLine(i);
                }
        
            }
            
            public void Runner()
            {
                Foo();
            }
            
        }

    }
}




public class Program
{

    static void Main(string[] args)
    {
    

    }
}