using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace NetworkPractice.E4.Server;

public class TcpServer
{
	private static TcpListener tcpListener;
    private static List<TcpClient> connectedClients = new List<TcpClient>();
    private static readonly object lockObject = new object();

    public static void Runner()
    {
        Start();
         

        while (true)
        {
            var message = Console.ReadLine();
            if (!string.IsNullOrEmpty(message))
            {
                BroadcastMessage("Server", message);
            }
            
        }
    }

    static void Start()
    {
        tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8052);
        tcpListener.Start();
        Console.WriteLine("Server is listening");

        while (true)
        {
            TcpClient client = tcpListener.AcceptTcpClient();

            lock (lockObject)
            {
                connectedClients.Add(client);
            }

            Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
            clientThread.Start(client);
        }
    }

    private static void HandleClient(object client)
    {
        TcpClient tcpClient = (TcpClient)client;
        NetworkStream stream = tcpClient.GetStream();
        string clientName = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();

        Console.WriteLine($"Client {clientName} connected");

        byte[] bytes = new byte[1024];
        int bytesRead;

        while (true)
        {
            try
            {
                bytesRead = stream.Read(bytes, 0, bytes.Length);

                if (bytesRead == 0)
                {
                    break; // Client disconnected
                }

                string message = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                
                if (message.StartsWith("VECTOR3:"))
                {
                    SerializableVector3? serializableVector = SerializableVector3.FromJson(message);
                    // Convert it back to a Vector3
                    Vector3 receivedVector = serializableVector.ToVector3();
                    Console.WriteLine($"Received from {clientName}: {receivedVector}");
                    
                }
                else
                {
                    Console.WriteLine($"Received from {clientName}: {message}");
                }
                

                // Broadcast the message to all connected clients
                BroadcastMessage(clientName, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                break;
            }
        }

        lock (lockObject)
        {
            connectedClients.Remove(tcpClient);
        }

        Console.WriteLine($"Client {clientName} disconnected");
        tcpClient.Close();
    }

    private static void BroadcastMessage(string sender, string message)
    {
        Console.WriteLine($"Broadcasting: {sender}: {message}");

        lock (lockObject)
        {
            foreach (var client in connectedClients)
            {
                NetworkStream stream = client.GetStream();
                byte[] bytes = Encoding.ASCII.GetBytes($"{sender}: {message}");
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
    
}

[Serializable]
public class SerializableVector3
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public SerializableVector3() { }

    public SerializableVector3(Vector3 vector3)
    {
        X = vector3.X;
        Y = vector3.Y;
        Z = vector3.Z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(X, Y, Z);
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static SerializableVector3 FromJson(string json)
    {
        return JsonSerializer.Deserialize<SerializableVector3>(json);
    }

}
 