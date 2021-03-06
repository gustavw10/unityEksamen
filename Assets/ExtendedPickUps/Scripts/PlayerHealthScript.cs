using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public RectTransform healthBar;
    public float healthBarWidthScale = 5;
    private float healthBarHeight = 54;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public bool CanHeal() {
        return currentHealth < maxHealth;
    }

    public void Heal(int amount) {
        currentHealth += amount;
        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        if(currentHealth < 0) {
            currentHealth = 0;
        }
        UpdateHealthBar();
        if(currentHealth == 0) {
            Die();
        }
    }

    private void UpdateHealthBar() {
        float newWidth = currentHealth * healthBarWidthScale;
        healthBar.sizeDelta = new Vector2(newWidth, healthBarHeight);
    }

    void Die() {
        //if(Application.isEditor) {
        //    UnityEditor.EditorApplication.isPlaying = false;
        //}
    }
}
