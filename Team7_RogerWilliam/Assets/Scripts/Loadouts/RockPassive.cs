using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPassive : Passive
{
    [SerializeField] private Health health;

    private void Start()
    {
        if (health == null)
        {
            Debug.LogError("Health not assigned!");
            return;
        }

        float current = health.GetHealth();
        float max = health.GetMaxHealth();

        health.SetHealth(current * 2f);

        typeof(Health).GetField("maxHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(health, max * 2f);
    }
}

