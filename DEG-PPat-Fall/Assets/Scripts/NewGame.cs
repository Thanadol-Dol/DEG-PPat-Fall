using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewGame : MonoBehaviour
{
    
    public void SelectDifficulty(string difficulty)
    {
        GameManager.Instance.difficulty = difficulty;

        GameManager.Instance.currentTower = "NonSelect";
        GameManager.Instance.currentFloor = 0;
        GameManager.Instance.isCompletedForTower = false;
        GameManager.Instance.isCompletedWhileTower = false;
        GameManager.Instance.isCompletedDoWhileTower = false;
        GameManager.Instance.isCompletedMasterTower = false;
        GameManager.Instance.isCompletedIntro = false;
        GameManager.Instance.currentLevel = 1;
        GameManager.Instance.SelectedTower = new List<string>();

        SceneManager.LoadScene("Intro");
    }
    
    public void Start(){
        Debug.Log("Tower : "+ GameManager.Instance.currentTower + " Floor : " + GameManager.Instance.currentFloor);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
