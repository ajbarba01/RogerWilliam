using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public static float health;

    void Start(){
        ResetHealth();
    }

    void ResetHealth() {
        health = maxHealth;
    }

    void TakeDamage(float damage) {
        health -= damage;
        if (health < 0) {
            health = 0;
            Die();
        }
    }

    void heal(float value) {
        health += value;
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    void Die() {
        Debug.Log("PLAYER DEAD");
    }
}
