using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class TowerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject[] Floors;

    private GameObject playerInstance;
    private GameObject enemyInstance;
    private GameObject enemyInstance2;
    private GameObject enemyInstance3;
    private float pickupRange;
    private bool isCompletedPrologue = false;
    private bool isCompletedFloor3rd = false;

    private bool StairUp1 = false;
    private bool StairUp2 = false;
    private List<bool> floorSwitch = new List<bool>();
    public List<GameObject> trapSetupPanelPrefabs = new List<GameObject>();
    public List<string> topicList = new List<string>();
    public Dictionary<string, int> filePuzzleConverter = new Dictionary<string, int>();
    public Dictionary<string, string> fileNameToContent = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.currentTargetTopic = "";
        FileNameToContent();
        ListAllFilePuzzle();
        
        //Trap Setup Panel
        if (GameManager.Instance.currentLevel >= 1)
        {
            foreach (GameObject trapSetupPanel in GameManager.Instance.easyTrapSetupPanel)
            {
                trapSetupPanelPrefabs.Add(trapSetupPanel);
            }
        }
        if (GameManager.Instance.currentLevel >= 2)
        {
            foreach (GameObject trapSetupPanel in GameManager.Instance.mediumTrapSetupPanel)
            {
                trapSetupPanelPrefabs.Add(trapSetupPanel);
            }
        }
        if (GameManager.Instance.currentLevel >= 3)
        {
            foreach (GameObject trapSetupPanel in GameManager.Instance.hardTrapSetupPanel)
            {
                trapSetupPanelPrefabs.Add(trapSetupPanel);
            }
        }
        if(GameManager.Instance.currentTower == "While")
        {
            trapSetupPanelPrefabs.Add(GameManager.Instance.whileTrapSetupPanel);
        }
        else if (GameManager.Instance.currentTower == "For")
        {
            trapSetupPanelPrefabs.Add(GameManager.Instance.forTrapSetupPanel);
        }
        else if (GameManager.Instance.currentTower == "DoWhile")
        {
            trapSetupPanelPrefabs.Add(GameManager.Instance.doWhileTrapSetupPanel);
        }

        

        Floors[0].SetActive(true);
        for (int i = 1; i < Floors.Length; i++)
        {
            Floors[i].SetActive(false);
        }
        SetActiveValue(false);
        FindSpawnPoint();

        for (int i = 0; i < Floors.Length; i++)
        {
            floorSwitch.Add(false);
        }
        pickupRange = 2.0f;
        TowerStartDialog();
        if(GameManager.Instance.currentLevel == 1){
            OpenDialog("Easy0");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            goNextFloor();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            CompleteTower();
        }
        //checkLevel1Dialog();
        CheckStairClick();
        checkSwitchClick();
    }

    public void goNextFloor()
    {
        DestroyPlayerAndEnemy();
        CheckCompletedTower();
        Debug.Log("Switching to next floor");
        NextFloor();
        FindSpawnPoint();
        checkDialogFloor4th();
    }

    private void SetActiveValue(bool value){
        GameObject currentFloor = Floors[GameManager.Instance.currentFloor];
        Transform SU1 = currentFloor.transform.Find("StairUpActive1");
        Transform SU2 = currentFloor.transform.Find("StairUpActive2");
        Transform SA = currentFloor.transform.Find("SwitchActiveTilemap");
        if(SU1 != null){
            SU1.gameObject.SetActive(value);
        }
        if(SU2 != null){
            SU2.gameObject.SetActive(value);
        }
        if(SA != null){
            SA.gameObject.SetActive(value);
        }
    }

    private void NextFloor()
    {
        if (GameManager.Instance.currentFloor < Floors.Length - 1)
        {
            Debug.Log("Current Floor : " + GameManager.Instance.currentFloor);
            Floors[GameManager.Instance.currentFloor].GetComponent<Floor>().ClearTilemaps();
            Floors[GameManager.Instance.currentFloor].SetActive(false);
            GameManager.Instance.currentFloor++;
            Debug.Log("Current Floor : " + GameManager.Instance.currentFloor);
            Floors[GameManager.Instance.currentFloor].GetComponent<Floor>().SetupTilemaps();
            Floors[GameManager.Instance.currentFloor].SetActive(true);
            SetActiveValue(false);
        }
        else
        {
            Debug.Log("No more floors!");
        }
    }

    public void FindSpawnPoint()
    {
        if (GameManager.Instance.currentFloor >= 0 && GameManager.Instance.currentFloor < Floors.Length)
        {
            if (GameManager.Instance.currentFloor == 0)
            {
                if (Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawn1") != null)
                {
                    Transform playerSpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawn1");
                    playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
                }
                StairUp1 = false;
                StairUp2 = false;
            }
            else
            {

                if (StairUp1 == true)
                {
                    if (Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawn1") != null)
                    {
                        Transform playerSpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawn1");
                        playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
                    }
                    StairUp1 = false;
                }
                else if (StairUp2 == true)
                {
                    if (Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawn2") != null)
                    {
                        Transform playerSpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawn2");
                        playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
                    }
                    StairUp2 = false;
                }
                else
                {
                    if (Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawn1") != null)
                    {
                        Transform playerSpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawn1");
                        playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
                    }
                }
            }

            if (Floors[GameManager.Instance.currentFloor].transform.Find("EnemySpawn") != null)
            {
                Transform enemySpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("EnemySpawn");
                enemyInstance = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
            }

            if (Floors[GameManager.Instance.currentFloor].transform.Find("EnemySpawn2") != null)
            {
                Transform enemySpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("EnemySpawn2");
                enemyInstance2 = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
            }

            if (Floors[GameManager.Instance.currentFloor].transform.Find("EnemySpawn3") != null)
            {
                Transform enemySpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("EnemySpawn3");
                enemyInstance3 = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
            }
        }
    }

    private void DestroyPlayerAndEnemy()
    {
        if (GameManager.Instance.currentFloor >= 0 && GameManager.Instance.currentFloor < Floors.Length)
        {
            if (playerInstance != null)
            {
                Destroy(playerInstance);
            }
            if (enemyInstance != null)
            {
                Destroy(enemyInstance);
            }
            if (enemyInstance2 != null)
            {
                Destroy(enemyInstance2);
            }
            if (enemyInstance3 != null)
            {
                Destroy(enemyInstance3);
            }
        }
    }

    public void StairUp(int StairNumber)
    {
        if (floorSwitch[GameManager.Instance.currentFloor] == true)
        {
            if (StairNumber == 1)
            {
                StairUp1 = true;
            }
            else if (StairNumber == 2)
            {
                StairUp2 = true;
            }
            goNextFloor();
        }
        else
        {
            Debug.Log("Switch is not completed!");
        }

    }


    public void CheckStairClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool isClickable = true;
            isClickable = CheckStair(Floors[GameManager.Instance.currentFloor].transform.Find("StairUpPoint1"), "Up", 1, isClickable);
            isClickable = CheckStair(Floors[GameManager.Instance.currentFloor].transform.Find("StairUpPoint2"), "Up", 2, isClickable);

        }
    }

    public bool CheckStair(Transform stairTransform, string StairDirection, int stairNumber, bool isClickable)
    {
        if (isClickable == false)
        {
            return false;
        }

        if (stairTransform != null)
        {

            Vector3 stairPosition = stairTransform.position;
            Debug.Log("Click!" + StairDirection);
            float disPlayer_Stair = Vector3.Distance(playerInstance.transform.position, stairPosition);

            if (disPlayer_Stair <= pickupRange)
            {
                if (StairDirection == "Up")
                {
                    Debug.Log("StairUp!");
                    StairUp(stairNumber);
                }
            }
        }
        return true;
    }

    public void SwitchCompleted()
    {
        ChangeFloorSwitch();
        ChangeStairUp();
        SetActiveValue(true);
        floorSwitch[GameManager.Instance.currentFloor] = true;
        if (floorSwitch[GameManager.Instance.currentFloor] && GameManager.Instance.currentFloor == Floors.Length - 1)
        {
            CompleteTower();
        }
    }

    public void checkSwitchClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Floors[GameManager.Instance.currentFloor].transform.Find("Switch") != null)
            {
               
                Vector3 switchPosition = Floors[GameManager.Instance.currentFloor].transform.Find("Switch").position;
                float disPlayer_Switch = Vector3.Distance(playerInstance.transform.position, switchPosition);

                if (disPlayer_Switch <= pickupRange)
                {
                    SwitchCompleted();
                    Debug.Log("Switch Completed!");
                }
            }
        }
    }


    public void ChangeFloorSwitch()
    {
        Floors[GameManager.Instance.currentFloor].GetComponent<Floor>().ReplaceSwitch();
    }

    public void ChangeStairUp(){
        Floors[GameManager.Instance.currentFloor].GetComponent<Floor>().ReplaceStairUp();
    }

    public void CheckCompletedTower()
    {
        if (GameManager.Instance.currentFloor == Floors.Length - 1)
        {
            if (GameManager.Instance.currentTower == "For")
            {
                GameManager.Instance.isCompletedForTower = true;
                GameManager.Instance.currentLevel++;
                SceneManager.LoadScene("ForEndScene");
            }
            else if (GameManager.Instance.currentTower == "While")
            {
                GameManager.Instance.isCompletedWhileTower = true;
                GameManager.Instance.currentLevel++;
                SceneManager.LoadScene("WhileEndScene");
            }
            else if (GameManager.Instance.currentTower == "DoWhile")
            {
                GameManager.Instance.isCompletedDoWhileTower = true;
                GameManager.Instance.currentLevel++;
                SceneManager.LoadScene("DoWhileEndScene");
            }
            else if (GameManager.Instance.currentTower == "Master")
            {
                GameManager.Instance.isCompletedMasterTower = true;
                GameManager.Instance.currentLevel++;
                SceneManager.LoadScene("Outro");
            }
        }
    }

    public void CompleteTower()
    {
        if (GameManager.Instance.currentTower == "For")
        {
            GameManager.Instance.isCompletedForTower = true;
            GameManager.Instance.currentLevel++;
            SceneManager.LoadScene("ForEndScene");
        }
        else if (GameManager.Instance.currentTower == "While")
        {
            GameManager.Instance.isCompletedWhileTower = true;
            GameManager.Instance.currentLevel++;
            SceneManager.LoadScene("WhileEndScene");
        }
        else if (GameManager.Instance.currentTower == "DoWhile")
        {
            GameManager.Instance.isCompletedDoWhileTower = true;
            GameManager.Instance.currentLevel++;
            SceneManager.LoadScene("DoWhileEndScene");
        }
        else if (GameManager.Instance.currentTower == "Master")
        {
            GameManager.Instance.isCompletedMasterTower = true;
            GameManager.Instance.currentLevel++;
            SceneManager.LoadScene("Outro");
        }

    }
    public void AddTopic(string topic)
    {
        topicList.Add(topic);
        OpenDialog(topic);
    }

    public void ListAllFilePuzzle()
    {
        filePuzzleConverter.Add("Easy1",1);
        filePuzzleConverter.Add("Easy2",2);
        filePuzzleConverter.Add("Easy3",3);
        filePuzzleConverter.Add("Easy4",4);
        filePuzzleConverter.Add("Easy5",5);
        filePuzzleConverter.Add("Medium1",6);
        filePuzzleConverter.Add("Hard1",7);
        if(GameManager.Instance.currentLevel <= 2)
        {
            filePuzzleConverter.Add("For1",8);
            filePuzzleConverter.Add("For2",9);
            filePuzzleConverter.Add("For3",10);
            filePuzzleConverter.Add("For4",11);
            filePuzzleConverter.Add("While1",16);
            filePuzzleConverter.Add("While2",17);
            filePuzzleConverter.Add("While3",18);
            filePuzzleConverter.Add("While4",19);
            filePuzzleConverter.Add("DoWhile1",24);
            filePuzzleConverter.Add("DoWhile2",25);
            filePuzzleConverter.Add("DoWhile3",26);
            filePuzzleConverter.Add("DoWhile4",27);   
        } else {
            filePuzzleConverter.Add("For1",12);
            filePuzzleConverter.Add("For2",13);
            filePuzzleConverter.Add("For3",14);
            filePuzzleConverter.Add("For4",15);
            filePuzzleConverter.Add("While1",20);
            filePuzzleConverter.Add("While2",21);
            filePuzzleConverter.Add("While3",22);
            filePuzzleConverter.Add("While4",23);
            filePuzzleConverter.Add("DoWhile1",28);
            filePuzzleConverter.Add("DoWhile2",29);
            filePuzzleConverter.Add("DoWhile3",30);
            filePuzzleConverter.Add("DoWhile4",31);   
        }
    }
    public void FileNameToContent()
    {
        fileNameToContent.Add("Easy1","Easy1");
        fileNameToContent.Add("Easy2","Easy2");
        fileNameToContent.Add("Easy3","Easy3");
        fileNameToContent.Add("Easy4","Easy4");
        fileNameToContent.Add("Easy5","Easy5");
        fileNameToContent.Add("Medium1","Medium1");
        fileNameToContent.Add("Hard1","Hard1");
        if(GameManager.Instance.currentLevel <= 2)
        {
            fileNameToContent.Add("For1","ForNormal1");
            fileNameToContent.Add("For2","ForNormal2");
            fileNameToContent.Add("For3","ForNormal3");
            fileNameToContent.Add("For4","ForNormal4");
            fileNameToContent.Add("While1","WhileNormal1");
            fileNameToContent.Add("While2","WhileNormal2");
            fileNameToContent.Add("While3","WhileNormal3");
            fileNameToContent.Add("While4","WhileNormal4");
            fileNameToContent.Add("DoWhile1","DoWhileNormal1");
            fileNameToContent.Add("DoWhile2","DoWhileNormal2");
            fileNameToContent.Add("DoWhile3","DoWhileNormal3");
            fileNameToContent.Add("DoWhile4","DoWhileNormal4");
        } else {
            fileNameToContent.Add("For1","ForHard1");
            fileNameToContent.Add("For2","ForHard2");
            fileNameToContent.Add("For3","ForHard3");
            fileNameToContent.Add("For4","ForHard4");
            fileNameToContent.Add("While1","WhileHard1");
            fileNameToContent.Add("While2","WhileHard2");
            fileNameToContent.Add("While3","WhileHard3");
            fileNameToContent.Add("While4","WhileHard4");
            fileNameToContent.Add("DoWhile1","DoWhileHard1");
            fileNameToContent.Add("DoWhile2","DoWhileHard2");
            fileNameToContent.Add("DoWhile3","DoWhileHard3");
            fileNameToContent.Add("DoWhile4","DoWhileHard4");
        }
    }

    private void OpenDialog(string topic){
        GameManager.Instance.currentTargetTopic = topic;
    }

    private void TowerStartDialog(){
        /*if(GameManager.Instance.currentLevel == 1){
            topicList.Add(GameManager.Instance.easyReadableFileContent[0].name);
        }else if(GameManager.Instance.currentLevel >= 2){
            foreach (GameObject easyReadableFile in GameManager.Instance.easyReadableFileContent){
                topicList.Add(easyReadableFile.name);
            }
            topicList.Add(GameManager.Instance.mediumReadableFileContent.name);
            if (GameManager.Instance.currentLevel >= 3){
                topicList.Add(GameManager.Instance.hardReadableFileContent.name);
            }
        }
        isCompletedPrologue = true;*/
        if(GameManager.Instance.currentLevel >= 1){
            foreach (GameObject easyReadableFile in GameManager.Instance.easyReadableFileContent){
                topicList.Add(easyReadableFile.name);
            }
        }
        if(GameManager.Instance.currentLevel >= 2)
        {
            topicList.Add(GameManager.Instance.mediumReadableFileContent.name);
        }
        if (GameManager.Instance.currentLevel >= 3){
            topicList.Add(GameManager.Instance.hardReadableFileContent.name);
        }
    }

    /*private void checkLevel1Dialog(){
        if(GameManager.Instance.currentLevel == 1 && isCompletedPrologue){
            for(int i = 1; i < GameManager.Instance.easyReadableFileContent.Count - 1; i++){
                topicList.Add(GameManager.Instance.easyReadableFileContent[i].name);

            }
            OpenDialog("Easy1");
            isCompletedPrologue = false;
        }
        
    }*/

    private void checkDialogFloor4th(){
        if(GameManager.Instance.currentFloor == 4 && !isCompletedFloor3rd){
            if(GameManager.Instance.currentTower == "For"){
                if(!topicList.Contains(GameManager.Instance.forReadableFileContent[1].name)){
                    topicList.Add(GameManager.Instance.forReadableFileContent[1].name);
                }
                OpenDialog("For2");
            }
            else if(GameManager.Instance.currentTower == "While"){
                if(!topicList.Contains(GameManager.Instance.whileReadableFileContent[1].name)){
                    topicList.Add(GameManager.Instance.whileReadableFileContent[1].name);
                }
                OpenDialog("While2");
            }
            else if(GameManager.Instance.currentTower == "DoWhile"){
                if(!topicList.Contains(GameManager.Instance.doWhileReadableFileContent[1].name)){
                    topicList.Add(GameManager.Instance.doWhileReadableFileContent[1].name);
                }
                OpenDialog("DoWhile2");
            }
            isCompletedFloor3rd = true;
        }
    }
}
