#region NameSpace

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.WebSockets;
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

        public static Dictionary<int, NetworkObject> networkObjects = new();

        private List<Operation> operations = new List<Operation>();

        public static PlayerEnum playerEnum = PlayerEnum.NotReady;

        private static CancellationTokenSource cts = new CancellationTokenSource();

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
            UniTask.WhenAll(NetworkUtility.Connect(senderClient, IPAddress, senderPort),
                NetworkUtility.Connect(receiverClient, IPAddress, receiverPort));
            // await ConnectUI();
            var task1 = NetworkUtility.RequestAsync(senderClient,
                JsonUtility.ToJson(new Operation(OperationType.TryConnectRoom, extraMessage: roomId.ToString())));
            task1.GetAwaiter().OnCompleted((() =>
            {
                var operation = JsonUtility.FromJson<Operation>(task1.GetAwaiter().GetResult());
                playerEnum = operation.playerEnum;
            }));
            var s = await NetworkUtility.RequestAsync(receiverClient,
                JsonUtility.ToJson(new Operation(OperationType.TryConnectRoom, extraMessage: roomId.ToString())));
            var operation = JsonUtility.FromJson<Operation>(s);
            playerEnum = operation.playerEnum;
            //networkObjects的0和1是Player和Opponent
            Player.instance.networkId = playerEnum == PlayerEnum.Player1 ? 0 : 1;
            networkObjects[Player.instance.networkId] = Player.instance;
            Opponent.instance.networkId = playerEnum == PlayerEnum.Player1 ? 1 : 0;
            networkObjects[Opponent.instance.networkId] = Opponent.instance;
            // 等待server下达Init命令代表游戏开始
            await NetworkUtility.ReadAsync(receiverClient);
            Debug.Log("开始游戏");
            //Request, 并且返回对手的Init信息
            var opponentInit = await NetworkUtility.RequestAsync(senderClient,
                JsonUtility.ToJson(new Operation(OperationType.Init, playerEnum, extraMessage: userName)));
            operation = JsonUtility.FromJson<Operation>(opponentInit);
            UIManager.instance.通知板.gameObject.SetActive(true);
            UIManager.instance.通知板.GetComponentInChildren<TMP_Text>().text =
                "your opponent is " + operation.extraMessage; //Init的extraMessage是username
            UniTask.Delay(3000).GetAwaiter()
                .OnCompleted((() => { UIManager.instance.通知板.gameObject.SetActive(false); }));
            MessageReceiver();
            senderClient.ReceiveTimeout = 1000;
            senderClient.SendTimeout = 1000;
            receiverClient.ReceiveTimeout = 1000;
            receiverClient.SendTimeout = 1000;
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
                    resp = await NetworkUtility.ReadAsync(receiverClient);
                    if (string.IsNullOrEmpty(resp))
                    {
                        break;
                    }
                }
                catch (Exception e)
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
                OperationExecutor.Execute(operation);
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
        public static async UniTask<NetworkObject> InstantiateNetworkObject(ObjectEnum objectEnum,
            Transform transform = null)
        {
            var o = Instantiate(ObjectFactory.instance.nameToObject[objectEnum], transform);
            string resp = await NetworkUtility.RequestAsync(senderClient,
                JsonUtility.ToJson(new Operation(OperationType.CreateObject, playerEnum, baseNetworkObject: o)));
            var baseNetworkObject = JsonUtility.FromJson<Operation>(resp).baseNetworkObject;
            return baseNetworkObject;
        }

        /// <summary>
        /// 对方申请创建物体, 在本地创建
        /// </summary>
        /// <returns></returns>
        public static NetworkObject InstantiateNetworkObjectLocal(ObjectEnum objectEnum, int networkId,
            Transform transform = null)
        {
            if (networkObjects.ContainsKey(networkId)) return networkObjects[networkId];
            var o = Instantiate(ObjectFactory.instance.nameToObject[objectEnum], transform);
            o.networkId = networkId;
            networkObjects.Add(networkId, o);
            return o;
        }

        #endregion


        public static void CloseAll()
        {
            NetworkUtility.CloseAll(receiverClient, senderClient);
        }
    }
}