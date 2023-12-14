using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player status")]
    public float speed;
    public float sprintSpeed;
    public float stamina;
    public float sprintCost;

    [Header("Trap status")]
    public float trapPlacementRadius;
    public float holdToIncreaseTrapTime;

    // New variable for trapMaterial
    private int trapMaterial = 0;

    public GameObject trapPrefab;
    public GameObject previewTrapPrefab;

    private Rigidbody2D rb;
    private bool isPlacingTrap;
    private float timeHoldingIncreaseKey;
    private int trapNumber;
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

    public Slider craftTime;
    public Slider staminaBar;

    public TextMeshProUGUI materials;
    public TextMeshProUGUI trap_C;
    public bool isReadableFilePanelOpen;
    private TowerManager towerManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprintSpeed = 6f;
        speed = 3f;
        stamina = 100f;
        sprintCost = 10f;
        trapPlacementRadius = 2.0f;
        holdToIncreaseTrapTime = 2f;
        //trapMaterial = 0;
        isPlacingTrap = false;
        timeHoldingIncreaseKey = 0f;
        trapNumber = 0;
        trapMaterial = 12;
        towerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
        currentTrapPanelNumber = Random.Range(0, towerManager.trapSetupPanelPrefabs.Count);
        isTrapPanelOpen = false;
        isTipTrickPanelOpen = false;
        isReadableFilePanelOpen = false;
        tipTrickPanelCooldown = false;
        tipTrickPanelCooldownTime = 0.5f;
        canvas = GameObject.Find("Canvas");
        canSeeEnemyStatus = true;
        isGrabByEnemy = false;
        materials.text = trapMaterial.ToString();
        trap_C.text = trapNumber.ToString();
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
            staminaBar.value = stamina;
        }
        else
        {
            stamina += Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, 100f);
            staminaBar.value = stamina;
        }
    }

    void CraftTrap()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            timeHoldingIncreaseKey += Time.deltaTime;
            craftTime.value = timeHoldingIncreaseKey * 0.5f;

            if (timeHoldingIncreaseKey >= holdToIncreaseTrapTime)
            {
                craftTime.value = 0;
                // Check if there are enough trapMaterial to craft a trap
                if (trapMaterial >= 3)
                {
                    trapNumber++;
                    trap_C.text = trapNumber.ToString();
                    Debug.Log("Trap number increased to " + trapNumber);

                    // Decrease trapMaterial by 3
                    trapMaterial -= 3;
                    materials.text = trapMaterial.ToString();
                    Debug.Log("trapMaterial remaining: " + trapMaterial);

                    timeHoldingIncreaseKey = 0f;
                }
                else
                {
                    Debug.Log("Not enough trapMaterial to craft a trap!");
                    craftTime.value = 0;
                }
            }
        }
        else
        {
            timeHoldingIncreaseKey = 0f;
            craftTime.value = 0;
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
                trapScript.trapSetupPanelPrefab = towerManager.trapSetupPanelPrefabs[currentTrapPanelNumber];
                if (currentTrapPanelNumber >= 3 && currentTrapPanelNumber <= 8)
                {
                    trapScript.extraNumber = Random.Range(5, 37);
                }
                currentTrapPanelNumber = Random.Range(0, towerManager.trapSetupPanelPrefabs.Count);
                trapNumber--;
                trap_C.text = trapNumber.ToString();
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
        if (Input.GetKey(KeyCode.T) && !isTrapPanelOpen && !isReadableFilePanelOpen && canvas != null && !tipTrickPanelCooldown)
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