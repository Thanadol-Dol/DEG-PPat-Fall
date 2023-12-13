using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public class SavedData{
    public string difficulty;
    public string currentTower;
    public int currentFloor;
    public int currentLevel;
    public bool isCompletedForTower;
    public bool isCompletedWhileTower;
    public bool isCompletedDoWhileTower;
    public bool isCompletedMasterTower;
    public bool isCompletedIntro;

}
public class SaveLoadManager : MonoBehaviour
{
    private Dictionary<string, SavedData> savedGame = new Dictionary<string, SavedData>();
    public void SaveGame()
    {
        SavedData saveData = new SavedData
        {
            difficulty = GameManager.Instance.difficulty,
            currentTower = GameManager.Instance.currentTower,
            currentFloor = GameManager.Instance.currentFloor,
            currentLevel = GameManager.Instance.currentLevel,
            isCompletedForTower = GameManager.Instance.isCompletedForTower,
            isCompletedWhileTower = GameManager.Instance.isCompletedWhileTower,
            isCompletedDoWhileTower = GameManager.Instance.isCompletedDoWhileTower,
            isCompletedMasterTower = GameManager.Instance.isCompletedMasterTower,
            isCompletedIntro = GameManager.Instance.isCompletedIntro

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

            GameManager.Instance.difficulty = saveData.difficulty;
            GameManager.Instance.currentTower = saveData.currentTower;
            GameManager.Instance.currentFloor = saveData.currentFloor;
            GameManager.Instance.currentLevel = saveData.currentLevel;
            GameManager.Instance.isCompletedForTower = saveData.isCompletedForTower;
            GameManager.Instance.isCompletedWhileTower = saveData.isCompletedWhileTower;
            GameManager.Instance.isCompletedDoWhileTower = saveData.isCompletedDoWhileTower;
            GameManager.Instance.isCompletedMasterTower = saveData.isCompletedMasterTower;
            GameManager.Instance.isCompletedIntro = saveData.isCompletedIntro;
           
        }   
    }
    
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

}
