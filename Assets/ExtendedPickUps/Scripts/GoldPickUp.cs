using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : PickUpScript
{
    public int goldAmount = 1;

    public override void PickUp(Collider other) {
        other.GetComponent<PlayerInventoryScript>().AddGold(goldAmount);
    }
}
