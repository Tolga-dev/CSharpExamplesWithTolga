using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ConstraintKeywords;
using ConstraintKeywords.newConstraint;
using ContextualKeyword;
using ContextualKeyword.Get.E1;
using ContextualKeyword.Init;
using ContextualKeyword.Require.E1;
using Query.From.E1;
using Query.Group.E1;
using Query.Group.E2;
using PassingParameters = MethodParam.PassingParameters;
using Params = MethodParam.Params;
using In_Out = MethodParam.In_Out;


namespace MethodParam
{
    namespace PassingParameters
    {
        internal class Address
        {
            public string? foo;
        }

        internal struct CopyItself
        {
            public string foo;
        }

        internal class PassByValue
        {
            public static int value;

            public static void Print(int value)
            {
                Console.WriteLine(value);
            }

            public static void PassByValue_f(int value)
            {
                value *= 4;
                Print(value);
            }

        }
        internal class PassByReference
        {
            public int value;

            public void Print(int value)
            {
                Console.WriteLine(value);
            }

            public void PassByReference_f(ref int value)
            {
                value *= 4;
                Print(value);
            }

        }

        internal class PassRefByVal
        {
            public int[] arr = { 1, 2, 3 };
            
            public void Print(int[] arr)
            {
                foreach (var VARIABLE in arr)
                {
                    Console.Write(VARIABLE + " ");
                }
                Console.WriteLine(" ");

            }

            public void Pass(int[] arr)
            {
                arr[0] = 31;
                arr = new int[3] { 2, 3, 4 };

                Print(arr);
            }
            
            
        } 
        internal class PassRefByRef
        {
            public int[] arr = { 1, 2, 3 };
            
            public void Print(int[] arr)
            {
                foreach (var VARIABLE in arr)
                {
                    Console.Write(VARIABLE + " ");
                }
                Console.WriteLine(" ");

            }

            public void Pass(ref int[] arr)
            {
                arr[0] = 31;
                arr = new int[3] { 2, 3, 4 };

                Print(arr);
            }
            
        }
    
    }

    namespace Params
    {
        public class Param
        {
            public void UseList_int(params int[] list)
            {
                foreach (var t in list)
                    Console.Write(t + " ");
                Console.WriteLine();
            }
            public void UseList_Object(params object[] list)
            {
                foreach (var t in list)
                    Console.Write(t + " ");
                Console.WriteLine();
            }
            
            
        }

    }

    namespace In_Out
    {
        public class MethodParameters
        {
            
            public void In(in int num)
            {
        //        num = 31;
                Debug.WriteLine(num);
            }

            public void Out(out int num)
            {
                num = 31;
            }
            //alternative
            public void Out_alternative(out int num) => num = 32;
            
        }
    }
    
}

namespace ConstraintKeywords
{

    namespace newConstraint
    {
        class ItemUninit { }
        class ItemFactory<T> where T : new()
        {
            public T GetNewItem()
            {
                return new T();
            }
        }
    }

    namespace Where
    {
        public interface ISample<T>
        {
            T Sample();
        }

        public class Where<T> where T : ISample<T> { }
        
        public class UsingEnum<T> where T : System.Enum {}

        public class MultiWhere<T, U> where T : class where U : struct { }
    }
    
}

namespace AccessKeywords
{
    namespace Base
    {
        namespace MyNamespace
        {
            public class Shape
            {
                protected virtual void Coords()
                {
                    Console.WriteLine("X:0 Y:0");
                }
            }

            public class Square : Shape
            {
                protected override void Coords()
                {
                    base.Coords();
                    Console.WriteLine("X:1 Y:1");
                
                }
            }
            
        }

        namespace E2
        {
            public class Base
            {
                public Base()
                {
                    Console.WriteLine("Base!");
                }
            }

            public class Derived : Base
            {
                public Derived() : base()
                {
                    Console.WriteLine("Derived!");
                }
            }
        }        
    }
}

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

namespace Query
{
    namespace From
    {
        namespace E1
        {
            class From
            {
                private int[] ids = { 0, 1, 2, 3, 4 };
                private IEnumerable<int> CreateQuery()
                {
                    var low = from i in ids where i < 4 select i;
                    return low;
                }
                public void ExecuteQuery()
                {
                    foreach(int i in CreateQuery())
                       Console.Write(i + " ");
                }

            }
        }

        namespace E2
        {
            public class Data
            {
                public string subject { get; set;}
                public List<int>  points { get; set;}
            }

            public class Query
            {
                private List<Data> _datas = new List<Data>()
                {
                    new Data { subject = "A", points = new List<int> { 1, 2, 3, 4, 5, 6 } },
                    new Data { subject = "B", points = new List<int> { 1, 2, 3, 4, 5, 6 } },
                    new Data { subject = "C", points = new List<int> { 1, 2, 3, 4, 5, 6 } },
                    new Data { subject = "D", points = new List<int> { 1, 2, 3, 4, 5, 6,7 } },
                };

