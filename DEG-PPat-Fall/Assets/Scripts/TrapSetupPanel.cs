using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapSetupPanel : MonoBehaviour
{
    private Trap trap;
    public Button setupButton;
    public List<AnswerSlot> answerSlots = new List<AnswerSlot>();
    private List<string> answers = new List<string>();

    private void Start()
    {
        setupButton.interactable = false;
    }

    private void Update()
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

    public void SetTrapReference(Trap trapReference)
    {
        trap = trapReference;
    }

    // This method can be called from the close button's onClick event
    public void ClosePanel()
    {
        if (trap != null)
        {
            // Interact with the specific trap
            Debug.Log("Closing panel for Trap: " + trap.name);

            // Destroy the panel
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Trap reference not set in the panel script.");
        }
    }

    public void SetTrap()
    {
        if (trap != null)
        {
            // Interact with the specific trap
            Debug.Log("Setting Trap: " + trap.name);
            GetAllAnswer();
            trap.answers = answers;
            // Destroy the panel
            Destroy(gameObject);
            trap.isSetup = true;
        }
        else
        {
            Debug.LogError("Trap reference not set in the panel script.");
        }
    }
}
