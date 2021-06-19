using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class PickUpScript : MonoBehaviour
{
    public string itemName;
    public GameObject prefab;
    public bool canRotate = true;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        if(canRotate) {
            transform.Rotate(0, 90 * Time.deltaTime, 0);
        }
    }

    public abstract void PickUp(Collider other);

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player") {
            PickUp(other);
        }
    }
}
