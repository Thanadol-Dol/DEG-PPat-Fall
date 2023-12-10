using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerSlot : MonoBehaviour, IDropHandler{
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null) {
            droppedObject.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
