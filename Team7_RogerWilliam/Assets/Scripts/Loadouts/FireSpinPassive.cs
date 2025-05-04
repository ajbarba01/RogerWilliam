using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpinPassive : MonoBehaviour
{
    [SerializeField] private float healthAddition = 50f;
    private Health health;

    private void Start()
    {
        health = Player.health;

        float newMax = health.GetMaxHealth() + healthAddition;

        health.SetMaxHealth(newMax);
        health.Heal(healthAddition);
    }

    private void OnDestroy() {
        float newMax = health.GetMaxHealth() - healthAddition;

        health.SetMaxHealth(newMax);
        health.TakeDamage(healthAddition);
    }
}
