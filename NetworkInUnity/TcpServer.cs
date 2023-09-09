using System.Net;
using System.Net.Sockets;

namespace NetworkInUnity;

public enum PacketTypes
{
    Information = 1,
    ExecutionClient
}

public class TcpServer
{
    private TcpListener _socket;

    public Clients[] Clients = new Clients[50]; 
    public void Init()
    {
        for (int i = 0; i < Clients.Length; i++)
        {
            Clients[i] = new Clients();
        }
    
        _socket = new TcpListener(IPAddress.Any, 5555);
        _socket.Start();
        Console.WriteLine("Starting Server");

        _socket.BeginAcceptTcpClient(ClientConnectCallBack, null);
    }

    private void ClientConnectCallBack(IAsyncResult result)
    {
        TcpClient tcpClient = _socket.EndAcceptTcpClient(result);
        _socket.BeginAcceptTcpClient(ClientConnectCallBack, null);
        
        for (int i = 0; i < Clients.Length; i++)
        {
            if (Clients[i].socket == null)
            {
                Clients[i].socket = tcpClient;
                Clients[i].id = i;
                Clients[i].ip = tcpClient.Client.RemoteEndPoint.ToString();
                Clients[i].Start();
                Console.WriteLine("Connected! " + Clients[i].ip);
                SendInformation(Clients[i].id);
                return;
            }
            
        }
    }

    public void SendInformation(int id)
    {
        Packet packet = new Packet();
        
        packet.Write<long>((long)PacketTypes.Information);
        packet.Write<string>("Welcome");
        packet.Write<string>("Play now!");
        packet.Write<int>(10);
        
        SendDataTo(id,packet.Buffer.ToArray());

    }

    public void SendExecutionMethodToClient(int id)
    {
        Packet packet = new Packet();
        packet.Write<long>((long)PacketTypes.ExecutionClient);
        SendDataTo(id,packet.Buffer.ToArray());
        
    }
    public void SendDataTo(int id, byte[] data)
    {
        Packet packet = new Packet();
        packet.Write<long>(
            (data.GetUpperBound(0) - data.GetLowerBound(0))
                + 1);
    }




    
    
}