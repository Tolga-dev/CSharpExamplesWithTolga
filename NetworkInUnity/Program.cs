﻿using System;
using System.Text;

namespace NetworkInUnity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerHandlePacket.InitializePacket();
            
            TcpServer tcpServer = new TcpServer();
            tcpServer.Init();
            
            Console.ReadKey();
            
        }
        
        
    }
}

