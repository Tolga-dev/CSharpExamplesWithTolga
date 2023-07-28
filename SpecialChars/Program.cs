namespace SpecialChars
{
    namespace DollarChars //$ -> the interpolated string character
    {
        internal class InterpolatedChar
        {
            private static void Foo()
            {
                const string name = "FOO";
                var date = DateTime.Now;
                
                Console.WriteLine($"{name}");
                Console.WriteLine($"{0} {1}",name,"Hello World");
                

            }

            private static void Bar(int n)
            {
                var message = $"{n} is {
                    n switch
                    {
                        > 50 => "Passed",
                        _ => "Failed",
                    }
                }";
                Console.WriteLine(message);
                Console.WriteLine($"{n} is { n switch { > 50 => "Passed", _ => "Failed", } }");
            }

            public void Runner()
            {
                Bar(31);                
                Bar(60);                
            }
            
            
        }
    }

    namespace VerbatimIdentifier
    {
        internal class Verbatim
        {
            private static void Foo()
            {
                string[] @while = { "A", "B", "C", "D" }; // while -> @while
                foreach (var t in @while)
                {
                    Console.WriteLine($"{t}!");
                }
                
                var f = @"c:\";
                var f2 = "c:\\";

                
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