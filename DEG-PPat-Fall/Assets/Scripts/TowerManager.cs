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

    private bool StairUp1 = false;
    private bool StairUp2 = false;
    //private bool StairDown1 = false;
    //private bool StairDown2 = false;
    private List<bool> floorSwitch = new List<bool>();
    public List<GameObject> trapSetupPanelPrefabs = new List<GameObject>();
    public List<string> topicList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //Trap Setup Panel
        if (GameManager.Instance.currentLevel >= 1)
        {
            foreach (GameObject trapSetupPanel in GameManager.Instance.easyTrapSetupPanel)
            {
                trapSetupPanelPrefabs.Add(trapSetupPanel);
            }
        }
        else if (GameManager.Instance.currentLevel >= 2)
        {
            foreach (GameObject trapSetupPanel in GameManager.Instance.mediumTrapSetupPanel)
            {
                trapSetupPanelPrefabs.Add(trapSetupPanel);
            }
        }
        else if (GameManager.Instance.currentLevel >= 3)
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
        
        //Readable File
        if (GameManager.Instance.currentLevel >= 1)
        {
            foreach (GameObject readableFileContent in GameManager.Instance.easyReadableFileContent)
            {
                topicList.Add(readableFileContent.name);
            }
        }
        if (GameManager.Instance.currentLevel >= 2)
        {

            topicList.Add(GameManager.Instance.mediumReadableFileContent.name);
        }
        if (GameManager.Instance.currentLevel >= 3)
        {

            topicList.Add(GameManager.Instance.hardReadableFileContent.name);
        }

        Floors[0].SetActive(true);
        for (int i = 1; i < Floors.Length; i++)
        {
            Floors[i].SetActive(false);
        }
        FindSpawnPoint();

        for (int i = 0; i < Floors.Length; i++)
        {
            floorSwitch.Add(false);
        }
        floorSwitch[0] = true;
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
    }

    public void goPreviousFloor()
    {
        DestroyPlayerAndEnemy();
        CheckCompletedTower();
        Debug.Log("Switching to previous floor");
        previousFloor();
        FindSpawnPoint();
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
        }
        else
        {
            Debug.Log("No more floors!");
        }
    }

    private void previousFloor()
    {
        /*if (GameManager.Instance.currentFloor > 0)
        {
            Debug.Log("Current Floor : " + GameManager.Instance.currentFloor);
            Floors[GameManager.Instance.currentFloor].SetActive(false);
            Floors[GameManager.Instance.currentFloor].GetComponent<Floor>().ClearTilemaps();
            GameManager.Instance.currentFloor--;
            Debug.Log("Current Floor : " + GameManager.Instance.currentFloor);
            Floors[GameManager.Instance.currentFloor].SetActive(true);
            Floors[GameManager.Instance.currentFloor].GetComponent<Floor>().SetupTilemaps();
            
        }
        else
        {
            Debug.Log("No more floors!");
        }*/
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
                //StairDown1 = false;
                //StairDown2 = false;
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
                    /*}
                    else if(StairDown1 == true){
                        if(Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawnD1") != null)
                        {
                            Transform playerSpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawnD1");
                            playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
                        }
                        StairDown1 = false;
                    }
                    else if (StairDown2 == true)
                    {
                        if (Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawnD2") != null)
                        {
                            Transform playerSpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawnD2");
                            playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
                        }
                        StairDown2 = false;*/
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

    /*public void StairDown(int StairNumber)
    {
        if (GameManager.Instance.currentFloor > 0)
        {
            if (StairNumber == 1)
            {
                StairDown1 = true;
            }
            else if (StairNumber == 2)
            {
                StairDown2 = true;
            }
            goPreviousFloor();
        }
        
    }*/

    public void CheckStairClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool isClickable = true;
            isClickable = CheckStair(Floors[GameManager.Instance.currentFloor].transform.Find("StairUpPoint1"), "Up", 1, mousePosition, isClickable);
            isClickable = CheckStair(Floors[GameManager.Instance.currentFloor].transform.Find("StairUpPoint2"), "Up", 2, mousePosition, isClickable);

            //isClickable = CheckStair(Floors[GameManager.Instance.currentFloor].transform.Find("StairDownPoint1"), "Down", 1, mousePosition, isClickable);
            //isClickable = CheckStair(Floors[GameManager.Instance.currentFloor].transform.Find("StairDownPoint2"), "Down", 2, mousePosition, isClickable);


        }
    }

    public bool CheckStair(Transform stairTransform, string StairDirection, int stairNumber, Vector3 mousePosition, bool isClickable)
    {
        if (isClickable == false)
        {
            return false;
        }

        if (stairTransform != null)
        {

            Vector3 stairPosition = stairTransform.position;
            /*float disPlayer_Mouse = Vector3.Distance(playerInstance.transform.position, mousePosition);
            if(disPlayer_Mouse >= 2.0f){
                return true;
            }*/
            Debug.Log("Click!" + StairDirection);
            float disPlayer_Stair = Vector3.Distance(playerInstance.transform.position, stairPosition);

            if (disPlayer_Stair <= 2.0f)
            {


                if (StairDirection == "Up")
                {
                    Debug.Log("StairUp!");
                    StairUp(stairNumber);
                }
                /*else if (StairDirection == "Down")
                {
                    Debug.Log("StairDown!");
                    StairDown(stairNumber);
                }*/
            }
            return false;
        }
        return true;
    }

    public void SwitchCompleted()
    {
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
                //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 switchPosition = Floors[GameManager.Instance.currentFloor].transform.Find("Switch").position;

                //float disPlayer_Mouse = Vector3.Distance(playerInstance.transform.position, mousePosition);
                //if(disPlayer_Mouse <= 1.0f){
                float disPlayer_Switch = Vector3.Distance(playerInstance.transform.position, switchPosition);

                if (disPlayer_Switch <= 1.3f)
                {
                    SwitchCompleted();
                    Debug.Log("Switch Completed!");
                }

                //}
            }
        }
    }

    public void CheckCompletedTower()
    {
        if (GameManager.Instance.currentFloor == Floors.Length - 1)
        {
            if (GameManager.Instance.currentTower == "For")
            {
                GameManager.Instance.isCompletedForTower = true;
            }
            else if (GameManager.Instance.currentTower == "While")
            {
                GameManager.Instance.isCompletedWhileTower = true;
            }
            else if (GameManager.Instance.currentTower == "DoWhile")
            {
                GameManager.Instance.isCompletedDoWhileTower = true;
            }
            else if (GameManager.Instance.currentTower == "Master")
            {
                GameManager.Instance.isCompletedMasterTower = true;
            }
            GameManager.Instance.currentLevel++;
            if (GameManager.Instance.isCompletedMasterTower)
            {
                SceneManager.LoadScene("Outro");
            }
            else
            {
                SceneManager.LoadScene("SelectStage");
            }
        }
    }

    public void CompleteTower()
    {
        if (GameManager.Instance.currentTower == "For")
        {
            GameManager.Instance.isCompletedForTower = true;
        }
        else if (GameManager.Instance.currentTower == "While")
        {
            GameManager.Instance.isCompletedWhileTower = true;
        }
        else if (GameManager.Instance.currentTower == "DoWhile")
        {
            GameManager.Instance.isCompletedDoWhileTower = true;
        }
        else if (GameManager.Instance.currentTower == "Master")
        {
            GameManager.Instance.isCompletedMasterTower = true;
        }
        GameManager.Instance.currentLevel++;
        if (GameManager.Instance.isCompletedMasterTower)
        {
            SceneManager.LoadScene("Outro");
        }
        else
        {
            SceneManager.LoadScene("SelectStage");
        }

    }
    public void AddTopic(string topic)
    {
        topicList.Add(topic);
    }
}
