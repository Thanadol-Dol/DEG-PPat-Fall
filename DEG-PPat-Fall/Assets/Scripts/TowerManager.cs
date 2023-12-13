using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerManager : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        playerSpawnPoint = GameObject.Find("PlayerSpawn").transform;
        enemySpawnPoint = GameObject.Find("EnemySpawn").transform;
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
