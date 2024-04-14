#region NameSpace

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Script.Character;
using Script.core;
using Script.Manager;
using TMPro;
using UnityEngine;
using NetworkUtility = Script.Manager.NetworkUtility;

#endregion

namespace Script.Network
{
    public class NetworkManager : MonoBehaviour
    {
        #region 变量
        public string IPAddress = "127.0.0.1";

        //服务器根据端口识别是receiver还是sender
        public int receiverPort = 7778;
        public int senderPort = 7779;
        public TcpClient receiverClient;
        public TcpClient senderClient;
        public int roomId = 00000;
        public string userName = "Admin";
        
        public static NetworkManager instance;

        public static Dictionary<int, NetworkObject> networkObjects = new();

        private List<Operation> operations = new List<Operation>();
        
        public PlayerEnum playerEnum = PlayerEnum.NotReady;

        /// <summary>
        /// 联机?
        /// </summary>
        public static bool isOnline = true;
        private static CancellationTokenSource cts = new CancellationTokenSource();

        #endregion

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            //第一次连接 发送房间号
            //Init() 在第一个场景中调用了
            senderClient = new TcpClient();
            receiverClient = new TcpClient();
        }
        #region 持续接收message并交给Executor执行

        //TryConnect和Init都是Request  Init返回的是对手的Init Operation
        public async UniTask Init()
        {
            // await ConnectUI();
            var initResp = await NetworkUtility.RequestAsync(NetworkManager.instance.receiverClient,
                JsonUtility.ToJson(new Operation(OperationType.TryConnectRoom, extraMessage: NetworkManager.instance.roomId.ToString())));
            var operation = JsonUtility.FromJson<Operation>(initResp);
            //networkObjects的0和1是Player和Opponent
            Myself.instance.networkId = NetworkManager.instance.playerEnum == PlayerEnum.Player1 ? 0 : 1;
            NetworkManager.networkObjects[Myself.instance.networkId] = Myself.instance;
            Opponent.instance.networkId = NetworkManager.instance.playerEnum == PlayerEnum.Player1 ? 1 : 0;
            NetworkManager.networkObjects[Opponent.instance.networkId] = Opponent.instance;
            // 等待server下达Init命令代表游戏开始
            await NetworkUtility.ReadAsync(NetworkManager.instance.receiverClient);
            var opponentInit = await NetworkUtility.RequestAsync(senderClient,
                JsonUtility.ToJson(new Operation(OperationType.Init, extraMessage: userName)));
            operation = JsonUtility.FromJson<Operation>(opponentInit);
            UIManager.instance.通知板.gameObject.SetActive(true);
            UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text =
                "your opponent is " + operation.extraMessage; //Init的extraMessage是username
            UniTask.Delay(3000).GetAwaiter()
                .OnCompleted((() => { UIManager.instance.通知板.gameObject.SetActive(false); }));
            var characterInfo = await NetworkUtility.RequestAsync(senderClient,
                JsonUtility.ToJson(new Operation(OperationType.Init,
                    extraMessage: string.Concat("Script.Character.",Myself.instance.characterName))));
            Type type = Type.GetType(JsonUtility.FromJson<Operation>(characterInfo).extraMessage);
            if(type==null) Debug.LogError("对方的角色名称错误");
            else
            {
                var component = Opponent.instance.gameObject.AddComponent(type);
                Opponent.instance.character = (CharacterBase)component;
            }
            MessageReceiver();
            senderClient.ReceiveTimeout = 1000;
            senderClient.SendTimeout = 1000;
            receiverClient.ReceiveTimeout = 1000;
            receiverClient.SendTimeout = 1000;
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
                    resp = await NetworkUtility.ReadAsync(receiverClient);
                    if (string.IsNullOrEmpty(resp))
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    return;
                }

                Operation operation = null;
                try
                {
                    operation = JsonUtility.FromJson<Operation>(resp);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
                //TODO 操作如何记录
            }
        }

        #endregion

        #region NetworkObject相关

        public static NetworkObject GetObjectById(int id)
        {
            if (id == -1) return null;
            networkObjects.TryGetValue(id, out var networkObject);
            return networkObject;
        }

        /// <summary>
        /// 申请创建NetworkObject, 代替Instantiate, 返回的baseNetworkObject中附带了新的networkId
        /// </summary>
        public static async UniTask<NetworkObject> InstantiateNetworkObject(string prefabName,
            Transform transform = null)
        {
            //单机
            if (!isOnline)
            {
                return InstantiateNetworkObjectLocal(prefabName, networkObjects.Count,
                    transform);
            }
            string resp = await NetworkUtility.RequestAsync(instance.senderClient,
                JsonUtility.ToJson(new Operation(OperationType.CreateObject,extraMessage:
                    prefabName))); // extraMessage 发送的是物体的名字
            var operation = JsonUtility.FromJson<Operation>(resp);
            var no = InstantiateNetworkObjectLocal(operation.extraMessage, operation.baseNetworkId,
                transform);
            return no;
        }

        /// <summary>
        /// 对方申请创建物体, 在本地创建
        /// </summary>
        /// <returns></returns>
        public static NetworkObject InstantiateNetworkObjectLocal(string prefabName, int networkId,
            Transform transform = null)
        {
            if (networkObjects.ContainsKey(networkId)) return networkObjects[networkId];
            var o = Instantiate(ObjectFactory.Instance.GetObject(prefabName), transform);
            o.networkId = networkId;
            networkObjects.Add(networkId, o);
            return o;
        }
        #endregion


        public static void CloseAll()
        {
            NetworkUtility.CloseAll(instance.receiverClient,instance.senderClient);
        }
    }
}