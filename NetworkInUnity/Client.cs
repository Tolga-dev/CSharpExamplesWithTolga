namespace NetworkInUnity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class Client
{
    public static int dataBufferSize = 4096;

    public int id;
    public Tcp tcp;

    public Client(int _id)
    {
        id = _id;
        tcp = new Tcp(_id);
    }

    public class Tcp
    {
        public TcpClient socket;

        private readonly int id;
        private NetworkStream _stream;
        private byte[] receiveBuffer;

        public Tcp(int _id)
        {
            id = _id;
        }

        public void Connect(TcpClient _socket)
        {
            socket = _socket;
            socket.ReceiveBufferSize = dataBufferSize;
            socket.SendBufferSize = dataBufferSize;

            _stream = socket.GetStream();

            receiveBuffer = new byte[dataBufferSize];

            _stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int _byteLength = _stream.EndRead(result);
                if (_byteLength <= 0)
                {
                    return;
                }

                byte[] _data = new byte[_byteLength];
                Array.Copy(receiveBuffer,_data,_byteLength);
                
                _stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error receiving TCP data: {_ex}");
                // TODO: disconnect
            }
            
        }

    }
    

}