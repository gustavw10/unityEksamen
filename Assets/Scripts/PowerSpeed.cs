using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpeed : MonoBehaviour
{
    public GameObject pickupEffect;

    public float multiplier = 2f;
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
        Debug.Log("Power speed");
        Instantiate(pickupEffect, transform.position, transform.rotation);

        PlayerMovement playerMove = player.GetComponent<PlayerMovement>();
        playerMove.walkingSpeed *= multiplier;
        playerMove.runningSpeed *= multiplier;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerMove.walkingSpeed /= multiplier;
        playerMove.runningSpeed /= multiplier;

        Destroy(gameObject);
    }
}
