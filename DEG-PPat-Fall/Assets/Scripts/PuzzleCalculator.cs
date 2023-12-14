using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCalculator : MonoBehaviour
{
    public int SetControl(List<string> answers,int enemyStatus,int trapNumber,int? extraNumber = null){
        int stunTime = 0;
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
                    stunTime += 5;
                    if(CompareCalculate(6,answers[1],extraNumberValue)){
                        stunTime += 3;
                        return stunTime;
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
                    stunTime += 5;
                    if(CompareCalculate(5,answers[1],extraNumberValue)){
                        stunTime += 3;
                        return stunTime;
                    } else {
                        return 0;
                    }
                }
            case 5:
                if(CompareCalculate(5,answers[0],enemyStatus)){
                    return 0;
                } else if(CompareCalculate(5,answers[1],enemyStatus)){
                    stunTime += 5;
                    if(CompareCalculate(5,answers[2],extraNumberValue)){
                        stunTime += 3;
                        return stunTime;
                    } else {
                        return 0;
                    }
                } else{
                    return 0;
                }
            case 6:
                if(CompareCalculate(6,answers[0],enemyStatus) && CompareCalculate(6,answers[1],extraNumberValue)){
                    stunTime += 5;
                    if(CompareCalculate(enemyStatus,answers[2],extraNumberValue)){
                        stunTime += 3;
                        return stunTime;
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
                    stunTime += 5;
                    if(CompareCalculate(enemyStatus,answers[2],extraNumberValue)){
                        stunTime += 3;
                        return stunTime;
                    } else {
                        return 0;
                    }
                }
            case 8:
                if(CompareCalculate(5,answers[0],enemyStatus) && CompareCalculate(5,answers[1],extraNumberValue)){
                    return 0;
                } else if(CompareCalculate(5,answers[2],enemyStatus)){
                    stunTime += 5;
                    if(CompareCalculate(enemyStatus,answers[3],extraNumberValue)){
                        stunTime += 3;
                        return stunTime;
                    } else {
                        return 0;
                    }
                } else {
                    return 0;
                }
            case 9:
            case 10:
                stunTime = enemyStatus - int.Parse(answers[0]);
                if(stunTime < 0){
                    stunTime = 0;
                }
                return stunTime;
            case 11:
                stunTime = int.Parse(answers[0]) - enemyStatus - 1;
                if(stunTime < 0){
                    stunTime = 0;
                }
                return stunTime;
            default:
                Debug.Log("Invalid trap number");
                return 0;
        } 
    }

    public bool AddingFileCalculate(List<string> answers,string filePanelName){
        return true;
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
