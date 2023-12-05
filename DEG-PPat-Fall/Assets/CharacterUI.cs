using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Text myText; // Reference to your Text component
    public int myValue; // Your variable whose value will determine the text

    void Start()
    {
        // Assuming you've assigned the Text component in the Inspector
        if (myText == null)
        {
            Debug.LogError("Text component not assigned!");
        }
    }

    void Update()
    {
        // Update the text based on the value of your variable
        myText.text = "Value: " + myValue.ToString();
    }
}
