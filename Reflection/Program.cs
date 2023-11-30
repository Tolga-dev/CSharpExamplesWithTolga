using System.Net;
using System.Net.Sockets;
using System.Text;
using Reflection;
 

namespace Reflection
{
    internal class Reflection01
    {
        private void Ex1()
        {
            int i = 42;
            System.Type type = i.GetType();
            Console.WriteLine(type);
        }

        private void Ex2()
        {
        }
        public void Runner()
        {
            Ex1();
        }

    }
}


public class Program
{
    static void Main(string[] args)
    {
        Reflection.Reflection01 reflection01 = new Reflection01();
        reflection01.Runner();
    }
}