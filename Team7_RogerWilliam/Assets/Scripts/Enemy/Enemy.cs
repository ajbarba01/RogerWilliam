using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(DamageFlash))]
[RequireComponent(typeof(DeathEffectTrigger))]
[RequireComponent(typeof(DamagePopupTrigger))]
public class Enemy : MonoBehaviour
{
    private Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        health.onDeath.AddListener(OnDeath);
    }

    // Update is called once per frame
    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
