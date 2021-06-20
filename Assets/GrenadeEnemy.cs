using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEnemy : MonoBehaviour
{
    private float delay = 8f;
    private float blast = 20f;
    public float grenadeDamage = 50;
   
    public ParticleSystem particleExplosion;
    float countdown;
    bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && !hasExploded){
            hasExploded = true;
            Explode();
        }
    }

    void Explode(){
        Instantiate(particleExplosion, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blast);

        foreach (Collider enemy in colliders){
            tag = enemy.transform.gameObject.tag;
            if(tag.Equals("Player")){
            DamageScript target = enemy.transform.GetComponent<DamageScript>();
            target.TakeDamage(grenadeDamage);
            }
            
        }
        Debug.Log("destroying");
        Destroy(gameObject);
        
    }
}