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
    private bool isOnShop;

    void Start()
    {
        shopPanel.SetActive(false);
        canOpenShopImage.SetActive(false);
        isOnShop = false;
        shopIcon.SetActive(true);
    }

    void Update()
    {
      /*  if (InputSuper_Script.AttackSuperButton())
            OpenShop(isOnShop);
        if ((Input.GetKeyDown(InputManager_Script.GM.BButton)) && isOnShop)
            CloseShop();*/
    }

    void OnTriggerEnter2D(Collider2D coll) // -- old version -> OnTriggerStay2D
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isOnShop = true;
            canOpenShopImage.SetActive(true);
            shopIcon.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            isOnShop = false;
            canOpenShopImage.SetActive(false);
            shopIcon.SetActive(true);
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }
}
