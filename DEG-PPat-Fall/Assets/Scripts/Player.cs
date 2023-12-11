using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float stamina;
    public float sprintCost;
    public float trapPlacementRadius;
    public float holdToIncreaseTrapTime;
    
    // New variable for trapMaterial
    private int trapMaterial;
    
    public GameObject trapPrefab;
    public GameObject previewTrapPrefab;

    private Rigidbody2D rb;
    private bool isPlacingTrap;
    private float timeHoldingIncreaseKey;
    private int trapNumber;
    public List<GameObject> trapSetupPanelPrefabs = new List<GameObject>();
    private int currentTrapPanelNumber;
    private GameObject previewTrap;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprintSpeed = 10f;
        speed = 5f;
        stamina = 100f;
        sprintCost = 10f;
        trapPlacementRadius = 2.0f;
        holdToIncreaseTrapTime = 2f;
        trapMaterial = 0;
        isPlacingTrap = false;
        timeHoldingIncreaseKey = 0f;
        trapNumber = 0;
        trapMaterial = 12;
        trapNumber = 1;
        currentTrapPanelNumber = Random.Range(0, trapSetupPanelPrefabs.Count);
    }

    void Update()
    {
        PlayerMovement();
        CraftTrap();
        PlaceTrap();
        CheckDistanceToTraps();
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && stamina > 0;

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        float currentSpeed = isSprinting ? sprintSpeed : speed;
        rb.velocity = movement * currentSpeed;

        if (isSprinting)
        {
            stamina -= sprintCost * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, 100f);
        }
        else
        {
            stamina += Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, 100f);
        }
    }

    void CraftTrap()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            timeHoldingIncreaseKey += Time.deltaTime;

            if (timeHoldingIncreaseKey >= holdToIncreaseTrapTime)
            {
                // Check if there are enough trapMaterial to craft a trap
                if (trapMaterial >= 3)
                {
                    trapNumber++;
                    Debug.Log("Trap number increased to " + trapNumber);

                    // Decrease trapMaterial by 3
                    trapMaterial -= 3;

                    Debug.Log("trapMaterial remaining: " + trapMaterial);

                    timeHoldingIncreaseKey = 0f;
                }
                else
                {
                    Debug.Log("Not enough trapMaterial to craft a trap!");
                }
            }
        }
        else
        {
            timeHoldingIncreaseKey = 0f;
        }
    }

    void PlaceTrap()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!isPlacingTrap)
            {
                isPlacingTrap = true;
                previewTrap = Instantiate(previewTrapPrefab, transform.position, Quaternion.identity);
            }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPosition = transform.position;
            float distance = Vector2.Distance(playerPosition, mousePosition);

            if (distance > trapPlacementRadius)
            {
                mousePosition = playerPosition + (mousePosition - playerPosition).normalized * trapPlacementRadius;
            }

            previewTrap.transform.position = mousePosition;
        }
        else if (isPlacingTrap)
        {
            isPlacingTrap = false;
            Destroy(previewTrap);

            if (trapNumber > 0)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 playerPosition = transform.position;

                float distance = Vector2.Distance(playerPosition, mousePosition);

                if (distance > trapPlacementRadius)
                {
                    mousePosition = playerPosition + (mousePosition - playerPosition).normalized * trapPlacementRadius;
                }

                GameObject trap = Instantiate(trapPrefab, mousePosition, Quaternion.identity);
                Trap trapScript = trap.GetComponent<Trap>();
                trapScript.puzzleNumber = currentTrapPanelNumber;
                trapScript.trapSetupPanelPrefab = trapSetupPanelPrefabs[currentTrapPanelNumber];
                if(currentTrapPanelNumber>=3 && currentTrapPanelNumber<=8){
                    trapScript.extraNumber = Random.Range(5, 36);
                }
                currentTrapPanelNumber = Random.Range(0, trapSetupPanelPrefabs.Count);
                trapNumber--;
            }
            else
            {
                Debug.Log("Not enough traps!");
            }
        }
    }

    public void CollectMaterial(){
        trapMaterial++;
        Debug.Log("Trap material: " + trapMaterial);
    }

    void CheckDistanceToTraps()
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap"); // Assuming traps have the "Trap" tag

        foreach (GameObject trap in traps)
        {
            float distance = Vector2.Distance(transform.position, trap.transform.position);

            if (distance > 2f)
            {
                // Close panels associated with the trap
                CloseTrapPanels();
            }
        }
    }

    void CloseTrapPanels()
    {
        GameObject[] panels = GameObject.FindGameObjectsWithTag("TrapSetupPanel"); // Assuming trap setup panels have the "TrapSetupPanel" tag
        GameObject canvas = GameObject.Find("Canvas");

        // Iterate through the found objects
        foreach (GameObject panel in panels)
        {
            // Check if the object is a child of the canvas
            if (panel.transform.IsChildOf(canvas.transform))
            {
                Destroy(panel);
            }
        }
    }
}