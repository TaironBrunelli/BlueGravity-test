/// <summary>
/// This is the Player Base Script:
/// - Player Movement;
/// - Set Player Animation;
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    [Space]
    [Header("Shop")]
    //-- Infos
    public Shop _shop;

    private void Awake()
    {
        input = new Player_Input();
        rb = GetComponent<Rigidbody2D>();
        player_Animator = GetComponent<Animator>();
        _shop = null;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.Interaction.performed += OnInteractionPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
        input.Player.Interaction.performed -= OnInteractionPerformed;
    }

    private void FixedUpdate()
    {
        player_Animator.SetFloat("Vertical", moveVector.y); //-- Animator value -> Change Animation based on moveVector value;
        player_Animator.SetFloat("Horizontal", moveVector.x); //-- Animator value -> Change Animation based on moveVector value;

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

    void OnTriggerEnter2D(Collider2D coll) // -- old version -> OnTriggerStay2D
    {
        if (coll.gameObject.CompareTag("Shopkeeper"))
        {
            _shop = coll.gameObject.GetComponent<Shop>();
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Shopkeeper"))
        {
            _shop = null;
        }
    }

    public void OnInteractionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("SHOP E");
        if(_shop != null)
            _shop.OpenShop();
    }
}
