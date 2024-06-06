using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_EggHat : Item
{
    //-- Item Effect
    public Sprite hatSprite;
    public override void ItemEffect()
    {
        player_base.playerHat.GetComponent<SpriteRenderer>().sprite = hatSprite;
    }
}
