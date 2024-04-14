using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Script.Network;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Script.Manager
{
    public class NetworkUtility
    {
        #region 基础网络方法
        /// <summary>
        /// 单次接收数据
        /// </summary>
        public static async UniTask<string> ReadAsync(TcpClient client)
        {
            //先预读一下长度(前4个byte, 即一个字节)
            var buffer = new byte[4];
            int total = 0;
            var respSize = await client.GetStream().ReadAsync(buffer, 0, 4);
            total += respSize;
            //一直读直到total到4为止
            while (total<4)
            {
                respSize = await client.GetStream().ReadAsync(buffer,total,4-total);
                total += respSize;
            }
            int messageSize = BitConverter.ToInt32(buffer, 0);
            buffer = new byte[messageSize];
            messageSize = await client.GetStream().ReadAsync(buffer,0,messageSize);
            var formatted = new byte[messageSize];
            Array.Copy(buffer,formatted,messageSize);
            var s = Encoding.UTF8.GetString(formatted);
            Debug.Log("读入的是"+s);
            return s;
        }
        /// <summary>
        /// 单次连接(无自动重连)
        /// </summary>
        public static async UniTask Connect(TcpClient client,string ip,int port)
        {
            try
            {
                await client.ConnectAsync(ip,port);
                Debug.Log("连接成功");
            }
            catch (Exception e)
            {
                Debug.Log("连接失败\n"+e);
                throw;
            }
        }
        
        /// <summary>
        /// 两个都连接上的时候显示连接成功,顺便起到等待全部连接的功能
        /// </summary>
        public async UniTask ConnectUI(TcpClient client1,TcpClient client2)
        {
            UIManager.instance.通知板.gameObject.SetActive(true);
            UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "正在连接中";
            CancellationTokenSource cts = new CancellationTokenSource();
            Task.Delay(5000).GetAwaiter().OnCompleted((() =>
            {
                cts.Cancel();
            }));
            try
            {
                await UniTask.WhenAll(UniTask.WaitUntil((() => client1.Connected),cancellationToken:cts.Token),
                    UniTask.WaitUntil((() => client2.Connected),cancellationToken:cts.Token));
                UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "连接成功";
                UniTask.Delay(2000).GetAwaiter().OnCompleted((() =>
                {
                    UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "";
                    UIManager.instance.通知板.SetActive(false);
                }));
            }
            catch (TaskCanceledException)
            {
                UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "连接超时";
                UniTask.Delay(2000).GetAwaiter().OnCompleted((() =>
                {
                    UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "";
                    UIManager.instance.通知板.SetActive(false);
                }));
            }
        }
        public static async UniTask WriteAsync(TcpClient client,string value)
        {
            Byte[] buffer = Encoding.UTF8.GetBytes(value);
            //把一个代表数据长度的int写在前面
            var bufferHead = BitConverter.GetBytes(buffer.Length);
            byte[] bufferToBeSend = new byte[bufferHead.Length+buffer.Length];
            Array.Copy(bufferHead,0,bufferToBeSend,0,bufferHead.Length);
            Array.Copy(buffer,0,bufferToBeSend,bufferHead.Length,buffer.Length);
            await client.GetStream().WriteAsync(bufferToBeSend,0,bufferToBeSend.Length);
        }
        public static async UniTask<string> RequestAsync(TcpClient client,string value)
        {
            string s = "";
            _ = WriteAsync(client,value);
            s = await ReadAsync(client);
            return s;
        }
        
        public static async UniTask<string> RequestAsync(string value)
        {
            var client = NetworkManager.instance.senderClient;
            string s = "";
            _ = WriteAsync(client,value);
            try
            {
                s = await ReadAsync(client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return s;
        }
        
        public static void CloseAll(params TcpClient[] clients)
        {
            Task.Run((() =>
            {
                foreach (var client in clients)
                {
                    client.Close();
                }
            }));
        }
        #endregion
#if (!UNITY_WEBGL||UNITY_EDITOR)
        
        //
        // public static async UniTask<string> ReadAsync(WebSocket ws)
        // {
        //     
        // }
        //
        // public static async UniTask<string> WriteAsync(WebSocket ws)
        // {
        //     string str = "http://xxx.xxx.xxx.xxx?xx";
        //     var httpRequest = new HTTPRequest(new Uri(str)).Send();
        //     
        // }
#endif
        
    }
}