using System.Numerics;
using OperatorAndExpressions;

namespace OperatorAndExpressions
{
 
        public class Aritmetic_Boolean_Bitwise
        {
            public void ArithmaticOperator(int a)
            {
                Console.WriteLine(a); //a 
                Console.WriteLine(++a);// a + 1  -> prefix
               
                Console.WriteLine(a); // a + 1
                
                Console.WriteLine(--a); // a + 1 - 1 -> prefix
                Console.WriteLine(a); // a + 1 - 1

                a = 5;
                // let a = 5
                Console.WriteLine(a); // 5
                Console.WriteLine(--a); // 4
                Console.WriteLine(a); // 4

                a = 5;
                //let a = 5
                Console.WriteLine(a); // 5
                Console.WriteLine(a--); // 5
                Console.WriteLine(a); // 4

                a = 5;
                // '/' division operator -> towards zero 
                Console.WriteLine(13 / a); // 2
                Console.WriteLine(-13 / a); // -2
                
            }

            public void booleanLogical()
            {
                var a = true & true;
                Console.WriteLine(a);
                
                Console.WriteLine(a ^ true); // xor -> false
                Console.WriteLine(a | false); // or -> true
                Console.WriteLine(a & false); // amd -> false
            }

            public void Bitwise()
            {
                uint x = 0b_1010;
                uint y = 0b_1000;
                Console.WriteLine(x << 1);  // - > 10100
                Console.WriteLine(x >> 1);  // - > 00101
                Console.WriteLine(x | y);  // - > 1010
                Console.WriteLine(x & y);  // - > 1000
                Console.WriteLine(x ^ y);  // - > 0010
                
                
                
            }
            
        }

        public class MemberAccess
        {
            public void Access_NullConditionalOperators()
            {
                {// to demonstrate indexer access
                    var dic = new Dictionary<string, double>();
                    dic["one"] = 1;
                    dic["pi"] = Math.PI;
                    Console.WriteLine(dic["one"] + dic["pi"]); 
                }
            }

            double Sums(List<double[]> set, int index)
            {
                return set?[index]?.Sum() ?? double.NaN;
            }

            int ZeroOrValue(int[] mem)
            {
                // if u dont use ?? 
                // let mem = null, this stat evaluates -> false, means execute else
                if ((mem?.Length ?? 0) < 2) return 0;
                else return mem[0] + mem[1];
                
            }
            

            public void Runner()
            {
                {
                    // demonstrate the usage of the ?. and ?[] 
                    Console.WriteLine(Sums(null, 31)); // nan!

                    //or

                    var members = new List<double[]>()
                    {
                        new[] { 1.0, 2.0 },
                        null
                    };
                    Console.WriteLine(Sums(members, 0)); // 3
                    Console.WriteLine(Sums(members, 1)); // nan

                    // or 

                    ZeroOrValue(null);
                    ZeroOrValue(new[] { 3, 4 });

                    int[] l = null;
                    Console.WriteLine((l?.Length ?? 0) < 5); // TRUE
                    Console.WriteLine((l?.Length) < 5); // FALSE
                }
                
                {
                    
                    
                }
            }
            
        }

        public class TypeTesting
        {   
            private void Is()
            {
                var b = new List<int>();
                Console.WriteLine(b is int); // false

                int i = 32;
                Console.WriteLine(i is int);
            }

            private void Cast()
            {
                var x = 312.0;
                var y = 312;

                var k = (double)y; // 312.0
                
            }

            void TypeOf<T>() => Console.WriteLine(typeof(T));
            
            
            public void Runner()
            {
                Is();
                Cast();
                TypeOf<System.Index>();
                TypeOf<System.Int16>();
                
            }
            
        } //  perform type checking or type conversion.

        public class UserDefinedOperators<T> // operator in class implicitly
        {
            public T val { get; set; }

            static public implicit operator UserDefinedOperators<T>(T val)
            {
                return new UserDefinedOperators<T>(){ val = val};
            }

