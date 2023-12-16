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
    public static GameManager Instance;

    //Trap Setup Panel
    public List<GameObject> easyTrapSetupPanel = new List<GameObject>();
    public List<GameObject> mediumTrapSetupPanel = new List<GameObject>();
    public List<GameObject> hardTrapSetupPanel = new List<GameObject>();
    public GameObject whileTrapSetupPanel;
    public GameObject forTrapSetupPanel;
    public GameObject doWhileTrapSetupPanel;

    //Readble File Content
    public List<GameObject> easyReadableFileContent = new List<GameObject>();
    public GameObject mediumReadableFileContent;
    public GameObject hardReadableFileContent;
    public List<GameObject> whileReadableFileContent = new List<GameObject>();
    public List<GameObject> forReadableFileContent = new List<GameObject>();
    public List<GameObject> doWhileReadableFileContent = new List<GameObject>();
    public List<GameObject> allReadableFileContent = new List<GameObject>();
    
    //Readble File Puzzle Panel
    public List<GameObject> easyReadableFilePanel = new List<GameObject>();
    public GameObject mediumReadableFilePanel;
    public GameObject hardReadableFilePanel;
    public List<GameObject> whileReadableFilePanel = new List<GameObject>();
    public List<GameObject> forReadableFilePanel = new List<GameObject>();
    public List<GameObject> doWhileReadableFilePanel = new List<GameObject>();
    public List<GameObject> whileReadableFilePanelHard = new List<GameObject>();
    public List<GameObject> forReadableFilePanelHard = new List<GameObject>();
    public List<GameObject> doWhileReadableFilePanelHard = new List<GameObject>();
    void Awake(){
        allReadableFileContent.AddRange(easyReadableFileContent);
        allReadableFileContent.Add(mediumReadableFileContent);
        allReadableFileContent.Add(hardReadableFileContent);
        allReadableFileContent.AddRange(whileReadableFileContent);
        allReadableFileContent.AddRange(forReadableFileContent);
        allReadableFileContent.AddRange(doWhileReadableFileContent);
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