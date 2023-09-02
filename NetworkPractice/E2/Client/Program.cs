using System.Net.Sockets;
using System.Text;

namespace NetworkPractice.E2.Client;

public class Client
{
    
    private const int PortNo = 50000;
    private const string ServerIp = "127.0.0.1";
    
    public static void Runner()
    {
        Loop();
    }

    private static void NotLoop()
    {
        string textToSend = "Tolga";

        TcpClient client = new TcpClient(ServerIp, PortNo);
        
        
        NetworkStream networkStream = client.GetStream();
        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
        
        Console.WriteLine("Sending: " + textToSend);
        networkStream.Write(bytesToSend, 0, bytesToSend.Length);

        byte[] bytesToRead = new byte[client.ReceiveBufferSize];
        int bytesRead = networkStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
        Console.WriteLine("Received: " + Encoding.ASCII.GetString(bytesToRead,0,bytesRead));
        Console.ReadLine();
        client.Close();
    }
    
    private static void Loop()
    {
        TcpClient client = new TcpClient(ServerIp, PortNo);

        try
        {
            while (true)
            {
                Console.Write("Enter a message (or 'exit' to quit): ");
                string textToSend = Console.ReadLine();

                if (textToSend.ToLower() == "exit")
                    break;

                NetworkStream networkStream = client.GetStream();
                byte[] bytesToSend = Encoding.ASCII.GetBytes(textToSend);

                Console.WriteLine("Sending: " + textToSend);
                networkStream.Write(bytesToSend, 0, bytesToSend.Length);

                byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                int bytesRead = networkStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                Console.WriteLine("Received: " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            }
        }
        finally
        {
            client.Close();
        }
    }
}