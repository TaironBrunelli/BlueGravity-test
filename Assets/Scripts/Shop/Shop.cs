/// <summary>
/// This is the ShopKeeper Script:
/// - Put on ShopKeeper to enable open a Shop canvas;
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject shopIcon;
    public GameObject canOpenShopImage;
    public Button firstButtonSelect;
    private Player_Base player;

    void Start()
    {
        shopPanel.SetActive(false);
        canOpenShopImage.SetActive(false);
        shopIcon.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D coll) // -- old version -> OnTriggerStay2D
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            canOpenShopImage.SetActive(true);
            shopIcon.SetActive(false);
            player = coll.GetComponent<Player_Base>(); //-- It could change every time to a new player if it has more players.
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            canOpenShopImage.SetActive(false);
            shopIcon.SetActive(true);
            CloseShop();
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        player.ClosePlayerShop();
    }

    public void OpenPlayerShop()
    {
        shopPanel.SetActive(false);
        player.OpenPlayerShop();
    }

    public void ClosePlayerShop()
    {
        shopPanel.SetActive(true);
        player.ClosePlayerShop();
    }
}
