
using System.Net;
using System.Text;
using NetworkPractice;

public class Program
{
    private static void Socket()
    {
        var generator = new SocketGenerator();
        generator.Runner("0.0.0.0",8000); //  python3 -m http.server 8000
    }
    
    static void Main(string[] args)
    {
        Socket();
    }
}