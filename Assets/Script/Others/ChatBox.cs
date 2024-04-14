using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//对话框按钮功能的实现
public class ChatBox : MonoBehaviour
{
    [SerializeField]
    public GameObject ChatList;
    public GameObject MessageList;
    public GameObject StrickerList;

    public void ShowORHideChatBox()
    {
        ChatList.SetActive(!ChatList.activeSelf);
    }

    public void ShowMessageList()
    {
        StrickerList.SetActive(false);
        MessageList.SetActive(true);
    }

    public void ShowStrickerList()
    {
        MessageList.SetActive(false);
        StrickerList.SetActive(true);
    }
}
