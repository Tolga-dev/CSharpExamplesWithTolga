using ConstraintKeywords.newConstraint;
using Example2;

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


namespace Example2
{
    public class foo
    {
        
    }    
    public class bar
    {
    }
    
    public class Item<T>
    {
        public float val1 = 5;
    }

    public class ItemFoo<T> : Item<T> where T : foo
    {
        public float val2 = 5;
        // Additional properties or methods specific to foo constraint
    }

    public class ItemBar<T> : Item<T> where T : bar
    {
        public float val3 = 5;
        // Additional properties or methods specific to bar constraint
    }
    
    
    public class RunnerClass 
    {
        public void Runner()
        {
            // The type 'Example2.foo' must be convertible to 'Example2.bar'
            // in order to use it as parameter 'T' in the generic class 'Example2.ItemBar<T>'
            //ItemBar<foo> val1 = new ItemBar<foo>();

            ItemBar<bar> bar = new ItemBar<bar>();
            ItemFoo<foo> foo = new ItemFoo<foo>();

            bar.val3 = 1;
            bar.val1 = 1;

            foo.val2 = 1;
            foo.val1 = 2;



        }
        
    
    }
    
}


public class Program
{

    static void Main(string[] args)
    {

        Example2.RunnerClass runnerClass = new RunnerClass();
        runnerClass.Runner();
        

        // ConstraintKeywords.newConstraint.ItemFactory<int> f = new ItemFactory<int>();


    }
    

    private static void newConstrant()
    {
        ConstraintKeywords.newConstraint.ItemFactory<ConstraintKeywords.newConstraint.ItemUninit> factory =
            new ItemFactory<ItemUninit>();
        ConstraintKeywords.newConstraint.ItemUninit item = factory.GetNewItem();
    }

}