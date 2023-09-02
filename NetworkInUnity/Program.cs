using System;

namespace NetworkInUnity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";
            
            Server.Start(50, 26950);

            Console.ReadKey();
            
        }
        
        
        
    }
}

