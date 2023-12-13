using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadableFilePanel : MonoBehaviour
{
    private ReadableFile file;
    public Button setupButton;
    public List<AnswerSlot> answerSlots = new List<AnswerSlot>();
    private List<string> answers = new List<string>();

    private void Start()
    {
        setupButton.interactable = false;
        Time.timeScale = 0f;
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

    public void SetFileReference(ReadableFile fileReference)
    {
        file = fileReference;
    }

    // This method can be called from the close button's onClick event
    public void ClosePanel()
    {
        if (file != null)
        {
            Time.timeScale = 1f;
            // Interact with the specific file
            Debug.Log("Closing panel for file: " + file.name);
            // Destroy the panel
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("file reference not set in the panel script.");
        }
    }

    public void Setfile()
    {
        if (file != null)
        {
            Time.timeScale = 1f;
            // Interact with the specific file
            Debug.Log("Setting file: " + file.name);
            GetAllAnswer();
            foreach(string answer in answers){
                Debug.Log(answer);
            }
            file.answers = answers;
            // Destroy the panel
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("file reference not set in the panel script.");
        }
    }
}
