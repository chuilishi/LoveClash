using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using EasyButtons;
using Script.core;
using Script.Manager;
using UnityEditor.PackageManager;
using UnityEngine;
using Object = System.Object;


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
        
        public static NetworkManager instance;
        
        public static Dictionary<int,NetworkObject> networkObjectDict = new();
        public static Dictionary<int,NetworkObject> networkObjects = new();

        private OperationExecutor operationExecutor;

        private List<string> operations = new List<string>();

        private static int operationCount = 0;

        public static bool isSync = true;
        
        [HideInInspector]
        public PlayerEnum playerEnum;

        private async void Awake()
        {
            instance = this;
            m_IPAddress = IPAddress;
            m_port = port;
            operationExecutor = GetComponent<OperationExecutor>();
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
            await Connect();
            stream = client.GetStream();
            Receiver();
            SendAsync(JsonUtility.ToJson(new Operation(OperationType.CreateRoom, extraMessage: roomId.ToString())));
        }
        public async void Receiver()
        {
            while (true)
            {
                try
                {
                    string resp = await ReceiveAsync();
                    operations.Add(resp);
                    var fromJson = JsonUtility.FromJson<Operation>(resp);
                    if (fromJson.playerEnum != playerEnum) operationCount++;
                    OperationExecutor.Execute(resp);
                    if (operations.Count == operationCount) isSync = true;
                }
                catch (Exception e)
                {
                    Debug.Log("Receiver Error "+e);
                    await Connect();
                    continue;
                }
            }
        }

        
        #endregion
        #region NetworkObject相关

        public static NetworkObject GetObjectById(int id)
        {
            if (id >= networkObjects.Count)
            {
                Debug.Log("Invalid Id");
                return null;
            }
            return networkObjectDict[id];
        }

        
        public static void AddObject(NetworkObject networkObject,int customId = -1)
        {
            if (customId != -1)
            {
                try
                {
                    networkObjects.Add(customId,networkObject);
                }
                catch (Exception e)
                {
                    networkObjects[customId] = networkObject;
                }
                
            }
            else if (networkObject.networkId == 0)
            {
                Debug.LogError("networkId 添加时不能为0");
            }
            networkObjects.Add(networkObject.networkId,networkObject);
        }

        /// <summary>
        /// 代替Instantiate
        /// </summary>
        /// <param name="name"></param>
        public static NetworkObject CreateNewNetworkObject(string name,int id=-1)
        {
            try
            {
                var o = Instantiate(ObjectFactory.instance.nameToObject[name]);
                o.networkId = networkObjects.Count;
                AddObject(o,id);
                return o;
            }
            catch (Exception e)
            {
                Debug.LogError("name error "+e);
                throw;
            }
        }
        
        #endregion
        #region 基础网络方法

        /// <summary>
        /// 单次接收数据
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static async UniTask<string> ReceiveAsync()
        {
            var buffer = new byte[4096];
            stream.ReadTimeout = TimeSpan.FromSeconds(5).Milliseconds;
            var size = await stream.ReadAsync(buffer,0,buffer.Length);
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
            try
            {
                await client.ConnectAsync(m_IPAddress, m_port);
                UIManager.instance.通知板.gameObject.SetActive(true);
                UIManager.instance.通知板.text = "连接成功";
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                UIManager.instance.通知板.gameObject.SetActive(false);
            }
            catch (Exception e)
            {
                Debug.Log("连接失败\n"+e);
                UIManager.instance.通知板.text = "连接失败,尝试重新连接中";
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
                isSync = false;
                operationCount++;
                Byte[] buffer = Encoding.ASCII.GetBytes(value);
                await client.GetStream().WriteAsync(buffer,0,buffer.Length);
            }
            catch (Exception e)
            {
                Debug.Log("发送失败"+e);
                throw;
            }
        }
        #endregion
    }
}