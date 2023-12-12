using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewGameManager : MonoBehaviour
{
    
    public void SelectDifficulty(string difficulty)
    {
        GameManager.Instance.difficulty = difficulty;
        SceneManager.LoadScene("Intro");
    }
    
    public void Start(){
        Debug.Log("Tower"+ GameManager.Instance.currentTower + "Floor" + GameManager.Instance.currentFloor);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
