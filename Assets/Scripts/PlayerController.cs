using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedPlayer = 5.0f;

    private Vector2 movement;
    private float saveDirection = 1;
    private float guardMovementAnim = 0.001f;

    private bool attackReady = true;
    private GameObject FXAttack;
    private Vector3 offsetFX;

    private Rigidbody2D rb;
    private Animator playerAnimator;

    private void Awake()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        // Inactive FX attack at first
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
            // Save Direction for attack FX as [-1 or 1]
            if (movement.x < 0)
            {
                // [-1] : Left direction
                saveDirection = Mathf.Floor(movement.x);
            } else
            {
                // [1] : Right direction
                saveDirection = Mathf.Ceil(movement.x);
            }

            // Update paramaters on the player animator for the movement
            playerAnimator.SetFloat("X", movement.x + guardMovementAnim);
            // Walk animation
            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            // Idle animation
            playerAnimator.SetBool("IsWalking", false);
        }
    }

    private void OnBasicAttack(InputValue value)
    {
        // Condition to avoid spam attack
        if (attackReady)
        {
            // Attack animation
            playerAnimator.SetTrigger("IsAttacking");
            attackReady = false;

            // Set active FXAttack gameobject containing the FX related to the weapon
            FXAttack.SetActive(true);

            // Compute the FX Offset and place it where the player attacked
            offsetFX = new Vector3(saveDirection * 1.2f, -1.0f, 0.0f);
            FXAttack.transform.position = gameObject.transform.position + offsetFX;
        }
    }

    // Unity Event call by animations [Player_AttackHand] @frame 63
    public void FinishAttack()
    {
        // Set inactive FXAttack gameobject and enable player basic attack
        FXAttack.SetActive(false);
        attackReady = true;
    }


    private void FixedUpdate()
    {
        // Update the player position according to his movement and his speed
        rb.MovePosition(rb.position + movement * speedPlayer * Time.fixedDeltaTime);
    }
}
