using System.Collections.Specialized;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using Delegates.E1;

namespace Delegates
{
    namespace E1
    {
        public class Delegate
        {
            private delegate T Foo<T>(T val) where T : INumber<T>;
            private delegate int MultiDel(int n);
            
            private static int Multi(int n)
            {
                Console.WriteLine(n);
                return n;
            }
            
            private static T FooFunction<T>(T val)
            {
                return val;
            }
            private T FooFunctionNonStatic<T>(T val) // same behaviour with static
            {
                return val;
            }
            
            public void Runner()
            {
                /*
                {
                    Foo<int> handler = FooFunction<int>;
                    Foo<float> handler2 = FooFunction<float>;

                    Console.WriteLine(FooFunction<Foo<int>>(handler)(31));

    //                Console.WriteLine(handler(31)); // 31

                }
                */

                /*
                {
                    Foo<int> handler = FooFunction<int>;
                    Foo<int> handler2 = FooFunction<int>;
                    Foo<int> handler3 = handler + handler2;
                    
                    Console.WriteLine(handler(31)); // 31
                    
                }
                */
                {
                    MultiDel foo = Multi;
                    MultiDel bar = Multi;
                    MultiDel sum = foo + bar;
                    Console.WriteLine(sum(31)); // 31 31 31
                }
                
            }

        }
        
    }

    namespace E2
    {

        public class Delegate
        {
            private delegate T Foo<T>(T val) where T : INumber<T>;

            private static T FooFunction<T>(T val)
            {
                return val;
            }

            public void Runner()
            {
                // Instantiate Del by using a lambda expression.
                Foo<int> del4 = name => 5;
                

            }
        }
    }
}


public class Program
{

    static void Main(string[] args)
    {
        
        Delegates.E2.Delegate m = new Delegates.E2.Delegate();
        
        m.Runner(); 
        
    }
}