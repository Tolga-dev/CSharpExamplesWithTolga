using System.Net;
using System.Net.Sockets;

namespace NetworkInUnity;

public class TcpServer
{
    private TcpListener _socket;

    public Clients[] Clients = new Clients[50]; 
    public void Init()
    {
        _socket = new TcpListener(IPAddress.Any, 5555);
        _socket.Start();
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
                return;
            }
            
        }
    }
    
    
}