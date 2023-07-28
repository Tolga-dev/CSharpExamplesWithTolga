using System.Collections.Specialized;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using Proporties;

namespace Polymorphism
{
    namespace E1
    {
        public class Polymorphism
        {

            private  class Foo
            {
                public virtual void Bar_Function(){Console.WriteLine("foo::bar"); }
            }

            private class Bar : Foo
            {
                public override void Bar_Function(){ Console.WriteLine("bar::bar");}
                
                // hides the base class method.  
//                public new void Bar_Function(){ Console.WriteLine("bar::bar");}
                
                public void Print()
                {
                    base.Bar_Function();
                    Bar_Function();
                    //foo::bar
                    //bar::bar

                }
                
            }

            public void Runner()
            {
                Bar bar = new Bar();
                
                bar.Print();
                

            }

        }
    }
 
}

namespace AbstractAndSealedClass
{
    namespace E1
    {
            
        public class Abstract
        {
            public abstract class Shape
            {
                int x, y;
                public Shape(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }

                public int Corx
                {
                    get
                    {
                        return x;
                    }
                    set
                    {
                        x = value;
                    }
                }
                
                public int Cory
                {
                    get
                    {
                        return y;
                    }
                    set
                    {
                        y = value;
                    }
                }
                
                public abstract double area
                {
                    get;
                }

                public override string ToString()
                {
                    return $"{x * y}";
                }
            }

            public class Square : Shape
            {
                public int _x,_y;

                public Square(int _y, int _x) : base(_x,_y)
                {
                    this._y = _y;
                }
                
                public override double area
                {
                    get { return _x * _y; }
                }
            }
            

            public void Runner()
            {
                        

            }

        }
    }   
       
}

namespace Proporties
{
    public class Employee
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }
    }

    public class Manager : Employee
    {
        private string _name;

        // Notice the use of the new modifier:
        public new string Name
        {
            get => _name;
            set => _name = value + ", Manager";
        }

        public void Runner()
        {
            Manager m1 = new Manager();

            // Derived class property.
            m1.Name = "John";

            // Base class property.
            ((Employee)m1).Name = "Mary";

            System.Console.WriteLine("Name in the derived class is: {0}", m1.Name);
            System.Console.WriteLine("Name in the base class is: {0}", ((Employee)m1).Name);

        }
/* Output:
    Name in the derived class is: John, Manager
    Name in the base class is: Mary
*/
    }
}

public class Program
{

    static void Main(string[] args)
    {

        Proporties.Manager dummy = new Manager();
        dummy.Runner();

//        Polymorphism.E1.Polymorphism n = new Polymorphism.E1.Polymorphism();
//        n.Runner();



    }
}