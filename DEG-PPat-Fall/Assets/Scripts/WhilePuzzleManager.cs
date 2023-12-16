using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhilePuzzleManager : MonoBehaviour
{
    public Dictionary<string, bool> primaryColor = new Dictionary<string, bool>();
    public Dictionary<string, bool> targetColor = new Dictionary<string, bool>();

    public int winningRound = 3;

    public int currentRound = 0;
    public int collectedColor = 0;
    private float countdownTime = 120f;
    private Coroutine countdownCoroutine;

    private void Start()
    {
        primaryColor.Add("Red", false);
        primaryColor.Add("Blue", false);
        primaryColor.Add("Green", false);

        targetColor.Add("Violet", false);
        targetColor.Add("Yellow", false);
        targetColor.Add("LightBlue", false);
        targetColor.Add("White", false);
        PickTargetColor();
        countdownCoroutine = StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            UpdateCountdownLog();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        // Countdown has reached zero, you can handle the event here
        Debug.Log("Countdown Finished!");
        ResetGame();
    }

    private void UpdateCountdownLog()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
    }

    // Start is called before the first frame update
    public void CollectColor(string color)
    {
        collectedColor++;
        primaryColor[color] = true;
        if (collectedColor >= 2)
        {
            CheckRound();
        }
    }

    public void CheckRound()
    {
        if (targetColor["Violet"])
        {
            if (primaryColor["Red"] && primaryColor["Blue"])
            {
                Debug.Log("Correct!");
                ToNextRound();
            }
            else
            {
                Debug.Log("Wrong!");
                StartCoroutine(DelayedResetGame());
            }
        }
        else if (targetColor["Yellow"])
        {
            if (primaryColor["Red"] && primaryColor["Green"])
            {
                Debug.Log("Correct!");
                ToNextRound();
            }
            else
            {
                Debug.Log("Wrong!");
                StartCoroutine(DelayedResetGame());
            }
        }
        else if (targetColor["LightBlue"])
        {
            if (primaryColor["Blue"] && primaryColor["Green"])
            {
                Debug.Log("Correct!");
                ToNextRound();
            }
            else
            {
                Debug.Log("Wrong!");
                StartCoroutine(DelayedResetGame());
            }
        }
        else if (targetColor["White"] && collectedColor == 3)
        {
            if (primaryColor["Red"] && primaryColor["Blue"] && primaryColor["Green"])
            {
                Debug.Log("Correct!");
                ToNextRound();
            }
            else
            {
                Debug.Log("Wrong!");
                StartCoroutine(DelayedResetGame());
            }
        }
        CheckWin();
    }

    private IEnumerator DelayedResetGame()
    {
        yield return new WaitForSeconds(1f); // Adjust the delay time as needed
        StopCoroutine(countdownCoroutine);  // Stop the existing countdown coroutine
        ResetGame();
    }

    public void ToNextRound()
    {
        currentRound++;
        ResetEachRound();
        PickTargetColor();
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

    public void PickTargetColor()
    {
        // Get a random color from targetColor keys
        string[] colorKeys = targetColor.Keys.ToArray();
        string randomColorKey = colorKeys[UnityEngine.Random.Range(0, colorKeys.Length)];

        // Toggle the value of the randomly chosen color
        targetColor[randomColorKey] = !targetColor[randomColorKey];

        Debug.Log("Picked target color: " + randomColorKey + ", Value toggled: " + targetColor[randomColorKey]);
    }

    public void ResetEachRound()
    {
        collectedColor = 0;

        // Create copies of keys to avoid modifying the dictionary during iteration
        List<string> primaryColorKeys = new List<string>(primaryColor.Keys);
        List<string> targetColorKeys = new List<string>(targetColor.Keys);

        foreach (var color in primaryColorKeys)
        {
            primaryColor[color] = false; // Modifying the collection while iterating
        }
        foreach (var color in targetColorKeys)
        {
            targetColor[color] = false; // Modifying the collection while iterating
        }
        PickTargetColor();
    }


    public void ResetGame()
    {
        currentRound = 0;
        countdownTime = 120f;
        ResetEachRound();
        countdownCoroutine = StartCoroutine(StartCountdown());  // Start a new countdown coroutine
    }
}
