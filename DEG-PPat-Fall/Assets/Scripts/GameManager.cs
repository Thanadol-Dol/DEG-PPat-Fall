using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string currentTower;
    public int currentFloor;
    public string difficulty;
    public bool isCompletedForTower;
    public bool isCompletedWhileTower;
    public bool isCompletedDoWhileTower;
    public bool isCompletedMasterTower;
    public int currentLevel;
    public bool isCompletedIntro;
    public List<string> SelectedTower = new List<string>();
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

}