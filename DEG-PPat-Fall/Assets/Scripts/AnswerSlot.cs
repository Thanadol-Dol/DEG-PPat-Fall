using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerSlot : MonoBehaviour, IDropHandler
{
    public bool isFilled;

    public Collider2D slotColider;
    public string answer;

    private void Start() {
        slotColider = this.GetComponent<Collider2D>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            RectTransform droppedObjectRectTransform = droppedObject.GetComponent<RectTransform>();
            droppedObjectRectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Choice droppedObjectChoice = droppedObject.GetComponent<Choice>();
            if(!isFilled){
                droppedObjectChoice.Respawn();
            }
            droppedObjectChoice.SetCurrentAnswerSlot(this); // Set the current AnswerSlot for the dropped Choice
            isFilled = true;
            answer = droppedObjectChoice.answer;
        }
    }

    public void RemoveChoice()
    {
        isFilled = false;
        answer = string.Empty;
    }
}
