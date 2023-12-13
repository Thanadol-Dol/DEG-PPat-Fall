using System.Collections;
using UnityEngine;
using TMPro;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int status;
    public TextMeshProUGUI textComponent;

    private AIDestinationSetter aIDestinationSetter;
    private AIPath aIPath;
    
    void Start()
    {
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        aIPath = GetComponent<AIPath>();
        aIPath.maxSpeed = 2.0f;
        aIDestinationSetter.target = GameObject.FindGameObjectWithTag("Player").transform;
        status = Random.Range(5, 37);
    }

    void Update()
    {
        aIDestinationSetter.target = GameObject.FindGameObjectWithTag("Player").transform;
        bool canPlayerSeeEnemyStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canSeeEnemyStatus;
        if(canPlayerSeeEnemyStatus)
        {
            // Update the text content (replace with your own logic)
            textComponent.text = status.ToString(); // Example content
        } else {
            textComponent.text = "---";
        }
    }

    public void ApplyStun(int stunTime)
    {
        Player playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(playerScript.currentLevel != 1)
        {
            status = Random.Range(5, 37);
        }
        StartCoroutine(StunCoroutine(stunTime));
    }

    private IEnumerator StunCoroutine(int stunTime)
    {
        aIPath.canMove = false;
        // Implement the stun effect here, for example, by disabling movement or changing behavior
        Debug.Log("Enemy is stunned!");

        yield return new WaitForSeconds(stunTime);

        aIPath.canMove = false;
        // Implement the recovery from stun here, for example, by enabling movement or restoring behavior
        Debug.Log("Enemy has recovered from stun!");
    }
}