using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkPractice.FtpServer.E4.Server;

public class Server
{
    
    private TcpListener _listener;

    public Server()
    {
    }

    public void Start()
    {
        _listener = new TcpListener(IPAddress.Any, 5555);
        _listener.Start();
        while (true)
        {
            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
            Thread.Sleep(1000);        
        }
    }

    public void Stop()
    {
        if (_listener != null)
        {
            _listener.Stop();
        }
    }

    private void HandleAcceptTcpClient(IAsyncResult result)
    {
        _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
        TcpClient client = _listener.EndAcceptTcpClient(result);

        NetworkPractice.FtpServer.E4.Client.Client connection = new Client.Client(client);
        
        ThreadPool.QueueUserWorkItem(connection.HandleClient, client);
    }
    
    
}