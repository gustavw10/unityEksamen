using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class PickUpScript : MonoBehaviour
{
    public string itemName;
    public GameObject prefab;
    public bool canRotate = true;
    public float randomRange = 50.0f;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        if(canRotate) {
            transform.Rotate(0, 0, 90 * Time.deltaTime);
        }
    }

    public abstract void PickUp(Collider other);

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player") {
            PickUp(other);
            InstantiateAndDestroy();
        }
    }

    private void InstantiateAndDestroy() {
        var position = new Vector3(Random.Range(-randomRange, randomRange), 1, Random.Range(-randomRange, randomRange));
        Instantiate(prefab, position, Quaternion.identity);
        Destroy(gameObject);
    }
}
