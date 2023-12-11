using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SavedData{
    public int currentTower;
    public int currentFloor;
    public string difficulty;
    public bool isCompletedForTower;
    public bool isCompletedWhileTower;
    public bool isCompletedDoWhileTower;
    public bool isCompletedFinalTower;
    public bool[,] CompletedFloor;

}

public class GameManager : MonoBehaviour
{
    public int currentTower;
    public int currentFloor;
    public string difficulty;
    public bool isCompletedForTower;
    public bool isCompletedWhileTower;
    public bool isCompletedDoWhileTower;
    public bool isCompletedFinalTower;
    public int currentLevel;
    private Dictionary<string, SavedData> savedGame = new Dictionary<string, SavedData>();
    public static GameManager Instance;
    
    void Awake(){
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    public void SaveGame()
    {
        SavedData saveData = new SavedData
        {
            currentTower = currentTower,
            currentFloor = currentFloor,
            difficulty = difficulty,
            isCompletedForTower = isCompletedForTower,
            isCompletedWhileTower = isCompletedWhileTower,
            isCompletedDoWhileTower = isCompletedDoWhileTower,
            isCompletedFinalTower = isCompletedFinalTower,

        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savegame.json", json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savegame.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavedData saveData = JsonUtility.FromJson<SavedData>(json);

            currentTower = saveData.currentTower;
            currentFloor = saveData.currentFloor;
            difficulty = saveData.difficulty;
            isCompletedForTower = saveData.isCompletedForTower;
            isCompletedWhileTower = saveData.isCompletedWhileTower;
            isCompletedDoWhileTower = saveData.isCompletedDoWhileTower;
            isCompletedFinalTower = saveData.isCompletedFinalTower;

        }
    }
    

    

}