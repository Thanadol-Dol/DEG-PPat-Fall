using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    public Button ForButton;
    public Button WhileButton;
    public Button DoWhileButton;
    public Button MasterButton;

    void Start()
    {
        Debug.Log("Tower : "+ GameManager.Instance.currentTower + " Floor : " + GameManager.Instance.currentFloor);
        isClickableTower();
    }

    void Update()
    {
        isClickableTower();
    }

    private void isClickableTower(){

        if(GameManager.Instance.isCompletedMasterTower == false){
            
            if(GameManager.Instance.isCompletedForTower == false){
                ForButton.interactable = true;
            }else{
                ForButton.interactable = false;
            }

            if(GameManager.Instance.isCompletedWhileTower == false){
                WhileButton.interactable = true;
            }else{
                WhileButton.interactable = false;
            }

            if(GameManager.Instance.isCompletedDoWhileTower == false){
                DoWhileButton.interactable = true;
            }else{
                DoWhileButton.interactable = false;
            }

            if(GameManager.Instance.isCompletedForTower == true && GameManager.Instance.isCompletedWhileTower == true && GameManager.Instance.isCompletedDoWhileTower == true){
                MasterButton.interactable = true;
            }
            else{
                MasterButton.interactable = false;
            }

        }else{
            ForButton.interactable = true;
            WhileButton.interactable = true;
            DoWhileButton.interactable = true;
            MasterButton.interactable = true;
        }
        
    }

    private void detailStage()
    {
        GameManager.Instance.currentFloor = 0;
        GameManager.Instance.SelectedTower.Add(GameManager.Instance.currentTower);
    }

    public void SelectForButton()
    {
        GameManager.Instance.currentTower = "For";
        detailStage();
        if(!GameManager.Instance.isCompletedForTower){
            StartStage();
        }
        else{
            ReStage();
        }
    }

    public void SelectWhileButton()
    {
        GameManager.Instance.currentTower = "While";
        detailStage();
        if(!GameManager.Instance.isCompletedWhileTower){
            StartStage();
        }
        else{
            ReStage();
        }
    }

    public void SelectDoWhileButton()
    {
        GameManager.Instance.currentTower = "DoWhile";
        detailStage();
        if(!GameManager.Instance.isCompletedDoWhileTower){
            StartStage();
        }
        else{
            ReStage();
        }
    }

    public void SelectMasterButton()
    {
        GameManager.Instance.currentTower = "Master";
        detailStage();
        if(!GameManager.Instance.isCompletedMasterTower){
            StartStage();
        }
        else{
            ReStage();
        }

    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void StartStage()
    {
        int currentLevel = GameManager.Instance.currentLevel;
        switch(currentLevel){
            case 1:
                SceneManager.LoadScene("Stage1");
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                break;
            case 3:
                SceneManager.LoadScene("Stage3");
                break;
            case 4:
                SceneManager.LoadScene("StageMaster");
                break;
        }
    }

    private void ReStage()
    {
        int Selectedlevel = GameManager.Instance.SelectedTower.IndexOf(GameManager.Instance.currentTower) + 1;

        switch(Selectedlevel){
            case 1:
                SceneManager.LoadScene("Stage1");
                break;
            case 2:
                SceneManager.LoadScene("Stage2");
                break;
            case 3:
                SceneManager.LoadScene("Stage3");
                break;
            case 4:
                SceneManager.LoadScene("StageMaster");
                break;
        }
        
    }
}
