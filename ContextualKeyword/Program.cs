
using System.Diagnostics.CodeAnalysis;
using ContextualKeyword.Get.E1;
using ContextualKeyword.Init;
using ContextualKeyword.Require.E1;

namespace ContextualKeyword
{
    namespace Get
    {
        namespace E1
        {
            public class Get<T>
            {
                [NotNull] private T _sec;

                public T Seconds
                {

                    get { Console.WriteLine("get operation is applied"); return _sec; }
                    set { Console.WriteLine("set operation is applied"); _sec = value; }
                }

            }
        
        }

        namespace E2
        {
            class Get<T>
            {
                private T _sec;
            
                public T Seconds
                {
                    get => _sec;
                    set => _sec = value;
                }
            }
        }
        
    }

    namespace Init
    {
        class Init<T>
        {
             private T data;

            public T birthday
            {
                get { return data; }
                init { data = value; } // cannot change after assign
            }
            public Init(T birth)
            {
                birthday = birth;
            }

        }
    }

    namespace Partial
    {
        namespace E1
        {
            namespace PC // file x
            {
                partial class A 
                {
                    private int num = 3;
                }            
            }

            namespace PC // file y
            {
                partial class A
                {
                
                }
            }
            
        }

        namespace E2
        {
            // method vversion
            partial class A
            {
                partial void f_1(string s);
            }
            partial class A
            {
                partial void f_1(string s)
                {
                    
                }
                
            }
        }
    }

    namespace Require
    {
        namespace E1
        {
            public class P
            {
                public P() {}
                
                [SetsRequiredMembers]
                public P(int i, int j) => (i,j) = (i_m,j_m);
                
                public required int i_m { get; init; }
                public required int j_m { get; init; }
            }

            public class Base_P : P
            {
                public Base_P() : base() { }

                [SetsRequiredMembers]
                public Base_P(int i, int j) : base(i,j) {}
            }
            
        }
        
    }

   
}


public class Program
{

    static void Main(string[] args)
    { 
        
        
    }
    
    
    private static void Requirement()
    {
        ContextualKeyword.Require.E1.Base_P baseP = new Base_P(1,2);
    }
    private static void Init_f()
    {
        ContextualKeyword.Init.Init<int> d = new Init<int>(23);
//        d.birthday = 31; // not allowed!
        Console.WriteLine(d.birthday);
    }

    private static void Get_Set_f()
    {
        ContextualKeyword.Get.E1.Get<double> get = new Get<double>();
        ContextualKeyword.Get.E2.Get<double> get2 = new ContextualKeyword.Get.E2.Get<double>();
        get.Seconds = 31;
        get2.Seconds = 31;
        Console.WriteLine(get.Seconds);
        Console.WriteLine(get2.Seconds);
    }
}