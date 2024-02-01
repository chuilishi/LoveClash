using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using EasyButtons;
using Script.core;
using Script.Manager;
using TMPro;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;


namespace Script.Network
{
    /// <summary>
    /// 逻辑: 第一次连接成功后发送房间号, 得到确认消息, 对局开始
    /// 
    /// </summary>
    [RequireComponent(typeof(OperationExecutor))]
    
    public class NetworkManager : MonoBehaviour
    {
        private static TcpClient client = new TcpClient();
        public string IPAddress = "127.0.0.1";
        public int port = 7777;
        private static string m_IPAddress;
        private static int m_port = 7777;
        private static NetworkStream stream;
        public int roomId = 00000;
        public string userName = "Admin";
        #if UNITY_EDITOR
        public static bool quit = false;
        #endif
        
        public static NetworkManager instance;
        
        public static Dictionary<int,NetworkObject> networkObjects = new();

        private List<Operation> operations = new List<Operation>();
        
        public static PlayerEnum playerEnum = PlayerEnum.NotReady;

        private static CancellationTokenSource receiverCts = new CancellationTokenSource();

        private async void Awake()
        {
            instance = this;
            m_IPAddress = IPAddress;
            m_port = port;
            //第一次连接 发送房间号
            Init();
        }
        public static void Execute(Operation operation)
        {
            OperationExecutor.Execute(operation);
        }
        
        #region 持续接收message并交给Executor执行
        private async void Init()
        {
            Connect();
            await UniTask.WaitUntil((() => client.Connected));
            stream = client.GetStream();
            var s = await Request(JsonUtility.ToJson(new Operation(OperationType.TryConnectRoom, extraMessage: roomId.ToString())));
            Debug.Log(s);
            var operation = JsonUtility.FromJson<Operation>(s);
            if (operation.operationType == OperationType.TryConnectRoom)
            {
                if (operation.playerEnum == PlayerEnum.Player1)
                {
                    playerEnum = PlayerEnum.Player1;
                    //等玩家2进入
                    operation = JsonUtility.FromJson<Operation>(await ReceiveAsync());
                }
                else
                {
                    playerEnum = PlayerEnum.Player2;
                }
                
            }
            //第二个信息是Init(两次,一次自己一次对手的) 名字(extraMessage) 和 PlayerPrefab(baseNetworkObject)//TODO 暂时没有角色
            var opponentName = "";
            var initStr = await ReceiveAsync();
            operation = JsonUtility.FromJson<Operation>(initStr);
            if (operation.playerEnum != playerEnum)
            {
                opponentName = operation.extraMessage;
            }
            
            initStr = await ReceiveAsync();
            operation = JsonUtility.FromJson<Operation>(initStr);
            if (operation.playerEnum != playerEnum)
            {
                opponentName = operation.extraMessage;
            }
            UIManager.instance.通知板.gameObject.SetActive(true);
            UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "你的对手是 " + opponentName;
            UniTask.Delay(3000).GetAwaiter().OnCompleted((() =>
            {
                UIManager.instance.通知板.gameObject.SetActive(false);
            }));
            Receiver();
        }
        /// <summary>
        /// 主要消息接收器
        /// </summary>
        public async void Receiver()
        {
            while (true)
            {
                //对方回合开始时 , 取消的token被重新设置
                await UniTask.WaitUntil((() => receiverCts.Token.IsCancellationRequested == false));
                string resp = "";
                try
                {
                    resp = await ReceiveAsync();
                }
                catch (TaskCanceledException e)
                {
                    continue;
                }
                var fromJson = JsonUtility.FromJson<Operation>(resp);
                if(fromJson.recordable) operations.Add(fromJson);
                OperationExecutor.Execute(fromJson);
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
        /// 本地创建NetworkObject, 代替Instantiate
        /// </summary>
        public static async UniTask<NetworkObject> InstantiateNetworkObject(string name)
        {
            string resp = await Request(JsonUtility.ToJson(new Operation(OperationType.GetObjectId, playerEnum)));
            var id = int.Parse(JsonUtility.FromJson<Operation>(resp).extraMessage);
            var o = Instantiate(ObjectFactory.instance.nameToObject[name]);
            o.networkId = id;
            networkObjects.Add(id,o);
            return o;
        }
        
        #endregion
        #region 基础网络方法

        /// <summary>
        /// 单次接收数据
        /// </summary>
        private static async UniTask<string> ReceiveAsync()
        {
            var buffer = new byte[4096];
            stream.ReadTimeout = 10000;
            int size = 0;
            size = await stream.ReadAsync(buffer,0,buffer.Length,receiverCts.Token);
            var formatted = new byte[size];
            Array.Copy(buffer,formatted,size);
            return Encoding.ASCII.GetString(formatted);
        }
        /// <summary>
        /// 单次连接(无自动重连)
        /// </summary>
        public async static UniTask Connect()
        {
            UIManager.instance.通知板.gameObject.SetActive(true);
            UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "正在连接中";
            try
            {
                await client.ConnectAsync(m_IPAddress, m_port);
                UIManager.instance.通知板.gameObject.SetActive(true);
                UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "连接成功";
                UniTask.Delay(TimeSpan.FromSeconds(1)).GetAwaiter().OnCompleted(() =>
                {
                    UIManager.instance.通知板.gameObject.SetActive(false);
                });
            }
            catch (Exception e)
            {
                Debug.Log("连接失败\n"+e);
                UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text = "连接失败,尝试重新连接中";
                throw;
            }
        }
        public static async UniTask SendAsync(string value)
        {
            if (!client.Connected)
            {
                while (true)
                {
                    try
                    {
                        await Connect();
                    }
                    catch (Exception e)
                    {
                        continue;
                        throw;
                    }
                }
            }
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

        public static async UniTask<string> Request(string value)
        {
            SendAsync(value);
            return await ReceiveAsync();
        }
        #endregion
    }
}