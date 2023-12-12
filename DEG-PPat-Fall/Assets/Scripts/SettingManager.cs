using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour
{
    public Scrollbar soundScrollbar;
    public TextMeshProUGUI soundLevelText;

    private void Start()
    {
        // Set initial sound level
        SetSoundLevel(PlayerPrefs.GetFloat("SoundLevel", 1.0f));

        // Subscribe to the scrollbar's onValueChanged event
        soundScrollbar.onValueChanged.AddListener(OnSoundLevelChanged);
    }

    private void OnSoundLevelChanged(float value)
    {
        // Update the sound level based on scrollbar value
        SetSoundLevel(value);

        // Save the sound level to PlayerPrefs for persistence
        PlayerPrefs.SetFloat("SoundLevel", value);
    }

    private void SetSoundLevel(float level)
    {
        // Adjust the game sound based on the level
        AudioListener.volume = level;

        // Update UI text to display the current sound level
        if (soundLevelText != null)
        {
            soundLevelText.text = "Sound Level: " + (int)(level * 100) + "%";
        }
    }
}
