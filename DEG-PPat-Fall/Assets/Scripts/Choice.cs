using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Choice : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 initialPosition; // Store the initial position of the choice
    private AnswerSlot currentAnswerSlot;
    public string answer;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition; // Store the initial position
        // Find the child GameObject with TextMeshPro component
        Transform childTransform = transform.Find("AnswerText"); // Replace "ChildObjectName" with the actual name of your child GameObject
        if (childTransform != null)
        {
            // Get the TextMeshPro component from the child GameObject
            TextMeshProUGUI textMeshPro = childTransform.GetComponent<TextMeshProUGUI>();

            if (textMeshPro != null)
            {
                // Access the text property of the TextMeshPro component
                answer = textMeshPro.text;

                // Do something with the text
                Debug.Log("Text: " + answer);
            }
            else
            {
                Debug.LogWarning("Child GameObject does not have TextMeshPro component.");
            }
        }
        else
        {
            Debug.LogWarning("Child GameObject not found.");
        }
    }

    public void SetCurrentAnswerSlot(AnswerSlot answerSlot)
    {
        currentAnswerSlot = answerSlot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            canvasGroup.alpha = .6f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (canvas != null)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
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
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            // Check if the choice is outside the AnswerSlot
            CheckIfOutsideAnswerSlot();
        }
    }

    private void CheckIfOutsideAnswerSlot()
    {
        if(currentAnswerSlot == null){
            rectTransform.anchoredPosition = initialPosition;
            return;
        }

        // Assuming AnswerSlot has a collider (Collider2D or Collider) for overlap check
        Collider2D slotCollider = currentAnswerSlot.slotColider;

        if (slotCollider != null)
        {
            // Check if the choice is outside the AnswerSlot using the colliders
            if (!slotCollider.OverlapPoint(rectTransform.position))
            {
                // Reset the position of the choice to its initial position
                rectTransform.anchoredPosition = initialPosition;

                // Remove the reference to the current AnswerSlot
                if (currentAnswerSlot != null)
                {
                    currentAnswerSlot.RemoveChoice();
                    currentAnswerSlot = null;
                }
            }
        } else {
            rectTransform.anchoredPosition = initialPosition;
        }
    }
}
