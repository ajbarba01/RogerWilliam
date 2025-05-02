using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sturdy : Passive
{
    private void Start()
    {
        Health health = Player.health;

        float current = health.GetHealth();
        float max = health.GetMaxHealth();

        health.SetHealth(current * 2f);

        typeof(Health).GetField("maxHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(health, max * 2f);
    }
}

