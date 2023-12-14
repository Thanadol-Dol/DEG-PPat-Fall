using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrapSetupPanel : MonoBehaviour
{
    private Trap trap;
    public Button setupButton;
    public List<AnswerSlot> answerSlots = new List<AnswerSlot>();
    private List<string> answers = new List<string>();
    public TextMeshProUGUI trapExtraNumberText;

    private void Start()
    {
        setupButton.interactable = false;
        Time.timeScale = 0f;
        if(trap.extraNumber != null){
            trapExtraNumberText.text = "int y = " + trap.extraNumber.ToString();
        }
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
    }

    public void SetTrap()
    {
        if (trap != null)
        {
            Time.timeScale = 1f;
            // Interact with the specific trap
            Debug.Log("Setting Trap: " + trap.name);
            GetAllAnswer();
            foreach(string answer in answers){
                Debug.Log(answer);
            }
            trap.answers = answers;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isTrapPanelOpen = false;
            // Destroy the panel
            Destroy(gameObject);
            trap.isSetup = true;
            trap.DestroyAfterDelay();
        }
        else
        {
            Debug.LogError("Trap reference not set in the panel script.");
        }
    }
}
