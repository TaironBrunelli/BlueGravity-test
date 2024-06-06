using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Axe : Item
{
    //-- Item Effect
    public Sprite axeSprite;
    public override void ItemEffect()
    {
        player_base.playerItem_2.GetComponent<SpriteRenderer>().sprite = axeSprite;
    }
}
