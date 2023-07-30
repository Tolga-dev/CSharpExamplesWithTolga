using System.Numerics;
using System.Runtime.Intrinsics.Arm;

namespace Example0
{

    public class Factory<T> where T : new()
    {
        public T CreateInstance()
        {
            return new T();
        }
    }

    public class MyClass
    {
        public void SayHello()
        {
            Console.WriteLine("Hello from MyClass!");
        }
    }

    public class MyOtherClass
    {
        public void SayGoodbye()
        {
            Console.WriteLine("Goodbye from MyOtherClass!");
        }
    }

    public class Program
    {
        public void Runner()
        {
            // Create a Factory for MyClass
            var myClassFactory = new Factory<MyClass>();
            var myClassInstance = myClassFactory.CreateInstance();
            myClassInstance.SayHello();

            // Create a Factory for MyOtherClass
            var myOtherClassFactory = new Factory<MyOtherClass>();
            var myOtherClassInstance = myOtherClassFactory.CreateInstance();
            myOtherClassInstance.SayGoodbye();
        }
    }
    
}

namespace Calculator
{
    internal interface ICalculate<T>
    {
        T Add(T a, T b);
        T Subtract(T a, T b);
        T Generator(T a, T b, Func<T, T, T> dummyFunction);
    }

    public class BasicCalc<T> : ICalculate<T> where T :  IComparable
    {
        public T Add(T a, T b) => (dynamic)a+b;
        public T Subtract(T a, T b) => (dynamic)a - b;
        public T Generator(T a, T b,Func<T,T,T> dummyFunction) => dummyFunction(a,b);
    }

    public class RunnerClass
    {
        public void Runner()
        {
            var run = new BasicCalc<double>();
            const double a = 1;
            const double b = 2;
            run.Add(a, b);
            run.Subtract(a, b);
            run.Generator(a, b, (a,b)=> a+ b);

        }
    }

}


namespace Interfaces
{
    namespace E1
    {
        public interface IControl<T>
        {
            public void Check();
            public int Runner() => 5;

        }
        public class Interfaces<T> : IControl<T>
        {
            public void Check()
            {
                Console.WriteLine("1");
            }
            public void Runner()
            {
                Check();
            }
        }

        public class Dummy : Interfaces2<int>
        {
            private new int Runner() => 6;

        }
        public class Interfaces2<T>
        {
            public static void Check()
            {
                Console.WriteLine("2");
            }
            public static void Runner()
            {
                Check();
            }
        }
        public class Interfaces3<T>
        {
            public void Check()
            {
                Console.WriteLine("3");
            }
            public void Runner()
            {
                Check();
            }
        }
        
    } 
}

namespace Example2
{
    public class ItemSpawner
    {

    }

    public class ItemWorldController
    {

    }
    
    public partial class Item<T> where T : ItemSpawner
    {
        public float k = 5;
    }

    public partial class Item<T, U> where U : ItemSpawner where T : ItemWorldController
    {
        public float b = 5;
    }

    public class RunnerClass
    {
        public void Runner()
        {
            Item<ItemSpawner> m = new Item<ItemSpawner>();
            m.k = 5;
            Item<ItemWorldController, ItemSpawner> n = new Item<ItemWorldController, ItemSpawner>();
            
        }
    }
    
}

