using System;
using OopCreate.Abstraction;
using OopCreate.Attributes;
using OopCreate.Constructors;
using OopCreate.Methods;
using OopCreate.Objects;


namespace OopCreate
{
    namespace Objects
    {

        public class Objects01
        {
            public struct Person
            {
                public string Name { get; set; }
                public int Age { get; set; }

                public Person(string name, int age)
                {
                    Name = name;
                    Age = age;
                }

            }
            
            public void Runner()
            {
                Person person = new Person("hell", 1);
                Person person2 = person;

                person2.Name = "sex";
                person2.Age = 2;
                

            }

        }
    }

    namespace Classes
    {
        public class Class01
        {
            private class Foo
            {
                private int _cap;

                public Foo(int cap) => _cap = cap;
            }
            public void Runner()
            {
                Foo f = new Foo(1);
                Foo f2 = new Foo(2);
                f2 = f;


            }
        }
    }
    
    namespace Attributes
    {
        public class dummyAttr : Attribute
        {
            public dummyAttr(string str)
            {
                Console.WriteLine(str);
            }
        }
        [dummyAttr("hell")]
        public class Att01
        {
            public void Runner()
            {
                Console.WriteLine(typeof(Att01));
            }
        }
    }

    namespace Methods
    {
        public class Met01
        {
            abstract class Motor
            {
                // anyone can call it
                public void Start(){}
            
                // only derived class can call this
                protected void AddGas(){}
            
                // derived class can override the base class implementation
                public virtual int Drive()
                {
                    return 0; 
                }
                // derived classes must implement this
                public abstract double GetTopSpeed();

                protected static async Task<int> DelayAsync(int a)
                {
                    await Task.Delay(100);
                    return a;
                }
            }

            private class Car : Motor
            {
                public override double GetTopSpeed()
                {
                    GettingADelay();
                    return _i;
                }

                private static async Task GettingADelay()
                {
                    Task<int> delayer = DelayAsync(10);
                    await delayer;
                }
                
                // arguments, compatible with the parameter type
                private double _i = 100;
            }
            
            public void Runner()
            {
                
            }
        }
    }
    
    namespace Constructors
    {
         internal class Constructors01
         {
             public class Constructor
             {
                 private string _last;
                 private static int _flag;

                 public Constructor(string name)
                 {
                     _last = name;
                     _flag = 1;
                 }

                 public Constructor(string name, int flag)
                     => _last = name;

                 static Constructor() => _flag = 0;

                 public Constructor(Constructor constructor)
                 {
                     _last = constructor._last;
                 }
                 
             }
             
            public void Runner()
            {
                
            }
        }
    }

    namespace Abstraction
    {
        internal class Abstraction01{

            private abstract class Entity
            {
                public int health = 100;
                public abstract int GetHealth();
            }

            private class Player : Entity
            {
                public override int GetHealth()
                {
                    return health * 2;
                }
            }
            private class Enemy : Entity
            {
                public override int GetHealth()
                {
                    return health * 2;
                }
            }
            
            
            public void Runner()
            {
                Player player = new Player();
                Enemy enemy = new Enemy();
                
                player.health = 50;
                Console.WriteLine(player.GetHealth() == enemy.GetHealth());

            }
        }
    }
    
    namespace Encapsulation
    {
        internal class Encapsulation01
        {
            class Bank // capsule
            {
                private string name;
                private int generalBalance;

                public int GetBalance()
                {
                    return generalBalance;
                }

                public string GetName()
                {
                    return name;
                }
            }
            public void Runner()
            {
                
            }
        }
    }

    namespace Inheritance
    {
        internal class Inheritance01
        {
            private class Parent
            {
                public string Name = "bar";

                public string GetLanguage()
                {
                    return "English";
                }
            }

            sealed class Parent2 // does not let to be derived by others
            {
                    
            }
            private class Child : Parent
            {
                public string SchoolName = "foo";
                
            }
            public void Runner()
            {
                var child = new Child
                {
                    SchoolName = "bar"
                };
                child.GetLanguage();
                
                
                
            }

             
        }

        internal class Inheritance02
        {
            private class BaseClass
            {
                public async Task<int> PerformTaskAsync()
                {
                    Console.WriteLine("BaseClass is performing a task asynchronously...");
                    await Task.Delay(2000); // Simulating an asynchronous operation
                    return 10;
                }
            }

            private class DerivedClass : BaseClass
            {
                public static async Task<string> PerformExtendedTaskAsync()
                {
                    Console.WriteLine("DerivedClass is performing an extended task asynchronously...");
                    await Task.Delay(3000); // Simulating another asynchronous operation
                    return "Extended task completed";
                }
            }

            public async Task Runner()
            {
                var derivedObj = new DerivedClass();

                Console.WriteLine("Calling asynchronous method from BaseClass...");
                int result = await derivedObj.PerformTaskAsync();
                Console.WriteLine($"BaseClass Task Result: {result}");

                Console.WriteLine("\nCalling asynchronous method from DerivedClass...");
                string extendedResult = await DerivedClass.PerformExtendedTaskAsync();
                Console.WriteLine($"DerivedClass Extended Task Result: {extendedResult}");
            }
        }
    }

    namespace Polymorphism
    {
        public class Polymorphism01
        {
            private class Animal
            {
                public virtual void AnimalSound()
                {
                    
                }
            }

            private class Cat : Animal
            {
                public  override void AnimalSound()
                {
                    base.AnimalSound();
                }
            }

            private class Dog : Animal
            {
                public  override void AnimalSound()
                {
                    base.AnimalSound();
                }
            }
            
            public void Runner()
            {
                Animal dog = new Dog();
                Animal cat = new Cat();
                
                dog.AnimalSound();
                cat.AnimalSound();

                var animals = new List<Animal>
                {
                    new Cat(),
                    new Dog()
                };
                foreach (var animal in animals)
                {
                    animal.AnimalSound();
                }

            }
        }
    }
    
    

    

}

public class Program
{

    static void Main(string[] args)
    {
        OopCreate.Abstraction.Abstraction01 M = new Abstraction01();
        M.Runner();
    }
}