using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMaterial : MonoBehaviour
{
    public float pickupRange = 2f; // Adjust this value to set the pickup range

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
                    // Check the distance between the player and the TrapMaterial
                    float distance = Vector2.Distance(playerObject.transform.position, transform.position);

                    // Check if the player is within the pickup range
                    if (distance <= pickupRange)
                    {
                        // Increase trapMaterials in the Player script
                        playerScript.CollectMaterial();

                        // Destroy the TrapMaterial object
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Player is not within pickup range.");
                        // Optionally provide feedback or do nothing if the player is not within range.
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
}