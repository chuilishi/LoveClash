using System.Collections;
using System.Collections.Generic;
using Script.Network;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reconnect : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        NetworkManager.instance.Connect(NetworkManager.instance.senderClient,NetworkManager.instance.IPAddress,NetworkManager.instance.senderPort);
        NetworkManager.instance.Connect(NetworkManager.instance.receiverClient,NetworkManager.instance.IPAddress,NetworkManager.instance.receiverPort);
    }
}