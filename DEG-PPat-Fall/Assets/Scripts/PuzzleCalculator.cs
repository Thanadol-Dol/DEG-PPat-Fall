using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCalculator : MonoBehaviour
{
    public void SetControl(List<string> answers,int enemyStatus,int trapNumber){
        switch(trapNumber){
            case 0:
                string expression = "6"+answers[0]+enemyStatus.ToString();
                Debug.Log(expression);
                break;
            case 1:
                Debug.Log("Trap 2");
                break;
            case 2:
                Debug.Log("Trap 3");
                break;
            case 3:
                Debug.Log("Trap 4");
                break;
            case 4:
                Debug.Log("Trap 5");
                break;
            case 5:
                Debug.Log("Trap 6");
                break;
            case 6:
                Debug.Log("Trap 7");
                break;
            case 7:
                Debug.Log("Trap 8");
                break;
            case 8:
                Debug.Log("Trap 9");
                break;
            case 9:
                Debug.Log("Trap 10");
                break;
            case 10:
                Debug.Log("Trap 11");
                break;
            case 11:
                Debug.Log("Trap 12");
                break;
            case 12:
                Debug.Log("Trap 13");
                break;
        }
    }
}
