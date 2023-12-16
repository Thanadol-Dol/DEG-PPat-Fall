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
        TowerManager towerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
        Debug.Log(towerManager.filePuzzleConverter[filePanelName]);
        switch(towerManager.filePuzzleConverter[filePanelName]){
            case 1:
                if(CompareCalculate(2,answers[0],3)){
                    return true;
                } else {
                    return false;
                }
            case 2:
                if(CompareCalculate(4,answers[0],4)){
                    return true;
                } else {
                    return false;
                }
            case 3:
                if(CompareCalculate(5,answers[0],4)){
                    return true;
                } else {
                    return false;
                }
            case 4:
                if(CompareCalculate(13,answers[0],7)){
                    return true;
                } else {
                    return false;
                }
            case 5:
                if(CompareCalculate(25,answers[0],1)){
                    return true;
                } else {
                    return false;
                }
            case 6:
                if(CompareCalculate(20,answers[0],35)){
                    return true;
                } else {
                    return false;
                }
            case 7:
                if(CompareCalculate(11,answers[0],22)){
                    return true;
                } else {
                    return false;
                }
            case 8:
                if(CompareCalculate(13,answers[0],19)){
                    return true;
                } else {
                    return false;
                }
            case 9:
                if(CompareCalculate(11,answers[0],29)){
                    return true;
                } else {
                    return false;
                }
            case 10:
                if(CompareCalculate(11,answers[0],10)){
                    return true;
                } else {
                    return false;
                }
            case 11:
                if(CompareCalculate(18,answers[0],18)){
                    return true;
                } else {
                    return false;
                }
            case 12:
                if(CompareCalculate(13,answers[0],19) || (0<3)){
                    return true;
                } else {
                    return false;
                }
            case 13:
                if(CompareCalculate(11,answers[0],36) && (31>29)){
                    return true;
                } else {
                    return false;
                }
            case 14:
                if(CompareCalculate(8,answers[0],11) && (8>=0)){
                    return true;
                } else {
                    return false;
                }
            case 15:
                if(CompareCalculate(18,answers[0],11) && (0==0)){
                    return true;
                } else {
                    return false;
                }
            case 16:
                if(CompareCalculate(19,answers[0],19)){
                    return true;
                } else {
                    return false;
                }
            case 17:
                if(CompareCalculate(15,answers[0],4)){
                    return true;
                } else {
                    return false;
                }
            case 18:
                if(CompareCalculate(19,answers[0],11)){
                    return true;
                } else {
                    return false;
                }
            case 19:
                if(CompareCalculate(1,answers[0],4)){
                    return true;
                } else {
                    return false;
                }
            case 20:
                if(CompareCalculate(13,answers[0],0) && (1<3)){
                    return true;
                } else {
                    return false;
                }
            case 21:
                if(CompareCalculate(17,answers[0],16) && (3<23)){
                    return true;
                } else {
                    return false;
                }
            case 22:
                if(CompareCalculate(3,answers[0],10) && (11>3)){
                    return true;
                } else {
                    return false;
                }
            case 23:
                if(CompareCalculate(17,answers[0],19) && (7<23)){
                    return true;
                } else {
                    return false;
                }
            case 24:
                if(CompareCalculate(14,answers[0],5)){
                    return true;
                } else {
                    return false;
                }
            case 25:
                if(CompareCalculate(15,answers[0],14)){
                    return true;
                } else {
                    return false;
                }
            case 26:
                if(CompareCalculate(19,answers[0],17)){
                    return true;
                } else {
                    return false;
                }
            case 27:
                if(CompareCalculate(36,answers[0],14)){
                    return true;
                } else {
                    return false;
                }
            case 28:
                if(CompareCalculate(10,answers[0],22) || (8<4)){
                    return true;
                } else {
                    return false;
                }
            case 29:
                if(CompareCalculate(12,answers[0],11) || (5<9)){
                    return true;
                } else {
                    return false;
                }
            case 30:
                if(CompareCalculate(15,answers[0],29) || (20<11)){
                    return true;
                } else {
                    return false;
                }
            case 31:
                if(CompareCalculate(7,answers[0],7) || (33<2)){
                    return true;
                } else {
                    return false;
                }
            default:
                Debug.Log("Invalid file number");
                return true;
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
