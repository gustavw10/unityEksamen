using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    public GameObject flagPrefab;
    private GameObject flag;

    void Start()
    {
        InstantiateFlagOnSpawnPoint(); 
    }
    
    private void InstantiateFlagOnSpawnPoint() {
        // Sets flag on spawnpoint as child of spawnpoint
        flag = Instantiate(flagPrefab, gameObject.transform, true);
        // Center flag on the spawnpoint
        flag.transform.localPosition = new Vector3(0, 0, 0);
        // Sets flag in a random rotation on Y
        flag.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    // Player picks up flag one at a time and flag gets removed from map
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerObjectivesScript player = other.GetComponent<PlayerObjectivesScript>();
            if (!player.hasFlag) {
                player.PickUpFlag();
                Destroy(flag);
            }
        }
    }
}
