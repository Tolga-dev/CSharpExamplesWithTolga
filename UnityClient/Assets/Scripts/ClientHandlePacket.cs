using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.TcpClients
{
    public enum PacketTypes
    {
        Information = 1,
        ExecutionClient,
    }
    public class ClientHandlePacket : MonoBehaviour
    {
        public static Packet Packet = new Packet();

        private delegate void PacketHandler(byte[] data);

        private static Dictionary<long, PacketHandler> _packetHandlersDic;
        private static long _lenght;

        private void Awake()
        { 
            InitializePacket();
        }

        private static void InitializePacket()
        {
            _packetHandlersDic = new FlexibleDictionary<long, PacketHandler>();
            _packetHandlersDic.Add((long)PacketTypes.Information,PacketInformation);
        }


        public static void HandleData(byte[] data)
        {
            var buffer = (byte[])data.Clone();
            
            Packet.Write(buffer);

            if (Packet.Buffer.Count == 0)
            {
                Packet.Clear();
                return;
            }
            else if (Packet.Buffer.Count >= 8)
            {
                _lenght = Packet.Read<long>(false);
                if (_lenght <= 0)
                {
                    Packet.Clear();
                    return;
                }
            }

            while (_lenght > 0 & _lenght <= Packet.Buffer.Count - 8)
            {
                Packet.Read<long>();
                data = Packet.Read((int)_lenght);
                HandleDataPacket(data);
                _lenght = 0;

                if (Packet.Buffer.Count >= 8)
                {
                    _lenght = Packet.Read<long>(false);
                    if (_lenght < 0)
                    {
                        Packet.Clear();
                        return;
                    }
                }
            }
            
        }

        private static void HandleDataPacket(byte[] data)
        {
            PacketHandler packetHandler;
            var packet = new Packet();
            packet.Write(data);
            
            packet.Dispose();

            if (_packetHandlersDic.TryGetValue(packet.Read<long>(), out packetHandler))
            {
                 packetHandler.Invoke(data);
            }
        }

        private static void PacketInformation(byte[] data)
        {
            Packet packet = new Packet();
            packet.Write(data);

            var packetIdentifier = packet.Read<long>();
            var m1 = packet.Read<string>();
            var m2 = packet.Read<string>();
            var lvl = packet.Read<int>();
            
            Debug.Log(m1);
            Debug.Log(m2);
            Debug.Log(lvl);

        }

    }
}