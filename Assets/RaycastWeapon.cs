using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{

    public bool isFiring = false;
    public int fireRate = 10;
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    public GameObject hitEffectEnemy;
    public TrailRenderer bulletTrace;
    public GameObject muzzle;

    float accumulatedTime;
    
    Ray ray;
    
    public Camera fpsCam;
    
    public float damage = 10f;
    public float range = 200f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            StartFiring();
        }
        if(Input.GetButtonUp("Fire1")){
            StopFiring();
        }
        if(isFiring){
            UpdateFiring(Time.deltaTime);
        }
    }

    public void UpdateFiring(float deltaTime){
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while(accumulatedTime >= 0.0f) {
            StartFiring();
            accumulatedTime -= fireInterval;
        }
    }

    public void StartFiring(){
        accumulatedTime = 0.0f;
        isFiring = true;
        muzzleFlash.Emit(1);

        RaycastHit hit;
        ray.origin = fpsCam.transform.position;
        ray.direction = fpsCam.transform.forward;

        // var bulletLine = Instantiate(bulletTrace, muzzle.transform.position, Quaternion.identity);
        // bulletLine.AddPosition(muzzle.transform.position);

        

            if (Physics.Raycast (ray, out hit))
            {
            
            // bulletLine.transform.position = hit.point;
            


            Debug.Log("firing");
            DamageScript target = hit.transform.GetComponent<DamageScript>();

            tag = hit.transform.gameObject.tag;
            if(!tag.Equals("Creep")){
                Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            if(target.transform.gameObject.tag.Equals("Creep")){
                 Instantiate(hitEffectEnemy, hit.point, Quaternion.LookRotation(hit.normal));
            } 
            
            if(target != null && !target.transform.gameObject.tag.Equals("Player")){
                 target.TakeDamage(damage);
            }
            
            GameObject bulletLine = ObjectPooler.SharedInstance.GetPooledObject();
            if(bulletLine != null){
            bulletLine.transform.position = hit.point;
            }
        }

    }

    public void StopFiring(){
        isFiring = false;
    }
}
