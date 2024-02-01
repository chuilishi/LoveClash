using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.core
{
    public abstract class NetworkObject : MonoBehaviour
    {
        [HideInInspector]
        public int networkId = -1;
        public string name;
    }
}