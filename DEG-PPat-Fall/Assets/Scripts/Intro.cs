using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Button continueButton;
    public Button BackButton;

    private float activeTime = 1.5f;
    public void Start()
    {
        continueButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        Debug.Log("Tower : "+ GameManager.Instance.currentTower + " Floor : " + GameManager.Instance.currentFloor);
        Invoke("ActtionButton", activeTime);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SelectStage()
    {
        SceneManager.LoadScene("SelectStage");
    }

    private void ActtionButton()
    {
        continueButton.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
    }
}
