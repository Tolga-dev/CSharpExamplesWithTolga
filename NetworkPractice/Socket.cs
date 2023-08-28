namespace NetworkPractice;
using System.Net;
using System.Net.Sockets;
using System.Text;
     
internal class SocketGenerator
{
    // a TCP/IP socket for IPv4 addresses
    private System.Net.Sockets.Socket _socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    // configure connection
    private void SetSocketOption() =>
        _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

    
    private void SimpleConnection(string address, int port)
    {
        try
        {
            SetSocketOption();
            var remoteEndpoint = new IPEndPoint(IPAddress.Parse(address), port);
            
            _socket.Connect(remoteEndpoint);
    
            byte[] dataToSend = Encoding.ASCII.GetBytes("Hello, World!");
            int bytesSent = _socket.Send(dataToSend);
            byte[] dataReceived = new byte[1024];
            int bytesRead = _socket.Receive(dataReceived);
    
            Console.WriteLine($"Bytes sent: {bytesSent}");
            Console.WriteLine($"Bytes received: {bytesRead}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            _socket.Close();
        }
    }

    private async void AsynchronousConnection(string address, int port)
    {
        try
        {
            SetSocketOption();
            var remoteEndpoint = new IPEndPoint(IPAddress.Parse(address), port);
            
            byte[] dataToSend = Encoding.ASCII.GetBytes("Hello, World!");
            byte[] dataReceived = new byte[1024];
    
            await _socket.ConnectAsync(remoteEndpoint);
            int bytesSent = await _socket.SendAsync(new ArraySegment<byte>(dataToSend), SocketFlags.None);
            int bytesRead = await _socket.ReceiveAsync(new ArraySegment<byte>(dataReceived), SocketFlags.None);
            
            Console.WriteLine($"Bytes sent: {bytesSent}");
            Console.WriteLine($"Bytes received: {bytesRead}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            _socket.Close();
        }
        // Connecting asynchronously
       
    }
    public void Runner(string address, int port)
    {
        SimpleConnection(address, port);
//            AsynchronousConnection(address,port);
    }

}
