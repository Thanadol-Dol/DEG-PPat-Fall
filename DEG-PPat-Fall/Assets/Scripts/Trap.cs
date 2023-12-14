using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float pickupRange;
    public GameObject trapSetupPanelPrefab;
    public int puzzleNumber;

    public bool isSetup;
    public List<string> answers = new List<string>();

    public int stunTime;
    public int? extraNumber = null;

    private void Start()
    {
        pickupRange = 2f;
        isSetup = false;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !isSetup)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                Player playerScript = playerObject.GetComponent<Player>();

                if (playerScript != null)
                {
                    if (IsPlayerWithinPickupRange(playerObject.transform.position))
                    {
                        ShowTrapSetupPanel();
                        playerScript.isTrapPanelOpen = true;
                    }
                    else
                    {
                        Debug.Log("Player is not within pickup range.");
                    }
                }
                else
                {
                    Debug.LogError("Player script not found on the Player object.");
                }
            }
            else
            {
                Debug.LogError("Player object not found in the scene.");
            }
        }
        else
        {
            Debug.Log("Trap is already setup.");
            foreach (string answer in answers)
            {
                Debug.Log(answer);
            }
        }
    }

    private bool IsPlayerWithinPickupRange(Vector3 playerPosition)
    {
        float distance = Vector2.Distance(playerPosition, transform.position);
        return distance <= pickupRange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && isSetup)
        {
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
            int enemyStatus = enemyScript.status;
            Debug.Log("Stun Calculator" + enemyStatus);
            PuzzleCalculator puzzleCalculator = GameObject.Find("PuzzleCalculator").GetComponent<PuzzleCalculator>();
            Debug.Log("Answers: " + answers);
            Debug.Log("Enemy Status: " + enemyStatus);
            Debug.Log("Puzzle Number: " + puzzleNumber);
            if (puzzleCalculator != null)
            {
                stunTime = puzzleCalculator.SetControl(answers, enemyStatus, puzzleNumber, extraNumber);
                enemyScript.ApplyStun(stunTime);
            }
            else
            {
                Debug.LogError("PuzzleCalculator script not found on the PuzzleCalculator object.");
            }
            Destroy(this.gameObject);
        }
    }

    private void ShowTrapSetupPanel()
    {
        if (trapSetupPanelPrefab != null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                // Instantiate a unique panel for each trap
                GameObject trapSetupPanel = Instantiate(trapSetupPanelPrefab, canvas.transform);

                // Pass a reference to the trap to the panel
                TrapSetupPanel panelScript = trapSetupPanel.GetComponent<TrapSetupPanel>();
                if (panelScript != null)
                {
                    panelScript.SetTrapReference(this);
                }
                else
                {
                    Debug.LogError("TrapSetupPanel script not found on the panel prefab.");
                }
            }
            else
            {
                Debug.LogError("Canvas not found in the scene.");
            }
        }
        else
        {
            Debug.LogError("Trap setup panel prefab is not assigned in the inspector.");
        }
    }

    public void DestroyAfterDelay()
    {
        float timeToLive = 0f;
        string currentDifficulty = GameManager.Instance.difficulty;
        int currentLevel = GameManager.Instance.currentLevel;
        if(currentDifficulty.Equals("Beginner")){
            if(currentLevel == 1){
                return;
            } else if(currentLevel == 2){
                timeToLive = 12.0f;
            } else if(currentLevel == 3){
                timeToLive = 10.0f;
            } else if(currentLevel == 4){
                timeToLive = 10.0f;
            }
        } else if(currentDifficulty.Equals("Normal")){
            if(currentLevel == 1){
                return;
            } else if(currentLevel == 2){
                timeToLive = 10.0f;
            } else if(currentLevel == 3){
                timeToLive = 8.0f;
            } else if(currentLevel == 4){
                timeToLive = 8.0f;
            }
        } else {
            if(currentLevel == 1){
                timeToLive = 10.0f;
            } else if(currentLevel == 2){
                timeToLive = 8.0f;
            } else if(currentLevel == 3){
                timeToLive = 6.0f;
            } else if(currentLevel == 4){
                timeToLive = 6.0f;
            }
        }
        Debug.Log("Time to live: " + timeToLive);
        StartCoroutine(DestroyAfterDelayCoroutine(timeToLive));
    }

    private IEnumerator DestroyAfterDelayCoroutine(float timeToLive)
    {
        yield return new WaitForSeconds(timeToLive); // Wait for 5 seconds

        // Additional cleanup or actions before destroying the object can be added here

        Destroy(this.gameObject); // Destroy the trap object
    }
}
