using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCalculator : MonoBehaviour
{
    public int SetControl(List<string> answers,int enemyStatus,int trapNumber,int? extraNumber = null){
        int extraNumberValue = 0;
        if(extraNumber != null){
            extraNumberValue = extraNumber.Value;
        }
        switch(trapNumber){
            case 0:
                if(CompareCalculate(6,answers[0],enemyStatus)){
                    return 5;
                } else {
                    return 0;
                }
            case 1:
                if(CompareCalculate(5,answers[0],enemyStatus)){
                    return 0;
                } else {
                    return 5;
                }
            case 2:
                if(CompareCalculate(5,answers[0],enemyStatus)){
                    return 0;
                } else if (CompareCalculate(5,answers[1],enemyStatus)){
                    return 5;
                } else {
                    return 0;
                }
            case 3:
                if(CompareCalculate(6,answers[0],enemyStatus)){
                    if(CompareCalculate(6,answers[1],extraNumberValue)){
                        return 8;
                    } else {
                        return 0;
                    }
                } else {
                    return 0;
                }
            case 4:
                if(CompareCalculate(5,answers[0],enemyStatus)){
                    return 0;
                } else {
                    if(CompareCalculate(5,answers[1],extraNumberValue)){
                        return 8;
                    } else {
                        return 0;
                    }
                }
            case 5:
                if(CompareCalculate(5,answers[0],enemyStatus)){
                    return 0;
                } else if(CompareCalculate(5,answers[1],enemyStatus)){
                    if(CompareCalculate(5,answers[2],extraNumberValue)){
                        return 8;
                    } else {
                        return 0;
                    }
                } else{
                    return 0;
                }
            case 6:
                if(CompareCalculate(6,answers[0],enemyStatus) && CompareCalculate(6,answers[1],extraNumberValue)){
                    if(CompareCalculate(enemyStatus,answers[2],extraNumberValue)){
                        return 8;
                    } else {
                        return 0;
                    }
                } else {
                    return 0;
                }
            case 7:
                if(CompareCalculate(5,answers[0],enemyStatus) && CompareCalculate(5,answers[1],extraNumberValue)){
                    return 0;
                } else {
                    if(CompareCalculate(enemyStatus,answers[2],extraNumberValue)){
                        return 8;
                    } else {
                        return 0;
                    }
                }
            case 8:
                if(CompareCalculate(5,answers[0],enemyStatus) && CompareCalculate(5,answers[1],extraNumberValue)){
                    return 0;
                } else if(CompareCalculate(5,answers[2],enemyStatus)){
                    if(CompareCalculate(enemyStatus,answers[3],extraNumberValue)){
                        return 8;
                    } else {
                        return 0;
                    }
                } else {
                    return 0;
                }
            case 9:
            case 10:
                int stunTime1 = enemyStatus - int.Parse(answers[0]);
                if(stunTime1 < 0){
                    stunTime1 = 0;
                }
                return stunTime1;
            case 11:
                int stunTime2 = int.Parse(answers[0]) - enemyStatus - 1;
                if(stunTime2 < 0){
                    stunTime2 = 0;
                }
                return stunTime2;
            default:
                Debug.Log("Invalid trap number");
                return 0;
        }
    }

    public bool CompareCalculate(int num1, string op, int num2){
        if(op == "=="){
            return num1 == num2;
        } else if(op == ">"){
            return num1 > num2;
        } else if(op == "<"){
            return num1 < num2;
        } else {
            Debug.Log("Invalid operator");
            return false;
        }
    }
}
