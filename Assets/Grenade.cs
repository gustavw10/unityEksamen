using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;
    public float blast = 8f;
    public float grenadeDamage = 100;
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
        Debug.Log("got inside");

        Collider[] colliders = Physics.OverlapSphere(transform.position, blast);

        foreach (Collider enemy in colliders){
            tag = enemy.transform.gameObject.tag;
            if(tag.Equals("Creep")){
            print(tag);
            DamageScript target = enemy.transform.GetComponent<DamageScript>();
            target.TakeDamage(grenadeDamage);
            }
            
            
        }
        
        Destroy(gameObject);
    }
}
