using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUpScript
{
    public int healAmount = 25;

    public override void PickUp(Collider other) {
        PlayerHealthScript player = other.GetComponent<PlayerHealthScript>();
        if (player.CanHeal()) {
            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
