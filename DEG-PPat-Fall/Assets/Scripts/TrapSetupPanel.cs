using UnityEngine;

public class TrapSetupPanel : MonoBehaviour
{
    private Trap trap;

    public void SetTrapReference(Trap trapReference)
    {
        trap = trapReference;
    }

    // This method can be called from the close button's onClick event
    public void ClosePanel()
    {
        if (trap != null)
        {
            // Interact with the specific trap
            Debug.Log("Closing panel for Trap: " + trap.name);

            // Destroy the panel
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Trap reference not set in the panel script.");
        }
    }

    public void SetTrap()
    {
        if (trap != null)
        {
            // Interact with the specific trap
            Debug.Log("Setting Trap: " + trap.name);

            // Destroy the panel
            Destroy(gameObject);
            trap.SetTrap();
        }
        else
        {
            Debug.LogError("Trap reference not set in the panel script.");
        }
    }
}
