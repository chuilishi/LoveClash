using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISizeEditor : MonoBehaviour
{
    public List<RectTransform> spriteRenderers;
    public List<TextMeshPro> texts;
    [Button]
    public void Change()
    {
        foreach (var sprite in spriteRenderers)
        {
            sprite.sizeDelta *= 100;
        }
    }

    [Button]
    public void ChangeText()
    {
        foreach (var text in texts)
        {
            var obj = text.gameObject;
            var t = text.text;
            DestroyImmediate(text);
            DestroyImmediate(obj.gameObject.GetComponent<MeshRenderer>());
            obj.gameObject.AddComponent<TextMeshProUGUI>().text = t;
        }
    }
}
