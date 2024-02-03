#region NameSpace

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Script.core;
using Script.Manager;
using TMPro;
using UnityEngine;

#endregion

namespace Script.Network
{
    [RequireComponent(typeof(OperationExecutor))]
    public class NetworkManager : MonoBehaviour
    {
        #region 变量

        public string IPAddress = "127.0.0.1";
        //服务器根据端口识别是receiver还是sender
        public int receiverPort = 7777;
        public int senderPort = 7778;
        public static TcpClient receiverClient;
        public static TcpClient senderClient;
        public int roomId = 00000;
        public string userName = "Admin";
        
        public static NetworkManager instance;
        
        public static Dictionary<int,NetworkObject> networkObjects = new();

        private List<Operation> operations = new List<Operation>();
        
        public static PlayerEnum playerEnum = PlayerEnum.NotReady;

        #endregion
        
        private void Awake()
        {
            instance = this;
            senderClient = new TcpClient();
            receiverClient = new TcpClient();
            //第一次连接 发送房间号
            Init();
        }
        #region 持续接收message并交给Executor执行
        //TryConnect和Init都是Request  Init返回的是对手的Init Operation
        private async void Init()
        {
            Connect(senderClient,IPAddress,senderPort);
            Connect(receiverClient,IPAddress,receiverPort);
            await ConnectUI();
            var task1 = RequestAsync(senderClient,
                JsonUtility.ToJson(new Operation(OperationType.TryConnectRoom, extraMessage: roomId.ToString())));
            task1.GetAwaiter().OnCompleted((() =>
            {
                var operation = JsonUtility.FromJson<Operation>(task1.GetAwaiter().GetResult());
                playerEnum = operation.playerEnum;
            }));
            var task2 = RequestAsync(receiverClient,
                JsonUtility.ToJson(new Operation(OperationType.TryConnectRoom, extraMessage: roomId.ToString())));
            task2.GetAwaiter().OnCompleted((() =>
            {
                var operation = JsonUtility.FromJson<Operation>(task2.GetAwaiter().GetResult());
                playerEnum = operation.playerEnum;
            }));
            //等待server下达Init命令代表游戏开始
            await ReceiveAsync(receiverClient);
            
            Debug.Log("开始游戏");
            //Request, 并且返回对手的Init信息
            var opponentInit = await RequestAsync(senderClient,
                JsonUtility.ToJson(new Operation(OperationType.Init, playerEnum, extraMessage: userName)));
            var operation = JsonUtility.FromJson<Operation>(opponentInit);
            UIManager.instance.通知板.gameObject.SetActive(true);
            UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "your opponent is " + operation.extraMessage;//Init的extraMessage是username
            UniTask.Delay(3000).GetAwaiter().OnCompleted((() =>
            {
                UIManager.instance.通知板.gameObject.SetActive(false);
            }));
            MessageReceiver();
            GameManager.instance.Main();
        }
        /// <summary>
        /// 主要消息接收器
        /// </summary>
        public async void MessageReceiver()
        {
            while (true)
            {
                string resp = "";
                try
                {
                    resp = await ReceiveAsync(receiverClient);
                }
                catch (Exception e)
                {
                    continue;
                }
                var operation = JsonUtility.FromJson<Operation>(resp);
                //TODO 操作如何记录
                // operations.Add(fromJson);
                OperationExecutor.Execute(operation);
            }
        }
        
        #endregion
        #region NetworkObject相关
        public static NetworkObject GetObjectById(int id)
        {
            networkObjects.TryGetValue(id,out var networkObject);
            return networkObject;
        }
        /// <summary>
        /// 本地创建NetworkObject, 代替Instantiate, 返回的baseNetworkObject中附带了新的networkId
        /// </summary>
        public static async UniTask<NetworkObject> InstantiateNetworkObject(ObjectEnum objectEnum)
        {
            var o = Instantiate(ObjectFactory.instance.nameToObject[objectEnum]);
            string resp = await RequestAsync(senderClient,JsonUtility.ToJson(new Operation(OperationType.CreateObject,playerEnum,baseNetworkObject:o)));
            var id = JsonUtility.FromJson<NetworkObjectJson>(
                JsonUtility.FromJson<Operation>(resp).baseNetworkObjectJson).networkId;
            o.networkId = id;
            networkObjects.Add(id,o);
            return o;
        }
        
        #endregion
        #region 基础网络方法

        /// <summary>
        /// 单次接收数据
        /// </summary>
        private static async UniTask<string> ReceiveAsync(TcpClient client)
        {
            var buffer = new byte[4096];
            client.GetStream().ReadTimeout = 10000;
            int size = 0;
            size = await client.GetStream().ReadAsync(buffer,0,buffer.Length);
            var formatted = new byte[size];
            Array.Copy(buffer,formatted,size);
            return Encoding.ASCII.GetString(formatted);
        }
        /// <summary>
        /// 单次连接(无自动重连)
        /// </summary>
        public async UniTask Connect(TcpClient client,string ip,int port)
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
        public async UniTask ConnectUI()
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
                await UniTask.WhenAll(UniTask.WaitUntil((() => receiverClient.Connected),cancellationToken:cts.Token),
                    UniTask.WaitUntil((() => senderClient.Connected),cancellationToken:cts.Token));
                UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "连接成功";
                UniTask.Delay(2000).GetAwaiter().OnCompleted((() =>
                {
                    UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "";
                    UIManager.instance.通知板.SetActive(false);
                }));
            }
            catch (TaskCanceledException e)
            {
                UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "连接超时";
                UniTask.Delay(2000).GetAwaiter().OnCompleted((() =>
                {
                    UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "";
                    UIManager.instance.通知板.SetActive(false);
                }));
            }
        }
        public static async UniTask SendAsync(TcpClient client ,string value)
        {
            try
            {
                Byte[] buffer = Encoding.ASCII.GetBytes(value);
                await client.GetStream().WriteAsync(buffer,0,buffer.Length);
            }
            catch (Exception e)
            {
                Debug.Log("发送失败"+e);
                throw;
            }
        }

        public static async UniTask<string> RequestAsync(TcpClient client,string value)
        {
            SendAsync(client,value);
            return await ReceiveAsync(client);
        }
        #endregion
    }
}