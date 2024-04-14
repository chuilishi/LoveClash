// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Net.Sockets;
// using Cysharp.Threading.Tasks;
// using DG.Tweening;
// using EasyButtons;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using Script.Cards;
// using Script.Character;
// using Script.core;
// using Script.Network;
// using Script.Utility;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.AddressableAssets;
// using UnityEngine.ResourceManagement.ResourceLocations;
// using UnityEngine.ResourceManagement.ResourceProviders;
// using UnityEngine.SceneManagement;
//
// namespace Script
// {
//     public class Test : MonoBehaviour
//     {
//         public AssetReference assetReference;
//
//         private void Start()
//         {
//             Addressables.LoadSceneAsync(assetReference);
//         }
//     }
// }

using System;
using System.Collections.Generic;
using EasyButtons;
using TMPro;
using UnityEngine;

class Test : MonoBehaviour
{
    public List<int> testInts = new List<int>();

    private void Awake()
    {
    }
    
}
