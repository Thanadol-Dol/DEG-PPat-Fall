using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[SerializeField]
public class SavedData{

}

public class saveManager : MonoBehaviour
{
    GameManager GameManager;
    public void SaveGame()
    {
        // Save global data
        currentData currenrtData = GameManager.Instance.GetCurrentData();

        // Save local scene data for the current scene
        
        // Save other relevant data...
        
        PlayerPrefs.Save(); // Persist the data
    }

    public void LoadGame()
    {
        // Load global data
        currentData CurrentData = new currentData
        {
            
        };
        GameManager.Instance.SetCurrentData(CurrentData);

        // Load local scene data for the current scene
        // Load other relevant data...
    }
    
}
