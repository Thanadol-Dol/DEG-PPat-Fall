using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipTrick : MonoBehaviour
{
    public GameObject topicButtonPrefab;
    public GameObject contentPanelPrefab;
    public Transform topicTransform;
    public Transform contentTransform;
    public List<string> topicList = new List<string>();

    private void Start()
    {
        topicList.Add("1");
        topicList.Add("2");
        topicList.Add("3");

        foreach (string topic in topicList)
        {
            GameObject topicButton = Instantiate(topicButtonPrefab, topicTransform);
            GameObject newContentPanel = Instantiate(contentPanelPrefab, contentTransform);
            topicButton.transform.Find("TopicName").GetComponent<TMPro.TextMeshProUGUI>().text = topic;
        }
    }

    public void CloseTipTrickPanel()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isTipTrickPanelOpen = false;
        Destroy(this.gameObject);
    }
}
