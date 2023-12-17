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
    public string currentTargetTopic;
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
    public List<GameObject> allReadableFilePanel = new List<GameObject>();
    public Dictionary<string, string> topicConverter = new Dictionary<string, string>();
    
    void Awake(){
        allReadableFileContent.AddRange(easyReadableFileContent);
        allReadableFileContent.Add(mediumReadableFileContent);
        allReadableFileContent.Add(hardReadableFileContent);
        allReadableFileContent.AddRange(whileReadableFileContent);
        allReadableFileContent.AddRange(forReadableFileContent);
        allReadableFileContent.AddRange(doWhileReadableFileContent);

        allReadableFilePanel.AddRange(easyReadableFilePanel);
        allReadableFilePanel.Add(mediumReadableFilePanel);
        allReadableFilePanel.Add(hardReadableFilePanel);
        allReadableFilePanel.AddRange(whileReadableFilePanel);
        allReadableFilePanel.AddRange(forReadableFilePanel);
        allReadableFilePanel.AddRange(doWhileReadableFilePanel);
        allReadableFilePanel.AddRange(whileReadableFilePanelHard);
        allReadableFilePanel.AddRange(forReadableFilePanelHard);
        allReadableFilePanel.AddRange(doWhileReadableFilePanelHard);
        ListAllTopic();
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

    public void ListAllTopic()
    {
        topicConverter.Add("While1","Tower Mechanism");
        topicConverter.Add("While2","Puzzle Mechanism");
        topicConverter.Add("While3","Information#1");
        topicConverter.Add("While4","Information#2");
        topicConverter.Add("DoWhile1","Tower Mechanism");
        topicConverter.Add("DoWhile2","Puzzle Mechanism");
        topicConverter.Add("DoWhile3","Information#1");
        topicConverter.Add("DoWhile4","Information#2");
        topicConverter.Add("For1","Tower Mechanism");
        topicConverter.Add("For2","Puzzle Mechanism");
        topicConverter.Add("For3","Information#1");
        topicConverter.Add("For4","Information#2");
        topicConverter.Add("Easy0","Basic Control");
        topicConverter.Add("Easy1","Basic Condition");
        topicConverter.Add("Easy2","Trap Crarfting");
        topicConverter.Add("Easy3","Trap Placing");
        topicConverter.Add("Easy4","Trap Setting");
        topicConverter.Add("Easy5","Switch Mechanism");
        topicConverter.Add("Medium1","Nested Condition");
        topicConverter.Add("Hard1","Complex Condition");
    }
}