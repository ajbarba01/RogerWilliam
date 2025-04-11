using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject art;

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
