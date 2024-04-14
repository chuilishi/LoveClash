using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.view
{
    [RequireComponent(typeof(EventTrigger))]
    public class InputHandler : MonoBehaviour,IPointerEnterHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerExitHandler
    {
        IMDragHandler dragHandler ;
        IMBeginDragHandler beginDragHandler ;
        IMEndDragHandler endDragHandler ;
        IMPointerEnterHandler pointerEnterHandler; 
        IMPointerExitHandler pointerExitHandler ;
        
        public bool enable = true;
        public bool draggable = true;
        
        private void Awake()
        {
            dragHandler = GetComponent<IMDragHandler>();
            beginDragHandler = GetComponent<IMBeginDragHandler>();
            endDragHandler = GetComponent<IMEndDragHandler>();
            pointerEnterHandler = GetComponent<IMPointerEnterHandler>();
            pointerExitHandler = GetComponent<IMPointerExitHandler>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!enable)return;
            pointerEnterHandler?.OnPointerEnter(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(!enable)return;
            if(!draggable)return;
            beginDragHandler?.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(!enable)return;
            if(!draggable)return;
            dragHandler?.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(!enable)return;
            if(!draggable)return;
            endDragHandler?.OnEndDrag(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!enable)return;
            pointerExitHandler?.OnPointerExit(eventData);
        }
    }
    public interface IMPointerEnterHandler{public void OnPointerEnter(PointerEventData eventData){}}
    public interface IMBeginDragHandler{public void OnBeginDrag(PointerEventData eventData){}}
    public interface IMDragHandler{public void OnDrag(PointerEventData eventData){}}
    public interface IMEndDragHandler{public void OnEndDrag(PointerEventData eventData){}}
    public interface IMPointerExitHandler{public void OnPointerExit(PointerEventData eventData){}}
}