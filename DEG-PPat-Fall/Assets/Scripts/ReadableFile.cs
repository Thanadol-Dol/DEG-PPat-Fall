using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReadableFile : MonoBehaviour
{
    public float pickupRange;
    private GameObject filePanel;
    public GameObject fileContent;
    TowerManager towerManager = null;

    public List<string> answers = new List<string>();

    private IEnumerator Start()
    {
        pickupRange = 2f;
        yield return StartCoroutine(WaitForTowerManagerInitialization());

        // Now you can access towerManager.fileNameToContent dictionary
        filePanel = GameManager.Instance.allReadableFilePanel
            .FirstOrDefault(x => x.name == towerManager.fileNameToContent[fileContent.name]);
    }

    private IEnumerator WaitForTowerManagerInitialization()
    {
        while (towerManager == null)
        {
            towerManager = GameObject.Find("TowerManager")?.GetComponent<TowerManager>();
            yield return null; // Wait for the next frame
        }
        // TowerManager is now initialized
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                Player playerScript = playerObject.GetComponent<Player>();

                if (playerScript != null)
                {
                    if (IsPlayerWithinPickupRange(playerObject.transform.position))
                    {
                        ShowPanel();
                        playerScript.isReadableFilePanelOpen = true;
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

    private void ShowPanel()
    {
        if (filePanel != null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                // Instantiate a unique panel for each trap
                GameObject readableFilePanel = Instantiate(filePanel, canvas.transform);

                // Pass a reference to the trap to the panel
                ReadableFilePanel fileScript = readableFilePanel.GetComponent<ReadableFilePanel>();
                if (fileScript != null)
                {
                    fileScript.SetFileReference(this);
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

    public void CheckAnswer()
    {
        PuzzleCalculator puzzleCalculator = GameObject.Find("PuzzleCalculator").GetComponent<PuzzleCalculator>();
        bool canAdd = puzzleCalculator.AddingFileCalculate(answers, fileContent.name);
        Debug.Log(answers[0]);
        Debug.Log(fileContent.name);
        if (canAdd)
        {
            TowerManager towerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
            towerManager.AddTopic(fileContent.name);
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }
}
