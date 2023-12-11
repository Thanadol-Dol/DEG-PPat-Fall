using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    
    public void SelectDifficulty(string difficulty)
    {
        GameManager.Instance.difficulty = difficulty;
    }
    
    public void Start(){
        Debug.Log(GameManager.Instance.currentTower + " " + GameManager.Instance.currentFloor);
    }
}
