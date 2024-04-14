using Cysharp.Threading.Tasks;
using Script.core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.view
{
    public abstract class ICardView : NetworkObject,IPointerEnterHandler,IDragHandler,IEndDragHandler,IPointerExitHandler,IBeginDragHandler
    {
        public abstract UniTask ResetPosition(Vector3? position = null);
        public abstract void OnPointerEnter(PointerEventData eventData);

        public abstract void OnDrag(PointerEventData eventData);

        public abstract void OnEndDrag(PointerEventData eventData);

        public abstract void OnPointerExit(PointerEventData eventData);

        public abstract void OnBeginDrag(PointerEventData eventData);
    }
}