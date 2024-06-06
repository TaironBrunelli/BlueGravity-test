using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_WateringCan : Item
{
    //-- Item Effect
    public Sprite WateringCan;
    public override void ItemEffect()
    {
        player_base.playerItem_1.GetComponent<SpriteRenderer>().sprite = WateringCan;
    }
}
