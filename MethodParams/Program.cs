using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Microsoft.VisualBasic.CompilerServices;
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

public class Program
{

    static void Main(string[] args)
    {
 
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
