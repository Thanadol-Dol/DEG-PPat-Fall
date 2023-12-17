using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class WhilePuzzleManager : MonoBehaviour
{
    public Dictionary<string, bool> primaryColor = new Dictionary<string, bool>();
    public Dictionary<string, bool> targetColor = new Dictionary<string, bool>();
    public Tilemap baseTilemap;
    public Tilemap wallTilemap;

    public Image pickedColor;
    public Image pickedColor2;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI roundSuccess;

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
        ResetGame();
    }

    private IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(0.1f);  // Reduce the delay to 0.1 seconds
            countdownTime -= 0.1f;  // Update the countdown time more frequently

            // Update countdownText during the countdown
            countdownText.text = "Time: " + Mathf.Ceil(countdownTime).ToString();  // Round up to the nearest second for display
        }

        // Countdown has reached zero, you can handle the event here
        Debug.Log("Countdown Finished!");
        ResetGame();
    }


    // Start is called before the first frame update
    public void CollectColor(string color)
    {
        collectedColor++;
        primaryColor[color] = true;
        if (collectedColor == 1)
        {
            if (color.Equals("Red"))
            {
                pickedColor.color = new Color(255f, 0f, 0f, 1f);
            }
            else if (color.Equals("Green"))
            {
                pickedColor.color = new Color(0f, 255f, 0f, 1f);
            }
            else if (color.Equals("Blue"))
            {
                pickedColor.color = new Color(0f, 0f, 255f, 1f);
            }
        }
        if (targetColor["White"] && collectedColor == 2)
        {
            if (color.Equals("Red"))
            {
                pickedColor2.color = new Color(255f, 0f, 0f, 1f);
            }
            else if (color.Equals("Green"))
            {
                pickedColor2.color = new Color(0f, 255f, 0f, 1f);
            }
            else if (color.Equals("Blue"))
            {
                pickedColor2.color = new Color(0f, 0f, 255f, 1f);
            }
        }
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
                ResetGame();
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
                ResetGame();
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
                ResetGame();
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
                ResetGame();
            }
        }
    }

    public void ToNextRound()
    {
        currentRound++;
        CheckWin();
        ResetEachRound();
        roundSuccess.text = "Round Success: " + currentRound.ToString();
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
        targetColor[randomColorKey] = true;

        if (targetColor["Violet"])
        {
            baseTilemap.color = new Color(255f, 0f, 255f);
            wallTilemap.color = new Color(255f, 0f, 255f);
        }
        else if (targetColor["Yellow"])
        {
            baseTilemap.color = new Color(255f, 255f, 0f);
            wallTilemap.color = new Color(255f, 255f, 0f);
        }
        else if (targetColor["LightBlue"])
        {
            baseTilemap.color = new Color(0f, 255f, 255f);
            wallTilemap.color = new Color(0f, 255f, 255f);
        }
        else if (targetColor["White"])
        {
            baseTilemap.color = new Color(255f, 255f, 255f);
            wallTilemap.color = new Color(255f, 255f, 255f);
        }

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
        pickedColor.color = new Color(0f, 0f, 0f, 0f);
        pickedColor2.color = new Color(0f, 0f, 0f, 0f);
        PickTargetColor();
    }


    public void ResetGame()
    {
        currentRound = 0;
        countdownTime = 120f;
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
