using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipTrick : MonoBehaviour
{
    public GameObject topicButtonPrefab;
    public List<GameObject> contentPanelPrefab = new List<GameObject>();
    public Transform topicTransform;
    public Transform contentTransform;
    public GameObject newContentPanel;
    public Transform canvasTransform;
    public List<string> topicList = new List<string>();

    private void Start()
    {
        Time.timeScale = 0f;
        topicList = GameObject.Find("TowerManager").GetComponent<TowerManager>().topicList;
        foreach (string topic in topicList)
        {
            GameObject spawnedTopicButton = Instantiate(topicButtonPrefab, topicTransform);
            spawnedTopicButton.transform.Find("TopicName").GetComponent<TMPro.TextMeshProUGUI>().text = GameManager.Instance.topicConverter[topic];
            Button buttonComponent = spawnedTopicButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                string topicName = topic;
                buttonComponent.onClick.AddListener(() => OpenContentPanel(topicName));
            }
            else
            {
                Debug.LogError("Button Component not found");
            }
        }
    }

    public void CloseTipTrickPanel()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isTipTrickPanelOpen = false;
        GameObject[] panels = GameObject.FindGameObjectsWithTag("TipTrickPanel");
        Transform canvasTransform = GameObject.Find("Canvas").transform;
        foreach (GameObject panel in panels)
        {
            // Check if the object is a child of the canvas
            if (panel.transform.IsChildOf(canvasTransform))
            {
                Destroy(panel);
            }
        }
        Time.timeScale = 1f;
    }

    public void OpenContentPanel(string topic)
    {
        Debug.Log("OpenContentPanel called");
        Transform canvasTransform = GameObject.Find("Canvas").transform;
        if (newContentPanel != null)
        {
            Destroy(newContentPanel);
        }
        GameObject selectedPanel = GameManager.Instance.allReadableFileContent.Find(x => x.name == topic);
        Debug.Log(selectedPanel.name);
        newContentPanel = Instantiate(selectedPanel, canvasTransform);
    }
}