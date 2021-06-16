using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageScript : MonoBehaviour
{
    public float health = 50f;
    public GameObject coins;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Vector3 placement = gameObject.transform.localPosition;
            Quaternion rotation = gameObject.transform.localRotation;
            Destroy(gameObject);
            GameObject test = Instantiate(coins, placement, rotation);
            Debug.Log(test);
            if (gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("StartMenu");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
