using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float stamina;
    public float sprintCost;
    public int status;
    public bool canMove;

    private Rigidbody2D rb;
    public Vector2 stunPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprintSpeed = 10f;
        speed = 5f;
        stamina = 100f;
        sprintCost = 10f;
        status = 10;
        canMove = true;
    }

    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        if (canMove)
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
        } else {
            transform.position = stunPosition;
        }
    }

    public void ApplyStun(int stunTime)
    {
        stunPosition = transform.position;
        StartCoroutine(StunCoroutine(stunTime));
    }

    private IEnumerator StunCoroutine(int stunTime)
    {
        canMove = false;
        // Implement the stun effect here, for example, by disabling movement or changing behavior
        Debug.Log("Enemy is stunned!");

        yield return new WaitForSeconds(stunTime);

        canMove = true;
        // Implement the recovery from stun here, for example, by enabling movement or restoring behavior
        Debug.Log("Enemy has recovered from stun!");
    }
}