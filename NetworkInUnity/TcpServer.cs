using System.Net;
using System.Net.Sockets;

namespace NetworkInUnity;

public enum PacketTypes
{
    Information = 1,
    ExecutionClient,
    PlayerData
}
public enum ClientPacketTypes
{
    Information = 1,
        
}
public class TcpServer
{
    private TcpListener _socket;

    public static Clients[] Clients = new Clients[50]; 
    public void Init()//
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

    private void ClientConnectCallBack(IAsyncResult result)//
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
                //SendInformation(Clients[i].id);
                SendJoinGame(i);
                return;
            }
            
        }
    }

    public void SendInformation(int id)//
    {
        Packet packet = new Packet();

        packet.WriteLong((long)PacketTypes.Information);
       
        packet.WriteString("Welcome");
        packet.WriteString("Play now!");
        packet.WriteInt(10);
        
        SendDataTo(id,packet.ToArray());

    }
    
    public void SendExecutionMethodToClient(int id)//
    {
        Packet packet = new Packet();
        packet.WriteLong((long)PacketTypes.ExecutionClient);
        SendDataTo(id,packet.ToArray());
    }

    public void SendDataToAll(byte[] data)
    {
        for (int i = 0; i < Clients.Length; i++)
        {
            if(Clients[i].socket != null)
                SendDataTo(i,data);
        }
        
    }
    
    public void SendDataTo(int id, byte[] data)//
    {
        Packet packet = new Packet();
        packet.WriteLong((data.GetUpperBound(0) - data.GetLowerBound(0))+ 1);
        packet.WriteByte(data);
        Clients[id].Stream.BeginWrite(packet.ToArray(), 0, packet.ToArray().Length,
            null, null);
    }

    public byte[] PlayerData(int id)
    {
        Packet packet = new Packet();
        packet.WriteLong((long)PacketTypes.PlayerData);
        packet.WriteInt(id);
        return packet.ToArray();
    }

    public void SendJoinGame(int id)
    {
        Packet packet = new Packet();

        for (int i = 0; i < Clients.Length; i++)
        {
            if (Clients[i].socket != null)
            {
                if (i != id)
                {
                    SendDataTo(id,PlayerData(i));
                }
            }
        }
        SendDataToAll(PlayerData(id));
    }
    
}