                public void Create()
                {
                    var que = from sub in _datas
                        from score in sub.points
                        where score > 6
                        select new { Last = sub.subject, score };

                    foreach (var student in que)
                    {
                        Console.WriteLine("{0} Score: {1}", student.Last, student.score);
                    }
                }
            }
            
            
        }
        
    }

    namespace Group
    {
        namespace E1
        {
            public class st
            {
                public int id { get; set; }
                public List<int> pts;
            }
            class Group
            {
                private List<st> _cool_list = new List<st>
                {
                    new st {id = 0, pts = new List<int>{0,1,2,3}},
                    new st {id = 1, pts = new List<int>{0,1,2,3}},
                    new st {id = 2, pts = new List<int>{1,2,3}},
                    new st {id = 3, pts = new List<int>{1,2,3}},
                };
                public void Create()
                {
                    
                    var cl = from i in _cool_list
                        group i by i.pts[0]!=0;
                    
                    foreach (var i in cl)
                    { 
                        Console.WriteLine(i.Key);
                        foreach (var j in i)
                        {
                            Console.WriteLine(j.pts[0] + " " + j.id);
                        }
                    }
                }
            }
            
        }

        namespace E2
        {
            public class st
            {
                public int id { get; set; }
                public List<int> pts;
            }
            
            class Groui
            {
                private List<st> _cool_list = new List<st>
                {
                    new st {id = 0, pts = new List<int>{0,1,2,3,4}},
                    new st {id = 1, pts = new List<int>{0,1,2,3,5}},
                    new st {id = 2, pts = new List<int>{1,2,3}},
                    new st {id = 3, pts = new List<int>{1,2,3}},
                };
                
                public void Create()
                {

                    var cl = from i in _cool_list
                        group i by i.pts[0]
                        into f_add
                        where f_add.Key == 1 || f_add.Key == 0
                        select new { f = f_add.Key, w = f_add.Count() };
                    
                    foreach (var i in cl)
                    { 
                            Console.WriteLine(i.f + " " + i.w);
                    }
                    
                }
            }
            
        }
        
    }
}

namespace OperatorAndExpressions
{
    namespace ArithmeticOperators
    {
        namespace E1
        {
            
            
            
        }
        
    }
}

public class Program
{

    static void Main(string[] args)
    {
        
    }

    private static void GroupINto()
    {
        Query.Group.E1.Group g = new Group();
        g.Create();
        Query.Group.E2.Groui g2 = new Groui();
        g2.Create();

    }
    private static void From()
    {
        Query.From.E1.From q = new From();
        q.ExecuteQuery();
        Query.From.E2.Query q1 = new Query.From.E2.Query();
        q1.Create();
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

    private static void newConstrant()
    {
        ConstraintKeywords.newConstraint.ItemFactory<ConstraintKeywords.newConstraint.ItemUninit> factory =
            new ItemFactory<ItemUninit>();
        ConstraintKeywords.newConstraint.ItemUninit item = factory.GetNewItem();
    }
    
    private static void In_Out()
    {
        In_Out.MethodParameters methodParameters = new In_Out.MethodParameters();

        // out
        int i = 3;
        Console.WriteLine(i);
        methodParameters.Out(out i);
        methodParameters.Out_alternative(out i);
        Console.WriteLine(i);
        // in 
        int inVariable = 44;
        methodParameters.In(inVariable);

    }
    private static void Params()
    {
        Params.Param param = new Params.Param();
        
        param.UseList_int(1,2,3,4,5);
        param.UseList_Object(1,'A',"flag");

    }
    private static void PassByValue()
    {
        PassingParameters.PassByValue.value = 31;
        PassingParameters.PassByValue.PassByValue_f(31);
        PassingParameters.PassByValue.Print(31);
    }      
    private static void PassRefByVal()
    {
        PassingParameters.PassRefByVal val = new PassingParameters.PassRefByVal();
        
        val.Print(val.arr);
        val.Pass(val.arr);
        val.Print(val.arr);
    }      
    private static void PassRefByRef()
    {
        PassingParameters.PassRefByRef passRefByRef = new PassingParameters.PassRefByRef();
        
        passRefByRef.Print(passRefByRef.arr);
        passRefByRef.Pass(ref passRefByRef.arr);
        passRefByRef.Print(passRefByRef.arr);
    }    
    private static void PassByReference()
    {
        PassingParameters.PassByReference passByValue = new PassingParameters.PassByReference();
        passByValue.value = 31;
        passByValue.PassByReference_f(ref passByValue.value);
        passByValue.Print(passByValue.value);
    }
    
    private static void LookChanges(PassingParameters.Address a, PassingParameters.CopyItself c)
    {
        a.foo = "changed";
        c.foo = "changed";
    }
    private static void BasicPassingParamenterImplementation()
    {
        PassingParameters.Address address = new PassingParameters.Address();
        PassingParameters.CopyItself copyItself = new PassingParameters.CopyItself();

        address.foo = "Default";
        copyItself.foo = "Default";
        
        Console.WriteLine(address.foo + " "  + copyItself.foo);
        
        LookChanges(address,copyItself);

        Console.WriteLine(address.foo + " "  + copyItself.foo);
        
    }
}
