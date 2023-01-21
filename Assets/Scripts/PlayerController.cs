using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedPlayer = 5.0f;

    private Vector2 movement;
    private float guardMovementAnim = 0.001f;

    private bool attackReady = true;
    private GameObject FXAttack;
    private Vector2 offsetFX;

    private Rigidbody2D rb;
    private Animator playerAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        FXAttack = GameObject.Find("FXAttack");
        FXAttack.SetActive(false);
    }

    private void OnMovement(InputValue value)
    {
        // Get the movement value
        movement = value.Get<Vector2>();

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


        // Set active FXAttack gameobject
        if (attackReady)
        {
            playerAnimator.SetTrigger("IsAttacking");
            Debug.Log("begin?");
            attackReady = false;
            FXAttack.SetActive(true);
            FXAttack.transform.position = gameObject.transform.position + new Vector3(movement.x * 1.5f, -1.0f, 0.0f);
        }
    }

    public void FinishAttack()
    {
        Debug.Log("Finish?");
        FXAttack.SetActive(false);
        attackReady = true;
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedPlayer * Time.fixedDeltaTime);
    }
}
