using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.UI;

public class UISizeEditor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;

    [Button]
    public void Change()
    {
        foreach (var sprite in spriteRenderers)
        {
            sprite.gameObject.AddComponent<Image>().sprite = sprite.sprite;
            DestroyImmediate(sprite);
        }
    }
}
