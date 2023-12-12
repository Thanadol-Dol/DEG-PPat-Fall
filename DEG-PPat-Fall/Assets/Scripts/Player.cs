using System.Collections.Generic;
using System.Collections;
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

    public bool isTrapPanelOpen;
    public bool isTipTrickPanelOpen;

    public GameObject tipTrickPanel;
    public GameObject canvas;
    private bool tipTrickPanelCooldown;
    private float tipTrickPanelCooldownTime; // Adjust the cooldown time as needed

    public bool canSeeEnemyStatus;
    public bool isGrabByEnemy;
    public string currentTower;
    public string currentDifficulty;
    public int currentLevel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprintSpeed = 6f;
        speed = 3f;
        stamina = 100f;
        sprintCost = 10f;
        trapPlacementRadius = 2.0f;
        holdToIncreaseTrapTime = 2f;
        trapMaterial = 0;
        isPlacingTrap = false;
        timeHoldingIncreaseKey = 0f;
        trapNumber = 0;
        trapMaterial = 12;
        currentTrapPanelNumber = Random.Range(0, trapSetupPanelPrefabs.Count);
        isTrapPanelOpen = false;
        isTipTrickPanelOpen = false;
        tipTrickPanelCooldown = false;
        tipTrickPanelCooldownTime = 0.5f;
        canvas = GameObject.Find("Canvas");
        canSeeEnemyStatus = true;
        isGrabByEnemy = false;
        currentTower = "While";
        currentDifficulty = "Normal";
        currentLevel = 2;
    }

    void Update()
    {
        PlayerMovement();
        CraftTrap();
        PlaceTrap();
        ToggleTipTrickPanel();
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
                if (currentTrapPanelNumber >= 3 && currentTrapPanelNumber <= 8)
                {
                    trapScript.extraNumber = Random.Range(5, 37);
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

    public void CollectMaterial()
    {
        trapMaterial++;
        Debug.Log("Trap material: " + trapMaterial);
    }

    void ToggleTipTrickPanel()
    {
        if (Input.GetKey(KeyCode.T) && !isTrapPanelOpen && canvas != null && !tipTrickPanelCooldown)
        {
            if (!isTipTrickPanelOpen)
            {
                isTipTrickPanelOpen = true;
                Instantiate(tipTrickPanel, canvas.transform);
            }
            else
            {
                GameObject.FindGameObjectWithTag("TipTrickPanel").GetComponent<TipTrick>().CloseTipTrickPanel();
            }
            StartCoroutine(TipTrickPanelCooldown());
        }
    }

    IEnumerator TipTrickPanelCooldown()
    {
        tipTrickPanelCooldown = true;
        yield return new WaitForSeconds(tipTrickPanelCooldownTime);
        tipTrickPanelCooldown = false;
    }
}