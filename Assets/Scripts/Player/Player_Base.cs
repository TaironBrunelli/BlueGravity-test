using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Base : MonoBehaviour
{
    [Space]
    [Header("Infos")]
    //-- Infos
    private Player_Input input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;
    private float moveSpeed = 3f;

    [Space]
    [Header("Animation")]
    //-- Infos
    public Animator player_Animator;

    private void Awake()
    {
        input = new Player_Input();
        rb = GetComponent<Rigidbody2D>();
        player_Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
    }

    private void FixedUpdate()
    {
        player_Animator.SetFloat("Vertical", moveVector.y);
        player_Animator.SetFloat("Horizontal", moveVector.x);

        rb.velocity = moveVector * moveSpeed;
    }
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
}
