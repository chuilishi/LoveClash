using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using Script.Manager;
using Script.Network;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public TMP_Text 房间号Text;
    public TMP_Text 用户名Text;
    public TMP_Text 开始游戏Text;
    public Button 开始游戏Button;
    public Button 单机测试Button;
    private CancellationTokenSource cts = new CancellationTokenSource();
    private void Awake()
    {
        用户名Text.text = "Whoami";
        开始游戏Button.onClick.AddListener((Init));
        单机测试Button.onClick.AddListener((() =>
        {
            单机测试Button.onClick.RemoveAllListeners();
            单机测试Button.GetComponentInChildren<TMP_Text>().text = "正在加载";
            SceneManager.LoadSceneAsync(1).completed += operation =>
            {
                GameManager.instance.Main(false);
            };
        }));
    }
    private async void Init()
    {
        Regex regex = new Regex(@"^\d{5}$");
        string s = 房间号Text.text.Substring(0, 房间号Text.text.Length - 1);
        if (!regex.IsMatch(s))
        {
            cts.Cancel();
            cts = new CancellationTokenSource();
            开始游戏Text.text = "房间号5位数字";
            try
            {
                await UniTask.Delay(1000,cancellationToken:cts.Token);
                开始游戏Text.text = "开始游戏";
            }
            catch (Exception)
            {
                开始游戏Text.text = "开始游戏";
                cts = new CancellationTokenSource();
                return;
            }
            return;
        }
        if (用户名Text.text.Length == 1)
        {
            cts.Cancel();
            cts = new CancellationTokenSource();
            开始游戏Text.text = "用户名不能为空";
            try
            {
                await UniTask.Delay(1000,cancellationToken:cts.Token);
                开始游戏Text.text = "开始游戏";
            }
            catch (Exception)
            {
                开始游戏Text.text = "开始游戏";
                cts = new CancellationTokenSource();
                return;
            }
            return;
        }
        NetworkManager.instance.roomId = int.Parse(s);
        cts.Cancel();
        cts = new CancellationTokenSource();
        开始游戏Text.text = "正在连接";
        try
        {
            await NetworkUtility.Connect(NetworkManager.instance.senderClient, NetworkManager.instance.IPAddress,
                NetworkManager.instance.senderPort);
            await NetworkUtility.Connect(NetworkManager.instance.receiverClient, NetworkManager.instance.IPAddress,
                NetworkManager.instance.receiverPort);
        }
        catch (Exception)
        {
            开始游戏Text.text = "连接失败";
            await UniTask.Delay(500,cancellationToken:cts.Token);
            开始游戏Text.text = "开始游戏";
            return;
        }
        
        开始游戏Button.onClick.RemoveAllListeners();
        开始游戏Button.GetComponentInChildren<TMP_Text>().text = "加载场景中";
        
        #region 第一次请求

        var initResp = await NetworkUtility.RequestAsync(NetworkManager.instance.senderClient,
            JsonUtility.ToJson(new Operation(OperationType.TryConnectRoom, extraMessage: NetworkManager.instance.roomId.ToString())));
        var operation = JsonUtility.FromJson<Operation>(initResp);
        if (operation.operationType == OperationType.Error)
        {
            开始游戏Text.text = "房间已满";
            开始游戏Button.onClick.AddListener((() =>
            {
                Init();
            }));
            await UniTask.Delay(500,cancellationToken:cts.Token);
            开始游戏Text.text = "开始游戏";
            return;
        }
        else
        {
            NetworkManager.instance.playerEnum = operation.playerEnum;
            SceneManager.LoadSceneAsync(1).completed += asyncOperation =>
            {
                NetworkManager.instance.Init().GetAwaiter()
                    .OnCompleted((() => { GameManager.instance.Main(true); }));
            };
        }
        #endregion;
    }

}