using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeetleNPCScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player;
    public float targetDistance;
    public float allowedDistance;
    public GameObject NPC;
    public float speed = 0.05f;
    private RaycastHit fire;

    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("MainPlayer").transform;
        print(player.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out fire)){
            targetDistance = fire.distance;
            if(targetDistance > 10){
               
            }
        }
        transform.LookAt(player);
        agent.SetDestination(player.position);
        
    }
}
