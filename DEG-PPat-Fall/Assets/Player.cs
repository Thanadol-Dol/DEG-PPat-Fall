using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 10f;
    public float stamina = 100f;
    public float sprintCost = 10f;
    public float trapPlacementRadius = 2.0f;
    public float holdToIncreaseTrapTime = 2f;
    
    // New variable for trapMaterials
    public int trapMaterials = 0;
    
    public GameObject trapPrefab;
    public GameObject previewTrapPrefab;

    private Rigidbody2D rb;
    private bool isPlacingTrap = false;
    private bool isHoldingIncreaseKey = false;
    private float timeHoldingIncreaseKey = 0f;
    private int trapNumber = 0;
    private GameObject previewTrap;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trapMaterials = 12;
    }

    void Update()
    {
        PlayerMovement();
        CraftTrap();
        PlaceTrap();
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
                // Check if there are enough trapMaterials to craft a trap
                if (trapMaterials >= 3)
                {
                    trapNumber++;
                    Debug.Log("Trap number increased to " + trapNumber);

                    // Decrease trapMaterials by 3
                    trapMaterials -= 3;

                    Debug.Log("trapMaterials remaining: " + trapMaterials);

                    timeHoldingIncreaseKey = 0f;
                }
                else
                {
                    Debug.Log("Not enough trapMaterials to craft a trap!");
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

                Instantiate(trapPrefab, mousePosition, Quaternion.identity);
                trapNumber--;
            }
            else
            {
                Debug.Log("Not enough traps!");
            }
        }
    }
}