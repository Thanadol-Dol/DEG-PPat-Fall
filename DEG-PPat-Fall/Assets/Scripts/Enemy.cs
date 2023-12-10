using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float stamina;
    public float sprintCost;
    public int status;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprintSpeed = 10f;
        speed = 5f;
        stamina = 100f;
        sprintCost = 10f;
        status = 10;
    }

    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.RightShift) && stamina > 0;

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
}