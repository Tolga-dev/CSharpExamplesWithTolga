using System;
using System.Diagnostics;
using Throw;
using TryCatch;

namespace Throw
{
    public class NumberGenerator
    {
        private int[] _number = {0, 2, 4, 5, 6, 9, 866, 546, 465, 4654 };
        
        public int GetNumber(int index)
        {
            if (index < 0) throw new InvalidDataException();

            try
            {
                int val;

                val = _number[index];
                return val;

            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"{e.GetType().Name} : {index} out of range");
                throw;
            }
            
        }

    }
    
}

namespace TryCatch
{
    public class Catch
    { 
        public Catch(object o)
        {
        
            try
            {
                var i = (int)o;

                
                if (o == null)
                    throw new NullReferenceException(o + " Null!");
                
            }
            catch(InvalidCastException e)
            {
                Console.WriteLine(e + " => Null!");
            }
        }
    }
}

public class Exceptions
{
    
    static string x;

    static void Main()
    {
        Console.WriteLine(Method());
        Console.WriteLine(x);
    }

    static string Method()
    {
        try
        {
            x = "try";
            return x;
        }
        finally
        {
            x = "finally";
        }
    }
    void TryCatch()
    {
        var o = new object();
        object b = null;
        
        var c2 = new Catch(o);
        var c = new Catch(b);
        
        string s = null;
        try
        {
            ProcessString(s);
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("{0} null Exception Caught!.",e);
        }
        catch (Exception e)
        {
            Console.WriteLine("{0} Exception Caught!.",e);
        }


    }
    int Throw()
    {
        var n = new NumberGenerator();
        const int index = 30;
        n.GetNumber(0);

        return index;
    }
    static void ProcessString(string s)
    {
        if (s == null) throw new ArgumentNullException(paramName: nameof(s), message: "Cannot be null!");

    }
}

