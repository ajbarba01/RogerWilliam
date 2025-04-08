using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public static float staticHealth;

    public override void SetHealth(float newHealth) {
        health = newHealth;
        staticHealth = newHealth;
    }

    private void Update() {
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     TakeDamage(10);
        // }

        // if (Input.GetKeyDown(KeyCode.Return)) {
        //     ResetHealth();
        // }
    }
}
