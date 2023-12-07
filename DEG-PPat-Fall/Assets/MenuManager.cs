using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    void Start()
    {
        
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
}
