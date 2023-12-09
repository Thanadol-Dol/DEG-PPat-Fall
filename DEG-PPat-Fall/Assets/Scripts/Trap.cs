using UnityEngine;
using UnityEngine.UI;

public class Trap : MonoBehaviour
{
    public float pickupRange = 2f;
    public GameObject trapSetupPanelPrefab;

    private void OnMouseDown()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Find the Player object in the scene
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            // Check if the Player object is found
            if (playerObject != null)
            {
                // Get the Player script from the Player object
                Player playerScript = playerObject.GetComponent<Player>();

                // Check if the Player script is found
                if (playerScript != null)
                {
                    // Check if the player is within the pickup range
                    if (IsPlayerWithinPickupRange(playerObject.transform.position))
                    {
                        // Show the trap setup panel
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
    }

    private bool IsPlayerWithinPickupRange(Vector3 playerPosition)
    {
        // Check the distance between the player and the trap
        float distance = Vector2.Distance(playerPosition, transform.position);

        // Check if the player is within the pickup range
        return distance <= pickupRange;
    }

    private void ShowTrapSetupPanel()
    {
        // Check if the trapSetupPanelPrefab is assigned
        if (trapSetupPanelPrefab != null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if(canvas != null){
                GameObject trapSetupPanel = Instantiate(trapSetupPanelPrefab, canvas.transform.position, Quaternion.identity);
                trapSetupPanel.transform.SetParent(canvas.transform);
                Debug.Log("Showing trap setup panel.");
            } else {
                Debug.LogError("Canvas not found in the scene.");
            }
        }
        else
        {
            Debug.LogError("Trap setup panel prefab is not assigned in the inspector.");
        }
    }
}
