using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Regular movement speed.
    public float sprintSpeed = 10f; // Sprinting movement speed.
    public float stamina = 100f; // Maximum stamina.
    public float sprintCost = 10f; // Stamina cost per second while sprinting.

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the WASD keys.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get input for sprinting.
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && stamina > 0;

        // Calculate the movement vector.
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Apply speed based on whether sprinting or not.
        float currentSpeed = isSprinting ? sprintSpeed : speed;
        rb.velocity = movement * currentSpeed;

        // Consume stamina while sprinting.
        if (isSprinting)
        {
            stamina -= sprintCost * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, 100f); // Ensure stamina stays within the range [0, 100].
        }
        else
        {
            // Regenerate stamina when not sprinting.
            stamina += Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, 100f); // Ensure stamina stays within the range [0, 100].
        }
    }
}