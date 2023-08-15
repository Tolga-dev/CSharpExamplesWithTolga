using System.Collections;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using Interfaces.E1;
using Interfaces.ex2;


namespace Interfaces;

/// <summary>
///  Example of interfaces to use
/// </summary>
interface Iname
{
    // declare Events
    // declare indexers
    // declare methods 
    // declare properties
}

public class Runner
{
    public void E1()
    {
        var i = new Interfaces.E1.Interfaces<int>();
        i.Runner();
        IControl<int> c1 = i;
        c1.Runner();
        c1.Check();
        
    }

    public void Weather()
    {
        Interfaces.ex2.Program weather = new Program();
        weather.Runner();
    }

    public void Interface()
    {
        var runnerClass = new RunnerClass();
        runnerClass.Runner();
    }
    
    private static void Main(string[] args)
    {
        

    }
}