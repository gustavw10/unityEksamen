using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeetleNPCScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyGrenadePre;
    public Transform player;
    public float targetDistance;
    public float allowedDistance;
    public GameObject NPC;
    public float speed = 0.05f;
    private RaycastHit fire;

    public Animator animator;

    private DamageScript target;

    public int permittedAttackDistance = 4;

    float TimerForNextAttack, Cooldown;

    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("MainPlayer").transform;
        target = player.transform.GetComponent<DamageScript>();

        Cooldown = 2;
        TimerForNextAttack = Cooldown;

        if(gameObject.name.Equals("SmallDarkBomber(Clone)") ){
        StartCoroutine(ThrowEnemyBomb());
        }
    }

      IEnumerator ThrowEnemyBomb()
  {
          var distance = Vector3.Distance(player.position, transform.position);
          yield return new WaitForSeconds(3f);
          
            if(gameObject.name.Equals("SmallDarkBomber(Clone)") && distance < 100){
                
            //GameObject grenade = Instantiate(enemyGrenadePre, transform.position, transform.rotation);
            GameObject bomb = Instantiate(enemyGrenadePre, transform.position, transform.rotation);
            Debug.Log("TOSSING BOMB");
            var direction = player.position - transform.position;
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(direction * 50);
        }

          StartCoroutine(ThrowEnemyBomb());
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
            else if (TimerForNextAttack <=0 && distance < permittedAttackDistance)
            {
            if(target != null){
                target.TakeDamage(5);
                animator.Play("Stab Attack");
                TimerForNextAttack = Cooldown;
            }
            
        }
    }
}
