using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageScript : MonoBehaviour
{
    public float health = 50f;
    public GameObject coins;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Player")) {
            PlayerTakeDamage(amount);
        } else {
            health -= amount;
            if (health <= 0f)
            {
                Destroy(gameObject);
                InstantiateCoin();
            }
        }
    }

    private void InstantiateCoin() {
        Vector3 placement = gameObject.transform.localPosition;
        placement.y += .5f;
        Quaternion rotation = gameObject.transform.localRotation;
        Instantiate(coins, placement, rotation);
    }

    private void PlayerTakeDamage(float amount) {
        PlayerHealthScript player = gameObject.GetComponent<PlayerHealthScript>();
        player.TakeDamage((int) amount);
        if (player.currentHealth <= 0)
        {
            SceneManager.LoadScene("StartMenu");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    public void TakeDamage(float amount)
    {
        if(health > 0)
        {
            health -= amount;
        }
        
        Debug.Log(health);
        
        if (health - amount <= 0f){
            Debug.Log("inside part 2");
            //npcs 
            
            var rNumber = Random.Range(1,5);
            Invoke("DropCoin", 2);
            Destroy(gameObject, 2f);
            if(rNumber < 3){
                animator.Play("Take Damage");
            }
            else {
                animator.Play("Die");
            }
            
            Invoke("KillSwitch", 1);
            
            GetComponent<BeetleNPCScript>().enabled = false;
            // foreach(Collider col in GetComponents<CapsuleCollider>()){
            //     col.enabled = false;
            // }
            
        }
    }
    public void KillSwitch(){
            GetComponent<Animator>().enabled = false;
            
            // setRigidbodyState(false);
            // setColliderState(true);
    }

    public void DropCoin(){
        Vector3 placement = gameObject.transform.localPosition;
            Quaternion rotation = gameObject.transform.localRotation;
            //Destroy(gameObject);
            GameObject test = Instantiate(coins, placement, rotation);
            Debug.Log(test);
            if (gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("StartMenu");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
    }


    // public void setRigidbodyState(bool state){
    //     Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

    //     foreach(Rigidbody rigidbody in rigidbodies){
    //         rigidbody.isKinematic = state;
    //     }

    //     GetComponent<Rigidbody>().isKinematic = !state;
    // }

    //  public void setColliderState(bool state){
    //     Collider[] colliders = GetComponentsInChildren<Collider>();

    //     foreach(Collider collider in colliders){
    //         collider.enabled = state;
    //     }

    //     GetComponent<Collider>().enabled = !state;
    // }
}
