Delegates

to pass a function as a parameter and to handle callback or event handler functions


at Where will we use it?
* Decouple Code: we can change which method gets called without changing the rest of code
* Handle Events: They are often used to handle events, like buttons or game events
* Implement CallBacks: callbacks, one part of code tells another part to do something


Delegates types

* Single Delegates: to a single method at a time
* Multiple Delegates: to multiple method at a time

* Usage Example
  * We can use delegates to 

Example
```csharp
    public delegate void delegater(string msg);
    
    ex1
    delegater del1 = new delegater(Afunction);
    ex2
    delegater del1 = Afunction;
    ex3
    delegater del1 = (string msg) => Console.WriteLine(msg);
    
    static void AFunction(string msg)
    {
        Console.WriteLine(msg);
    }
    
    del1.Invoke("hello world");
    

```

Sources:

https://medium.com/@sonusprocks/understanding-events-and-delegates-in-c-unity-ba4d3bbe9234

https://learn.microsoft.com/en-us/dotnet/csharp/delegates-overview

https://www.tutorialsteacher.com/csharp/csharp-delegates