using System.Collections;
using UnityEngine;
using TMPro;

public class ForPuzzleManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    private float countdownTime = 10f;

    private void OnEnable()
    {
        Debug.Log("Script enabled, starting countdown");
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(0.1f);
            countdownTime -= 0.1f;

            // Update countdownText during the countdown
            countdownText.text = "Survive Within: " + Mathf.Ceil(countdownTime).ToString() + " Seconds";
        }

        Debug.Log("Countdown Finished!");
        TowerManager towerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
        towerManager.goNextFloor();
    }
}