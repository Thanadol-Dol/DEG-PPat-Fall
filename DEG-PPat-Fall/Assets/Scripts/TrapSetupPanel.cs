using UnityEngine;

public class TrapSetupPanel : MonoBehaviour
{
    // This method can be called from the cross button's onClick event
    public void ClosePanel()
    {
        Destroy(gameObject);
    }
}
