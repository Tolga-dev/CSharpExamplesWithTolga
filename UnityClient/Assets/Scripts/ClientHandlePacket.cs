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
        PlayerData
    }

    public enum ClientPacketTypes
    {
        Information = 1,
        
    }
    public class ClientHandlePacket : MonoBehaviour
    {
        public static Packet Packet;
        public GameObject player;
        public static Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();
        private delegate void Packet_(byte[] data);

        private static Dictionary<long, Packet_> packets;
        private static long _lenght;
        private TcpClients _tcpClients = new TcpClients();
        private void Awake()
        { 
            InitializePacket();
        }

        private void InitializePacket()
        {
            packets = new Dictionary<long, Packet_>();
            packets.Add((long)PacketTypes.Information,PacketInformation);
            packets.Add((long)PacketTypes.ExecutionClient,PacketExecution);
            packets.Add((long)PacketTypes.PlayerData,PacketPlayerData);
        }


        public static void HandleData(byte[] data)
        {
            var buffer = (byte[])data.Clone();

            if (Packet == null) Packet = new Packet();
            
            Packet.WriteByte(buffer);

            if (Packet.Count() == 0)
            {
                Packet.Clear();
                return;
            }
            
            if (Packet.Length() >= 8)
            {
                _lenght = Packet.ReadLong(false);
                if (_lenght <= 0)
                {
                    Packet.Clear();
                    return;
                }
            }

            while (_lenght > 0 & _lenght <= Packet.Length() - 8)
            {
                if (_lenght <= Packet.Length() - 8)
                {
                    Packet.ReadLong();
                    data = Packet.ReadByte((int)_lenght);
                    HandleDataPacket(data);
                    
                }
                _lenght = 0;

                if (Packet.Length() >= 8)
                {
                    _lenght = Packet.ReadLong(false);
                    if (_lenght < 0)
                    {
                        Packet.Clear();
                        return;
                    }
                }
            }
            
        }

        private static void HandleDataPacket(byte[] data) //
        {
            long packetIdentifier;
            var packet = new Packet();
            Packet_ packetHandler;
            
            packet.WriteByte(data);
            packetIdentifier = packet.ReadLong();
            packet.Dispose();
            
            if (packets.TryGetValue(packetIdentifier, out packetHandler))
            {
                 packetHandler.Invoke(data);
            }
        }

        private static void PacketInformation(byte[] data)
        {
            Packet packet = new Packet();
            packet.WriteByte(data);

            var packetIdentifier = packet.ReadLong();
           var m1 = packet.ReadString();
          var m2 = packet.ReadString();
           var lvl = packet.ReadInt();
            
            
            Debug.Log(m1);
            Debug.Log(m2);
            Debug.Log(lvl);
            
            TcpClients.instance.SendInformation();
            
        }

        private static void PacketExecution(byte[] data)
        {
            Debug.Log("Executed from the server!");
        }
        private void PacketPlayerData(byte[] data)
        {
            Packet packet = new Packet();
            packet.WriteByte(data);

            var packetIdentifier = packet.ReadLong();
            var id = packet.ReadInt();

            GameObject NewPlayer = player;
            NewPlayer.name = "Player: " + id;
            playerList.Add(id,NewPlayer);

            Instantiate(playerList[id]);

        }


    }
}