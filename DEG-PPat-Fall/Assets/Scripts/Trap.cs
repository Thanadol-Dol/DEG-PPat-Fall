using UnityEngine;

public class Trap : MonoBehaviour
{
    public float pickupRange;
    public GameObject trapSetupPanelPrefab;

    private bool isSetup;

    private void Start() {
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
        }
    }

    private bool IsPlayerWithinPickupRange(Vector3 playerPosition)
    {
        float distance = Vector2.Distance(playerPosition, transform.position);
        return distance <= pickupRange;
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

    public void SetTrap()
    {
        isSetup = true;
    }
}
