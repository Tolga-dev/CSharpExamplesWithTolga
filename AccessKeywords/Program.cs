using AccessKeywords.Base;
using AccessKeywords.Base.E1;

namespace AccessKeywords
{
    namespace Base
    {
        namespace E1
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

public class Program
{

    static void Main(string[] args)
    {
        AccessKeywords.Base.E1.Shape m = new Shape();
        
    }
}