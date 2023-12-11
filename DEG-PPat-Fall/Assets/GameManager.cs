using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneData{
    public int Tower;
    public int Scarp;

}

[Serializable]
public class currentData{
    public int currentTower;
    public int currentFloor;
    public string difficulty;
    public bool isCompletedForTower;
    public bool isCompletedWhileTower;
    public bool isCompletedDoWhileTower;
    public bool isCompletedFimalTower;
    public bool[,] CompletedFloor;

}

public class GameManager : MonoBehaviour
{
    private Dictionary<string, SceneData> sceneDataDict = new Dictionary<string, SceneData>();    
    public int currentTower;
    public int currentFloor;
    public string difficulty;
    public bool isCompletedForTower;
    public bool isCompletedWhileTower;
    public bool isCompletedDoWhileTower;
    public bool isCompletedFimalTower;
    public bool[,] CompletedFloor;
    private Dictionary<int, currentData> currentDataDict = new Dictionary<int, currentData>();
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }
            return _instance;
        }
    }
    void Awake(){
        if(_instance == null){
            _instance = this;
            DontDestroyOnLoad(this);
        }else{
            if(this != _instance){
                Destroy(this.gameObject);
            }
        }
    }

    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        InitializeSceneData(scene.name);
    }
    ///////////////////////////////////////

    public currentData GetCurrentData()
    {
       return new currentData
        {
            currentTower = currentTower,
            currentFloor = currentFloor
            // Set other relevant data...
        };
    }
    public void SetCurrentData(currentData data)
    {
        currentTower = data.currentTower;
        currentFloor = data.currentFloor;
        // Set other relevant data...
    }

    ///////////////////////////////////////
    /*public SceneData GetSceneData()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (sceneDataDict.ContainsKey(currentSceneName))
        {
            return sceneDataDict[currentSceneName];
        }
        else
        {
            // Scene data not found, initialize and return a new one
            InitializeSceneData(currentSceneName);
            return sceneDataDict[currentSceneName];
        }
    }

    public void SetSceneData(SceneData data)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (sceneDataDict.ContainsKey(currentSceneName))
        {
            sceneDataDict[currentSceneName] = data;
        }
        else
        {
            // Scene data not found, initialize and return a new one
            InitializeSceneData(currentSceneName);
            sceneDataDict[currentSceneName] = data;
        }
    }*/

    // Initialize data for a specific scene
    private void InitializeSceneData(string sceneName)
    {
        if (!sceneDataDict.ContainsKey(sceneName))
        {
            // Initialize new scene data
            SceneData sceneData = new SceneData();
            sceneDataDict.Add(sceneName, sceneData);
        }
    }

    

}