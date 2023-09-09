using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using UnityEngine;

namespace DefaultNamespace.TcpClients
{
    public class TcpClients: MonoBehaviour
    {
        
        public TcpClient client;
        public NetworkStream Stream;
        private byte[] buffer;
        public bool isConnected;
        private int bufferSize = 4096;

        public byte[] receivedBytes;
        public bool handleData = false;
        
        private string IP_ADDRESS = "127.0.0.1";
        private int PORT = 5555;
        
        /*private void Update()
        {
            if (handleData == true)
            {
                Debug.Log("Handling Data");
                ClientHandlePacket.HandleData(receivedBytes);
                handleData = false;
            }
        }*/

        public void Connect()
        {
            client = new TcpClient();
            client.ReceiveBufferSize = bufferSize;
            client.SendBufferSize = bufferSize;
            buffer = new byte[8192];
            Debug.Log("Starting");
            
            
            try
            {
                client.BeginConnect(IP_ADDRESS, PORT, new AsyncCallback(ConnectCallBack), client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); 
                throw;
            }
        }

        private void ConnectCallBack(IAsyncResult result)
        {
            Debug.Log("Connecting!");
            try
            {
                client.EndConnect(result);
                if (client.Connected == false)
                {
                    Debug.Log("Lost server");
                    return;
                }
                else
                {
                    isConnected = true;
                    Debug.Log("Entered!");
                    Stream = client.GetStream();
                    Debug.Log("Reading!");
                  //  Stream.BeginRead(buffer, 0, 8192, OnReceiveData, null);
                }
            }
            catch (Exception e)
            {
                
                isConnected = false;
                return;
            }
        }

        private void OnReceiveData(IAsyncResult result)
        {   
            try
            {
                
                var lenght = Stream.EndRead(result);
                Debug.Log("Data Reading!  " + lenght);

                receivedBytes = new byte[lenght];
                
                Buffer.BlockCopy(buffer,0,receivedBytes,0,lenght);

                if (lenght == 0)
                {
                    Debug.Log("Disconnected");
                    Application.Quit();
                    return;  
                }

                handleData = true;
                Debug.Log("Data Received");
                Stream.BeginRead(buffer, 0, 8192, OnReceiveData, null);

            }
            catch (Exception e)
            {
                Debug.Log("Disconnected");
                Console.WriteLine(e);
                throw;
            }
            
        }



    }
}