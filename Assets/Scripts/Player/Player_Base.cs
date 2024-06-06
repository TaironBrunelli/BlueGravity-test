/// <summary>
/// This is the Player Base Script:
/// - Player Movement;
/// - Set Player Animation;
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class Player_Base : MonoBehaviour
{
    [Space]
    [Header("Infos")]
    //-- Infos
    private int gold;
    public TMP_Text goldQuantity;
    private Player_Input input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;
    private float moveSpeed = 3f;

    [Space]
    [Header("Animation")]
    //-- Animation
    private Animator player_Animator;
    public GameObject playerHat;
    public GameObject playerItem_1;
    private Transform transformPlayerItem_1;
    private SpriteRenderer spritePlayerItem_1;
    public GameObject playerItem_2;
    private Transform transformPlayerItem_2;
    private SpriteRenderer spritePlayerItem_2;

    [Space]
    [Header("Shop")]
    //-- Shop
    private Shop _shop; //-- Used to get the NPC's Shop;
    public GameObject playerShopPanel; //-- Player Shop -> To sell items;
    public List<GameObject> playerInventory; //-- All Player items that can be sold.
    public GameObject buttonSellItem; //-- Prefab with the Button config to sell the item;
    public Transform sellItemTransform; //-- Get Item position;


    private void Awake()
    {
        input = new Player_Input();
        rb = GetComponent<Rigidbody2D>();
        player_Animator = GetComponent<Animator>();
        _shop = null;
        spritePlayerItem_1 = playerItem_1.GetComponent<SpriteRenderer>();
        spritePlayerItem_2 = playerItem_2.GetComponent<SpriteRenderer>();
        transformPlayerItem_1 = playerItem_1.GetComponent<Transform>();
        transformPlayerItem_2 = playerItem_2.GetComponent<Transform>();
    }

    private void Start()
    {
        playerShopPanel.SetActive(false);
        GetGold(999); //-- Start with 999 gold;
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

        #region "Sprite Position"
        //-- Change the sprites positions to the base position;
        transformPlayerItem_1.localPosition = new Vector2(0.5f, 0.2f);
        spritePlayerItem_1.sortingOrder = 11;
        spritePlayerItem_1.flipX = true;
        transformPlayerItem_2.localPosition = new Vector2(0.02f, 0.185f);
        spritePlayerItem_2.sortingOrder = 11;
        spritePlayerItem_2.flipX = true;
        //-- Move the sprites positions if the Player moves;
        //-- Moving Up
        if (moveVector.y > 0)
        {
            transformPlayerItem_1.localPosition = new Vector2(0.04f, 0.235f);
            spritePlayerItem_1.sortingOrder = 9;
            spritePlayerItem_1.flipX = false;
            transformPlayerItem_2.localPosition = new Vector2(0.5f, 0.25f);
            spritePlayerItem_2.sortingOrder = 9;
            spritePlayerItem_2.flipX = false;
        }
        //-- Moving Down
        if (moveVector.y < 0)
        {
            transformPlayerItem_1.localPosition = new Vector2(0.5f, 0.2f);
            spritePlayerItem_1.sortingOrder = 11;
            spritePlayerItem_1.flipX = true;
            transformPlayerItem_2.localPosition = new Vector2(0.02f, 0.185f);
            spritePlayerItem_2.sortingOrder = 11;
            spritePlayerItem_2.flipX = true;
        }
        //-- Moving Right
        if (moveVector.x > 0)
        {
            transformPlayerItem_1.localPosition = new Vector2(0.3f, 0.15f);
            spritePlayerItem_1.sortingOrder = 11;
            spritePlayerItem_1.flipX = true;
            transformPlayerItem_2.localPosition = new Vector2(0.35f, 0.3f);
            spritePlayerItem_2.sortingOrder = 9;
            spritePlayerItem_2.flipX = false;
        }
        //-- Moving Left
        if (moveVector.x < 0)
        {
            transformPlayerItem_1.localPosition = new Vector2(0.2f, 0.35f);
            spritePlayerItem_1.sortingOrder = 9;
            spritePlayerItem_1.flipX = false;
            transformPlayerItem_2.localPosition = new Vector2(0.15f, 0.2f);
            spritePlayerItem_2.sortingOrder = 11;
            spritePlayerItem_2.flipX = true;
        }

        #endregion "Sprite Position"

        rb.velocity = moveVector * moveSpeed;
    }

    #region "Input actions"
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    public void OnInteractionPerformed(InputAction.CallbackContext context)
    {
        if (_shop != null)
            _shop.OpenShop();
    }
    #endregion "Input actions"

    #region "Collider"
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
    #endregion "Collider"

    #region "Shop"
    public void OpenPlayerShop()
    {
        playerShopPanel.SetActive(true);
        SellItems(playerInventory);
    }

    public void OpenShopkeeperShop()
    {
        playerShopPanel.SetActive(false);
        if (_shop != null)
            _shop.OpenShop();
    }

    public void ClosePlayerShop()
    {
        playerShopPanel.SetActive(false);
    }
    #endregion "Shop"

    //-- Increment the Player's Gold
    public void GetGold(int i)
    {
        gold += i;
        goldQuantity.text = gold.ToString();
    }

    //-- Call when Enter in Player's Shop (Sell Button);
    public void SellItems(List<GameObject> items)
    {
        int x = 65;
        for (int i = 0; i < items.Count; i++) 
        {
            if (items[i].GetComponent<Item>().quantity > 0)
            {
                Instantiate(items[i], sellItemTransform.position, sellItemTransform.rotation, playerShopPanel.transform);
                //items[i].transform.position = new Vector2(-44, x);
                x -= 35;
                sellItemTransform.position += new Vector3(0, -35, 0);
            }
        }
    }
}
