using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedPlayer = 5.0f;

    private Vector2 movement;
    private float guardMovementAnim = 0.001f;


    private Rigidbody2D rb;
    private Animator playerAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnMovement(InputValue value)
    {
        // Get the movement value
        movement = value.Get<Vector2>();
        Debug.Log("movement->" + movement);

        // Condition to keep last animation
        if (movement.x != 0)
        {
            // Update paramaters on the player animator for the movement
            playerAnimator.SetFloat("X", movement.x + guardMovementAnim);
            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            // Reset to idle
            playerAnimator.SetBool("IsWalking", false);
        }
    }

    private void OnBasicAttack(InputValue value)
    {
        Debug.Log("attack: " + value);
        playerAnimator.SetTrigger("IsAttacking");
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedPlayer * Time.fixedDeltaTime);
    }
}
