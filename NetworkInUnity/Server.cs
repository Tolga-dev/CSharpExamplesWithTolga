namespace NetworkInUnity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class Server
{
    public static int MaxPlayer { get; private set; }
    public static int Port { get; private set; }
    public static Dictionary<int, Client> clients = new Dictionary<int, Client>();

    private static TcpListener _tcpListener;

    public static void Start(int _maxPlayer, int _port)
    {
        MaxPlayer = _maxPlayer;
        Port = _port;
        InitializeServerData();
        Console.WriteLine("Server Starting!");

        _tcpListener = new TcpListener(IPAddress.Any, Port);
        _tcpListener.Start();
        _tcpListener.BeginAcceptTcpClient(TcpConnectCallBack, null);
        
        Console.WriteLine($"Server started on port {Port}.");
    }

    private static void TcpConnectCallBack(IAsyncResult _asyncResult)
    {
        var client = _tcpListener.EndAcceptTcpClient(_asyncResult);
        _tcpListener.BeginAcceptTcpClient(TcpConnectCallBack, null);
        Console.WriteLine($"Incoming connection from {client.Client.RemoteEndPoint}...");

        
        DoJobWithMaxPlayer(i =>
        {
            if (clients[i].tcp.socket == null)
            {
                clients[i].tcp.Connect(client);
                return;
            }
        });
        
    }

    private static void InitializeServerData() => DoJobWithMaxPlayer(i => {   clients.Add(i, new Client(i)); });

    private static void DoJobWithMaxPlayer(Action<int> action)
    {
        for (var i = 1; i <= MaxPlayer; i++)
        {
            action(i);
        }
    }

}