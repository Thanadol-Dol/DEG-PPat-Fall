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
            Debug.Log("Switching to next floor");
            SwitchToNextFloor();
            FindSpawnPoint();
        }
    }

    void SwitchToNextFloor()
    {
        // Clear the current floor before deactivating
        Floors[GameManager.Instance.currentFloor].GetComponent<Floor>().ClearTilemaps();

        // Deactivate the current floor
        Floors[GameManager.Instance.currentFloor].SetActive(false);

        // Increment the floor index
        GameManager.Instance.currentFloor++;

        // Activate the next floor
        Floors[GameManager.Instance.currentFloor].SetActive(true);

        // Set up the new floor
        SetupCurrentFloor();
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
}
