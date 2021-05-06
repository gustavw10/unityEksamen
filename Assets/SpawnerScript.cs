using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject npcOne;
    public GameObject npcTwo;
    public bool stopSpawning = false;
    public float spawnTime = 5;
    public float spawnDelay = 5;
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject(){
        Instantiate(npcOne, transform.position, transform.rotation);
        if(stopSpawning){
            CancelInvoke("SpawnObject");
        }
    }
}
