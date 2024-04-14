using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.Network;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reconnect : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
         NetworkManager.CloseAll();
         UniTask.Delay(100).GetAwaiter().OnCompleted((Application.Quit));
    }
}