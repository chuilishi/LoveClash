using System;
using System.Net;
using System.Net.Sockets;
using Cysharp.Threading.Tasks;
using EasyButtons;
using UnityEngine;

namespace Script.Network
{
    public class NetworkManager : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        private static Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

        public string IPAddress = "127.0.0.1";
        public int port = 7777;
        private static string m_IPAddress;
        private static int m_port = 7777;
        public int roomId = 00000;
        public static NetworkManager instance;

        private void Awake()
        {
            instance = this;
            m_IPAddress = IPAddress;
            m_port = port;
            try
            {
                socket.SendTimeout = 5000;
                socket.ConnectAsync(new IPEndPoint(System.Net.IPAddress.Parse(m_IPAddress), m_port));
            }
            catch (Exception e)
            {
                Debug.Log("连接失败");
                Console.WriteLine(e);
                throw;
            }
        }

        public static async void Send(int playerCode, int operationCode, int cardCode, int target)
        {
            if (!socket.Connected)
            {
                await socket.ConnectAsync(new IPEndPoint(System.Net.IPAddress.Parse(m_IPAddress), m_port));
            }
        }
    }
}