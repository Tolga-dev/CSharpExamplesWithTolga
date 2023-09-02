using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkPractice.E2.Server;

public class Program
{
    private const int PortNo = 50000;
    private const string ServerIp = "127.0.0.1";
    
    public static void Runner()
    {
        Loop();
    }

    private static void NoLoop()
    {
        
        Console.WriteLine("Server Running");
        var localAddr = IPAddress.Parse(ServerIp);
        var listener = new TcpListener(localAddr, PortNo);
        Console.WriteLine("Listening...");
        listener.Start();

        TcpClient client = listener.AcceptTcpClient();

        NetworkStream networkStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        int bytesToRead = networkStream.Read(buffer, 0, client.ReceiveBufferSize);

        string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesToRead);
        Console.WriteLine("Received: " + dataReceived);
        
        Console.WriteLine("Sending Back: " + dataReceived);
        networkStream.Write(buffer, 0, bytesToRead);
        client.Close();
        listener.Stop();
        Console.ReadLine();
    }
    
    
    private static void Loop()
    {
        Console.WriteLine("Server Running");
        var localAddr = IPAddress.Parse(ServerIp);
        var listener = new TcpListener(localAddr, PortNo);
        Console.WriteLine("Listening...");
        listener.Start();
        
        try
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                // Handle the client in a separate thread or task to allow concurrent connections.
                // For simplicity, we'll handle it in the main thread here.

                NetworkStream networkStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];

                int bytesRead;
                while ((bytesRead = networkStream.Read(buffer, 0, client.ReceiveBufferSize)) > 0)
                {
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received: " + dataReceived);

                    Console.WriteLine("Sending Back: " + dataReceived);
                    networkStream.Write(buffer, 0, bytesRead);
                }

                client.Close();
            }
        }
        finally
        {
            listener.Stop();
        }
    }
    
}