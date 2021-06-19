using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject grenadePre;

    public float throwForce = 500f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            Debug.Log("throwing");
            ThrowAGrenade();
        }
    }

    void ThrowAGrenade(){
        GameObject grenade = Instantiate(grenadePre, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce);
    }
}
