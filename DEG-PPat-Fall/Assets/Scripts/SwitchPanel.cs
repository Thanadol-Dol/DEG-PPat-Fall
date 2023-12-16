using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchPanel : MonoBehaviour
{
    
    public List<AnswerSlot> answerSlots = new List<AnswerSlot>();
    private List<string> answers = new List<string>();
    public TextMeshProUGUI trapExtraNumberText;
    /*private void Update()
    {
        if (CheckIfAllAnswerSlotsFilled())
        {
            setupButton.interactable = true;
        }
        else
        {
            setupButton.interactable = false;
        }
    }

    private bool CheckIfAllAnswerSlotsFilled()
    {
        foreach(AnswerSlot answerSlot in answerSlots)
        {
            if (!answerSlot.isFilled)
            {
                return false;
            }
        }
        return true;
    }

    private void GetAllAnswer()
    {
        foreach(AnswerSlot answerSlot in answerSlots)
        {
            answers.Add(answerSlot.answer);
        }
    }

    public void ClosePanel()
    {
        if (trap != null)
        {
            Time.timeScale = 1f;
            // Interact with the specific trap
            Debug.Log("Closing panel for Trap: " + trap.name);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isTrapPanelOpen = false;
            // Destroy the panel
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Trap reference not set in the panel script.");
        }
    }*/
}
