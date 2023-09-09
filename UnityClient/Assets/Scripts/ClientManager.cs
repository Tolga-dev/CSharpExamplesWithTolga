using System;
using System.Net.Sockets;
using UnityEngine;

namespace DefaultNamespace.TcpClients
{
    public class ClientManager : MonoBehaviour
    {
        private TcpClients client;
        private void Start()
        {
            client = GetComponent<TcpClients>();
            Debug.Log("Connecting");
            client.Connect();
        }

        private void OnApplicationQuit()
        {
            client.client.Close();
        }
    }
}