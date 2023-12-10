using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Choice : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 initialPosition; // Store the initial position of the choice
    private GameObject answerSlot;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition; // Store the initial position
        answerSlot = GameObject.Find("AnswerSlot");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("OnBeginDrag");
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("OnDrag");
            if (canvas != null)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
                Debug.Log("Canvas Exists");
            }
            else
            {
                rectTransform.anchoredPosition += eventData.delta;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("OnEndDrag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            // Check if the choice is outside the AnswerSlot
            CheckIfOutsideAnswerSlot();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    private void CheckIfOutsideAnswerSlot()
    {
        // Assuming AnswerSlot has a collider (Collider2D or Collider) for overlap check
        Collider2D slotCollider = answerSlot.GetComponent<Collider2D>();

        if (slotCollider != null)
        {
            // Check if the choice is outside the AnswerSlot using the colliders
            if (!slotCollider.OverlapPoint(rectTransform.position))
            {
                Debug.Log("Choice dragged outside of AnswerSlot");

                // Reset the position of the choice to its initial position
                rectTransform.anchoredPosition = initialPosition;
            }
        }
    }
}
