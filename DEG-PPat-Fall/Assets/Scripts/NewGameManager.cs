using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    GameManager gameManager;
    
    public void SelectDifficulty(string difficulty)
    {
        gameManager.difficulty = difficulty;
        
    }
}
