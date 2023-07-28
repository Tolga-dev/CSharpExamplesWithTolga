// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using Interfaces.E1;


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

public class Program
{

    static void Main(string[] args)
    {
        Interfaces.E1.Interfaces<int> i = new Interfaces.E1.Interfaces<int>();
        i.Runner();
        IControl<int> c1 = i;
        c1.Runner();
        c1.Check();
        

    }
}