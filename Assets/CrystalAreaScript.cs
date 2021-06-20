using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalAreaScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private DamageScript target;

    float timer = 0;
    private float damageTime = 0.2f;
    private float damageAmount = 10;
    void Start()
    {
        player = GameObject.Find("MainPlayer").transform;
        target = player.transform.GetComponent<DamageScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider _collision){
        if(_collision.gameObject.tag == "Player"){
            
             if(timer >= damageTime)
         {
             timer -= damageTime;
            target.TakeDamage(damageAmount);
            Debug.Log("Entered aoe");
         }
         timer += Time.deltaTime;
        }
    }
}
