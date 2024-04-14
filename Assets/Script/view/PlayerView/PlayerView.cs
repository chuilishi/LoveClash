using System.Collections.Generic;
using Script.Manager;
using TMPro;
using UnityEngine;

namespace Script.view
{
    public class PlayerView : MonoBehaviour
    {
        public List<ICardView> handCards = new List<ICardView>();
        public List<MinionView> minions = new List<MinionView>(7);
        public int _心动值_ = 10;
        public int _信任值_ = 10;
        public int _上头值_ = 10;
        [HideInInspector]
        public TMP_Text 心动值;
        [HideInInspector]
        public TMP_Text 信任值;
        [HideInInspector]
        public TMP_Text 上头值;
        public virtual void Awake()
        {
            心动值 = transform.Find("心动值/心动值Text").gameObject.GetComponent<TextMeshProUGUI>();
            上头值 = transform.Find("上头值/上头值Text").gameObject.GetComponent<TextMeshProUGUI>();
            信任值 = transform.Find("信任值/信任值Text").gameObject.GetComponent<TextMeshProUGUI>();
        }
    }
}