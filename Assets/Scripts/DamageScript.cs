using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageScript : MonoBehaviour
{
    public float health = 50f;
    public GameObject coins;

    public void TakeDamage(float amount)
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
}
