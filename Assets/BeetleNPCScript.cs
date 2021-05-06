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

    public Animator animator;

    private DamageScript target;

    float TimerForNextAttack, Cooldown;

    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("MainPlayer").transform;
        target = player.transform.GetComponent<DamageScript>();

        Cooldown = 2;
        TimerForNextAttack = Cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        agent.SetDestination(player.position);
        animator.SetBool("Run Forward", true);
        var distance = Vector3.Distance(player.position, transform.position);

         if (TimerForNextAttack > 0)
            {
            TimerForNextAttack  -= Time.deltaTime;
            }
            else if (TimerForNextAttack <=0 && distance < 4)
            {
            if(target != null){
                target.TakeDamage(5);
                animator.Play("Stab Attack");
                TimerForNextAttack = Cooldown;
            }
            
        }
    }
}
