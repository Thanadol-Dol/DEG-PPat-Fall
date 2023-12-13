using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{

    private int currentPanelIndex = 0;
    private float activeTime = 1.5f;
    public Button continueButton;
    public Button BackButton;
    public GameObject[] cutscenePanels;
    //public CanvasGroup[] cutscenePanels;
    private CanvasGroup canvasGroup;
    public void Start()
    {
    
        continueButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        Debug.Log("Tower : "+ GameManager.Instance.currentTower + " Floor : " + GameManager.Instance.currentFloor);
        
        for (int i = 1; i < cutscenePanels.Length; i++)
        {
            cutscenePanels[i].SetActive(false);
        }
        

        /*canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        StartCoroutine(FadeInPanel());*/
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            ShowNextPanel();
            //StartCoroutine(FadeOutPanel());
        }

        if (currentPanelIndex == cutscenePanels.Length - 1){
            Debug.Log("Intro finished!");
            Invoke("ActionButton", activeTime);
            
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SelectStage()
    {
        GameManager.Instance.isCompletedIntro = true;
        SceneManager.LoadScene("SelectStage");
    }

    private void ActionButton()
    {
        continueButton.gameObject.SetActive(true);
        //BackButton.gameObject.SetActive(true);
    }
    
    /*IEnumerator FadeInPanel()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }

        // Enable the first panel
        cutscenePanels[currentPanelIndex].SetActive(true);
    }

    IEnumerator FadeOutPanel()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        // Disable the current panel
        cutscenePanels[currentPanelIndex].SetActive(false);

        // Increment the panel index
        currentPanelIndex++;

        // Check if there are more panels to show
        if (currentPanelIndex < cutscenePanels.Length)
        {
            // Enable the next panel with a fade-in effect
            StartCoroutine(FadeInPanel());
        }
        else
        {
            // All panels shown, you can perform an action or load the next scene here
            Debug.Log("Cutscene finished!");
        }
    }*/

    void ShowNextPanel()
    {
        if(currentPanelIndex < cutscenePanels.Length - 1){
            cutscenePanels[currentPanelIndex].SetActive(false);
        }
        currentPanelIndex++;
        if (currentPanelIndex < cutscenePanels.Length)
        {
            cutscenePanels[currentPanelIndex].SetActive(true);
        }
          
    }
    

}
