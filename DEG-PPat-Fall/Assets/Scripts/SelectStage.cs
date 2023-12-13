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

    public void SelectForButton()
    {
        GameManager.Instance.currentTower = "For";
        GameManager.Instance.currentFloor = 0;
        
        SceneManager.LoadScene("ForTower");
    }

    public void SelectWhileButton()
    {
        GameManager.Instance.currentTower = "While";
        GameManager.Instance.currentFloor = 0;
        
        SceneManager.LoadScene("WhileTower");
    }

    public void SelectDoWhileButton()
    {
        GameManager.Instance.currentTower = "DoWhile";
        GameManager.Instance.currentFloor = 0;
        
        SceneManager.LoadScene("DoWhileTower");
    }

    public void SelectMasterButton()
    {
        GameManager.Instance.currentTower = "Master";
        GameManager.Instance.currentFloor = 0;
        
        SceneManager.LoadScene("MasterTower");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void StartStage()
    {
        
    }
}