            static public explicit operator T(UserDefinedOperators<T> operators)
            {
                return operators.val;
            }

            public void Runner()
            {
                {
                    UserDefinedOperators<int> o1 = new UserDefinedOperators<int>();
                    int val = 31;

                    o1 = val; // implicitly, int -> op.type
                    Console.WriteLine(o1.val);

                    int val2 = (int)o1;
                }

                {
                    UserDefinedOperators<string> o1 = new UserDefinedOperators<string>();
                    var val = "31";

                    o1 = val; // implicitly, string -> op.type
                    Console.WriteLine(o1.val);

                    var val2 = (string)o1;

                }
            }
            
        }
        
        public class Lambda<T>
        {
            void Runner()
            {
                Func<int, int> outcube = x => x * x * x;
                Console.WriteLine(outcube(5));
            }

        }
        
        // is string?
        public class DeclaretionANdTypePatterns
        {
            void foo<T>(T s)
            {
                var k = (T s) => {  return (s is string message) ? 1 : 0; };
                Console.WriteLine(k(s));
                
                if (k is string message)
                    Console.WriteLine(k(s));
                else
                    Console.WriteLine("sad!");
            }
            public void Runner()
            { 
                foo("hello world!");
            }
        }
        // switch
        public class @Switch
        {
            static float GetSource<T>(INumber<T> source) where T : System.Numerics.INumber<T>?
                => source switch
                {
                    float => 1f,
                    null => throw new ArgumentNullException(nameof(source)),
                    _ => throw new ArgumentNullException($"not supported!"),
                };
 
            private record Vector2<T>(T X, T Y);
            // switch
            static Vector2<T> Transform<T>(Vector2<T> vector2) where T : System.Numerics.INumber<T>? => vector2 switch
            {
                var (x, y) when GetSource(vector2.X) == 0 || GetSource(vector2.Y) == 0 => throw new ArgumentNullException($"not float!"),
                var (x, y) when x < y => new Vector2<T>(-x, y),
                var (x, y) when x > y => new Vector2<T>(x, -y),
                var (x, y) => new Vector2<T>(x, y),

            };
            public void Runner()
            { 
                Console.WriteLine(Transform(new Vector2<float>(1.2f,2.1f)));

               Console.WriteLine(GetSource(1.2f));
               
            }
        }

        //delegate operator creates an anonymous method that can be converted to a delegate type
        public class Delegates
        {
            private void foo<T>(T a, T b) where T : INumber<T>
            {
                Func<T,T,T> sum = delegate(T a, T b) { return a + b;};

                Func<int, int, int> sum2 = delegate (int a, int b) { return a + b; };
                
                Console.WriteLine(sum(a,b) + " " + sum2(4,3));  // output: 7
            }
            public void Runner()
            {
                foo<int>(4, 3); 
            }
        }
        
        //a with expression produces a copy of its operand 
        public class With
        {
            public record coord(int x, int y);
            public record vector3(int x, int y, int z) : coord(x, y);

            void foo(int a, int b)
            {
                {
                    var p1 = new coord(a, 0);
                    Console.WriteLine(p1); // a 0

                    var p2 = p1 with { x = 6, y = 5 }; // 6 5
                    Console.WriteLine(p2); // a 0

                    var p3 = p1 with
                    {
                        x = 7,
                        y = 4
                    };
                    Console.WriteLine(p3); // output:7 4 
                }

                vector3 vector3 = new vector3(0, 5, 6);
                vector3 vector32 = vector3 with { x = vector3.x + 1 };
                Console.WriteLine(vector32);

            }

            
            public void Runner()
            {
                foo(5,6);
            }

        }
        
}

public class Program
{

    static void Main(string[] args)
    {
 
    }
    private static void operators()
    {
        OperatorAndExpressions.MemberAccess memberAccess = new MemberAccess();
        memberAccess.Runner();
    }
}