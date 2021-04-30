using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float damage = 10f;
    public float range = 200f;
    public Camera fpsCam;
    private Animator anim;

    public AudioSource fireSound;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            anim.Play("Shoot");
            ShootGun();
            fireSound.Play();
        }
    }

    void ShootGun() {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            
            DamageScript target = hit.transform.GetComponent<DamageScript>();
            if(target != null){
                target.TakeDamage(damage);
            }
        }
    }
}
