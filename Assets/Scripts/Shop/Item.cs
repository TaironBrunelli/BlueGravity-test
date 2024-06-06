/// <summary>
/// This is the Item Script:
/// - All Items must have Price and Quantity;
/// </summary>

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Item : MonoBehaviour
{
    public int price;
    public TMP_Text priceText;
    public int quantity;
    public TMP_Text quantityText;
    private GameObject player;
    protected Player_Base player_base; //-- Get the Script from the Player GameObject;
    private Button _button;
    public bool isSellable; //-- True if the Item can be sold -> Player items/Inventory;

    private void Awake()
    {
        if (price < 0) 
            price = 1; //-- Minimum value;
        player = GameObject.FindWithTag("Player");
        player_base = player.GetComponent<Player_Base>();
        priceText = transform.GetChild(0).GetComponent<TMP_Text>(); //-- First Child;
        quantityText = transform.GetChild(1).GetComponent<TMP_Text>(); //-- Second Child;
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        priceText.text = price.ToString();
        quantityText.text = quantity.ToString();
    }

    private void OnEnable()
    {
        if (isSellable)
            _button.onClick.AddListener(() => ItemCanSell());
        else
            _button.onClick.AddListener(() => ItemBuy());
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    //-- Butt function to sell the item;
    public void ItemCanSell()
    {
        quantity--;
        quantityText.text = quantity.ToString();
        player_base.GetGold(price); //-- Receive the Gold -> Price++;
        if (quantity == 0)
        {
            gameObject.SetActive(false);
        }
    }

    //-- Btt function to Buy the item;
    public void ItemBuy()
    {
        quantity--;
        quantityText.text = quantity.ToString();
        player_base.GetGold(-price); //-- Give the Gold -> Price--;
        ItemEffect(); //-- Call any Item Effect;
        if (quantity == 0)
        {
            gameObject.SetActive(false);
        }
    }

    //-- Item Effect
    public virtual void ItemEffect()
    {
        //-- Override to explicit Items;
    }
}
