using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPassive : MonoBehaviour
{
    [SerializeField] private Health health;

    public void DoubleHealth()
    {
        if (health == null)
        {
            Debug.LogError("Health not assigned!");
            return;
        }

        float current = health.GetHealth();
        float max = health.GetMaxHealth();

        health.SetHealth(current * 2f);

        // Double the maxHealth field too
        typeof(Health).GetField("maxHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(health, max * 2f);
    }
}

