using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedPlayer = 5.0f;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedPlayer * Time.fixedDeltaTime);
    }
}
