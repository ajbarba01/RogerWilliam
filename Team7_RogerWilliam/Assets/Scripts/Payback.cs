using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payback : MonoBehaviour
{
    public float retaliationDamage = 5f;

    private Health playerHealth;

    void Start()
    {
        playerHealth = GetComponent<Health>();

        if (playerHealth == null)
        {
            Debug.LogError("No Health component found on the player!");
        }
    }

    // This method should be called whenever the player takes damage
    public void OnPlayerDamaged(float damageAmount, GameObject attacker)
    {
        if (attacker != null)
        {
            Health enemyHealth = attacker.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(retaliationDamage);
                Debug.Log($"Enemy {attacker.name} took {retaliationDamage} retaliation damage.");
            }
        }
    }
}
