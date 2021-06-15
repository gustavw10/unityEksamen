using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float damage = 10f;
    public float range = 200f;

    public float fireRate = 15f;
    private float nextTimeToFire = 0f;
    public int maxAmmo = 12;
    private int currentAmmo = -1;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Text ammoDisplay;

    public Camera fpsCam;
    public Animator anim;

    public AudioSource fireSound;
    void Start()
    {
        anim = GetComponent<Animator>();
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = currentAmmo.ToString();

        if (isReloading)
            return;
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetButtonDown("Fire1") && currentAmmo > 0 && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            anim.Play("Shoot");
            ShootGun();
            fireSound.Play();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        anim.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void ShootGun() {

        currentAmmo--;

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            
            DamageScript target = hit.transform.GetComponent<DamageScript>();
            if(target != null){
                target.TakeDamage(damage);
            }
            print(target);
        }
    }
}
