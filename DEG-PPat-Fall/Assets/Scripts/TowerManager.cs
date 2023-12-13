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
    
    // Start is called before the first frame update
    void Start()
    {
        Floors[0].SetActive(true);
        for (int i = 1; i < Floors.Length; i++)
        {
            Floors[i].SetActive(false);
        }
        FindSpawnPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DestroyPlayerAndEnemy();
            CheckCompletedTower();
            Debug.Log("Switching to next floor");
            SwitchToNextFloor();
            FindSpawnPoint();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            CompleteTower();
        }
    }

    void SwitchToNextFloor()
    {
        if (GameManager.Instance.currentFloor < Floors.Length - 1)
        {
            Floors[GameManager.Instance.currentFloor].GetComponent<Floor>().ClearTilemaps();
            Floors[GameManager.Instance.currentFloor].SetActive(false);
            GameManager.Instance.currentFloor++;
            Floors[GameManager.Instance.currentFloor].SetActive(true);
            SetupCurrentFloor();
        }
        else{
            Debug.Log("No more floors!");
        }
    }

    void SetupCurrentFloor()
    {
        Floors[GameManager.Instance.currentFloor].GetComponent<Floor>().SetupTilemaps();
    }

    public void FindSpawnPoint()
    {
        if (GameManager.Instance.currentFloor < Floors.Length)
        {
            Transform playerSpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("PlayerSpawn");
            if (playerSpawnPoint != null)
            {
                playerInstance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
            }

            Transform enemySpawnPoint = Floors[GameManager.Instance.currentFloor].transform.Find("EnemySpawn");
            if (enemySpawnPoint != null)
            {
                enemyInstance = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
            }
        }
    }

    public void DestroyPlayerAndEnemy()
    {
        if (GameManager.Instance.currentFloor < Floors.Length)
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

    public void CheckCompletedTower()
    {
        if (GameManager.Instance.currentFloor == Floors.Length - 1)
        {
            GameManager.Instance.isCompletedForTower = true;
            SceneManager.LoadScene("SelectStage");
        }
    }

    public void CompleteTower()
    {
        GameManager.Instance.isCompletedForTower = true;
        SceneManager.LoadScene("SelectStage");
    }
}
