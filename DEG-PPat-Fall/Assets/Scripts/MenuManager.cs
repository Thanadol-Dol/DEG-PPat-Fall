using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button continueButton;
    public Button newGameButton;
    public Button LoadGameButton;
    public Button settingButton;
    public Button quitGameButton;

    void Start()
    {
        newGameButton.interactable = true;
        settingButton.interactable = true;
        quitGameButton.interactable = true;

        string currentTower = GameManager.Instance.currentTower;
        string checkTower = "NonSelect";

        if (string.Equals(currentTower,checkTower, System.StringComparison.OrdinalIgnoreCase))
        {
            continueButton.interactable = false;
            LoadGameButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
            LoadGameButton.interactable = true;
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void continueGame()
    {
        //SceneManager.LoadScene("Level1");
    }

    public void newGame()
    {
        SceneManager.LoadScene("NewGame");
    }

    public void loadGame()
    {
        SceneManager.LoadScene("LoadGame");
    }
    public void setting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void PlayerTest()
    {
        SceneManager.LoadScene("PlayerTest");
    }

    public void Stage1()
    {
        SceneManager.LoadScene("Stage1");
    }
}
