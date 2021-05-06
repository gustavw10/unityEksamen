using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerHealth : MonoBehaviour
{

    public GameObject pickupEffect;

    public float multiplier = 50f;
    public int duration = 10;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine( Pickup(other) );
        }
    }

    IEnumerator Pickup(Collider player)
    {
        Debug.Log("Power picked");
        Instantiate(pickupEffect, transform.position, transform.rotation);

        DamageScript playerHealth = player.GetComponent<DamageScript>();
        playerHealth.health += multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        if (playerHealth.health > 50)
        {
            playerHealth.health = 50;
        }

        Destroy(gameObject);
    }
}
