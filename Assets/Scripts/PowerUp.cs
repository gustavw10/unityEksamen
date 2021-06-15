using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGravity : MonoBehaviour
{

    public GameObject pickupEffect;

    public float multiplier = 1.9f;
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

        PlayerMovement playerMove = player.GetComponent<PlayerMovement>();
        playerMove.gravity /= multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerMove.gravity *= multiplier;

        Destroy(gameObject);
    }
}
