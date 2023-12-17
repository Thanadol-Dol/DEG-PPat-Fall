using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoWhilePuzzleManager : MonoBehaviour
{
    public Dictionary<string, bool> yourChoice = new Dictionary<string, bool>();
    public Dictionary<string, bool> targetChoice = new Dictionary<string, bool>();
    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorSprite;
    public Image targetLatestChoice;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI roundSuccess;

    public int winningRound = 3;

    public int currentRound = 0;
    private float countdownTime = 120f;
    private Coroutine countdownCoroutine;
    public string opponentLatestChoice = "";

    private void OnEnable()
    {
        yourChoice.Clear();
        targetChoice.Clear();

        yourChoice.Add("Rock", false);
        yourChoice.Add("Paper", false);
        yourChoice.Add("Scissor", false);

        targetChoice.Add("Rock", false);
        targetChoice.Add("Paper", false);
        targetChoice.Add("Scissor", false);
        ResetGame();
    }

    private IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(0.1f);  // Reduce the delay to 0.1 seconds
            countdownTime -= 0.1f;  // Update the countdown time more frequently

            // Update countdownText during the countdown
            countdownText.text = "Timeout: " + Mathf.Ceil(countdownTime).ToString() + " Seconds";  // Round up to the nearest second for display
        }

        // Countdown has reached zero, you can handle the event here
        Debug.Log("Countdown Finished!");
        ResetGame();
    }

    // Start is called before the first frame update
    public void CollectChoice(string choice)
    {
        yourChoice[choice] = true;
        //targetLatestChoice.sprite = targetSprite;
        CheckRound();
    }

    public void CheckRound()
    {
        if (targetChoice["Rock"])
        {
            if (yourChoice["Paper"])
            {
                Debug.Log("Correct!");
                ToNextRound();
            }
            else
            {
                Debug.Log("Wrong!");
                ResetGame();
            }
        }
        else if (targetChoice["Paper"])
        {
            if (yourChoice["Scissor"])
            {
                Debug.Log("Correct!");
                ToNextRound();
            }
            else
            {
                Debug.Log("Wrong!");
                ResetGame();
            }
        }
        else if (targetChoice["Scissor"])
        {
            if (yourChoice["Rock"])
            {
                Debug.Log("Correct!");
                ToNextRound();
            }
            else
            {
                Debug.Log("Wrong!");
                ResetGame();
            }
        }
        if(opponentLatestChoice == "Rock")
        {
            targetLatestChoice.sprite = rockSprite;
        }
        else if(opponentLatestChoice == "Paper")
        {
            targetLatestChoice.sprite = paperSprite;
        }
        else if(opponentLatestChoice == "Scissor")
        {
            targetLatestChoice.sprite = scissorSprite;
        }
    }

    public void ToNextRound()
    {
        currentRound++;
        CheckWin();
        ResetEachRound();
        roundSuccess.text = "Round Success: " + currentRound.ToString() + " / " + winningRound.ToString();
    }

    public void CheckWin()
    {
        if (currentRound == winningRound)
        {
            Debug.Log("You win!");
            TowerManager towerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
            towerManager.goNextFloor();
        }
    }

    public void PickTargetChoice()
    {
        // Get a random choice from targetChoice keys
        string[] choiceKeys = targetChoice.Keys.ToArray();
        string randomChoiceKey;
        do{
            randomChoiceKey = choiceKeys[UnityEngine.Random.Range(0, choiceKeys.Length)];
        }while(string.Compare(randomChoiceKey, opponentLatestChoice) == 0);
    
        // Update the latest choice of the target
        opponentLatestChoice = randomChoiceKey;
        Debug.Log("Latest choice: " + opponentLatestChoice);

        // Toggle the value of the randomly chosen choice
        targetChoice[randomChoiceKey] = true;
        Debug.Log("Picked target choice: " + randomChoiceKey + ", Value toggled: " + targetChoice[randomChoiceKey]);
    }

    public void ResetEachRound()
    {
        // Create copies of keys to avoid modifying the dictionary during iteration
        List<string> yourChoiceKeys = new List<string>(yourChoice.Keys);
        List<string> targetChoiceKeys = new List<string>(targetChoice.Keys);

        foreach (var choice in yourChoiceKeys)
        {
            yourChoice[choice] = false; // Modifying the collection while iterating
        }
        foreach (var choice in targetChoiceKeys)
        {
            targetChoice[choice] = false; // Modifying the collection while iterating
        }
        PickTargetChoice();
    }

    public void ResetGame()
    {
        currentRound = 0;
        countdownTime = 120f;
        roundSuccess.text = "Round Success: " + currentRound.ToString() + " / " + winningRound.ToString();
        ResetEachRound();

        // Stop the existing coroutine before starting a new one
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        countdownCoroutine = StartCoroutine(StartCountdown());  // Start a new countdown coroutine
        Debug.Log("Game Reset!");
    }

}
