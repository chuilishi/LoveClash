using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.view
{
    public class RectView : MonoBehaviour
    {
        public Rect rect;
#if UNITY_EDITOR
        [Header("左上,右下")]
        public List<Transform> twoPoints = new List<Transform>();
#endif
        private void OnDrawGizmos()
        {
            if (twoPoints.Count != 2) return;
            rect = Rect.MinMaxRect(
                Mathf.Min(twoPoints[0].position.x,twoPoints[1].position.x),
                Mathf.Min(twoPoints[0].position.y,twoPoints[1].position.y),
                Mathf.Max(twoPoints[0].position.x,twoPoints[1].position.x),
                Mathf.Max(twoPoints[0].position.y,twoPoints[1].position.y)
                );
            //draw rect by Gizmos
            Gizmos.DrawLine(new Vector3(rect.xMin,rect.yMin),new Vector3(rect.xMax,rect.yMin));
            Gizmos.DrawLine(new Vector3(rect.xMax,rect.yMin),new Vector3(rect.xMax,rect.yMax));
            Gizmos.DrawLine(new Vector3(rect.xMax,rect.yMax),new Vector3(rect.xMin,rect.yMax));
            Gizmos.DrawLine(new Vector3(rect.xMin,rect.yMax),new Vector3(rect.xMin,rect.yMin));
        }
        private void Awake()
        {
            rect = new Rect();
        }
    }
}