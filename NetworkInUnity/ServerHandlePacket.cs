namespace NetworkInUnity;

public class ServerHandlePacket
{

    private delegate void Packet_(long id, byte[] data);
    private static Dictionary<long, Packet_> packets;
    private static long _lenght;
    
    public static void InitializePacket()
    {
        packets = new Dictionary<long, Packet_>();
        packets.Add((long)ClientPacketTypes.Information,PacketInformation);
    }


    public static void HandleData(long id,byte[] data)
    {
        var buffer = (byte[])data.Clone();

        if (TcpServer.Clients[id].Packet == null)
        {
            TcpServer.Clients[id].Packet = new Packet();
        }
        
        TcpServer.Clients[id].Packet.WriteByte(buffer);
        
        if (TcpServer.Clients[id].Packet.Count() == 0)
        {
            TcpServer.Clients[id].Packet.Clear();
            return;
        }
        
        if (TcpServer.Clients[id].Packet.Length() >= 4)
        {
            _lenght = TcpServer.Clients[id].Packet.ReadLong(false);
            if (_lenght <= 0)
            {
                TcpServer.Clients[id].Packet.Clear();
                return;
            }
        }

        while (_lenght > 0 & _lenght <= TcpServer.Clients[id].Packet.Length() - 8)
        {
            if (_lenght <= TcpServer.Clients[id].Packet.Length() - 8)
            {
                TcpServer.Clients[id].Packet.ReadLong();
                data = TcpServer.Clients[id].Packet.ReadByte((int)_lenght);
                HandleDataPacket(id, data);

            }
            _lenght = 0;

            if (TcpServer.Clients[id].Packet.Length() >= 4)
            {
                _lenght = TcpServer.Clients[id].Packet.ReadLong(false);
                if (_lenght < 0)
                {
                    TcpServer.Clients[id].Packet.Clear();
                    return;
                }
            }
            if(_lenght < 1)
                TcpServer.Clients[id].Packet.Clear();

        }
        
    }

    private static void HandleDataPacket(long id,byte[] data) //
    {
        long packetIdentifier;
        var packet = new Packet();
        Packet_ packetHandler;
        
        packet.WriteByte(data);
        packetIdentifier = packet.ReadLong();
        packet.Dispose();
        
        if (packets.TryGetValue(packetIdentifier, out packetHandler))
        {
             packetHandler.Invoke(id,data);
        }
    }

    private static void PacketInformation(long id,byte[] data)
    {
        Packet packet = new Packet();
        packet.WriteByte(data);
        long packetIdentifier = packet.ReadLong();
        string msg = packet.ReadString();
        
        Console.WriteLine(msg);

    }

    private static void PacketExecution(byte[] data)
    {
    }

}