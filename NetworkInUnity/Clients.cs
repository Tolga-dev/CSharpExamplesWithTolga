using System.Net.Sockets;

namespace NetworkInUnity;

public class Clients
{
    public int id;
    public string ip;
    public TcpClient socket;
    public NetworkStream Stream;
    private byte[] readBuffer;
    private int bufferSize = 4096;
    public void Start()
    {
        socket.SendBufferSize = bufferSize;
        socket.ReceiveBufferSize = bufferSize;
        Stream = socket.GetStream();
        readBuffer = new byte[bufferSize];
        Stream.BeginRead(readBuffer, 0, socket.ReceiveBufferSize,ReceivedDataCallBack, null);
    }

    private void ReceivedDataCallBack(IAsyncResult result)
    {
        try
        {
            int readBytes = Stream.EndRead(result);
            if(readBytes <= 0) return;
            byte[] bytes = new byte[readBytes];
            Buffer.BlockCopy(readBuffer,0,bytes,0,readBytes);
            Stream.BeginRead(readBuffer, 0, socket.ReceiveBufferSize, ReceivedDataCallBack, null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}