using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Script.core
{
    public abstract class NetworkObject : MonoBehaviour
    {
        [HideInInspector]
        public int networkId = -1;
        public ObjectEnum objectEnum;
    }

    //Deserialize时候的工具类
    public class NetworkObjectJson
    {
        public int networkId = -1;
        public ObjectEnum objectEnum;
    }
}