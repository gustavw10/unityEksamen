using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int takeDamageAmount = 5;

    void Start()
    {
        currentHealth = maxHealth;
        InvokeRepeating("TakeDamage", 5.0f, 1.0f);
    }

    public void Heal(int amount) {
        currentHealth += amount;
        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }

    void TakeDamage() {
        currentHealth -= takeDamageAmount;
        if(currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
         UnityEditor.EditorApplication.isPlaying = false;
    }
}
