using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        health.onDeath.AddListener(onDeath);
    }

    // Update is called once per frame
    private void onDeath()
    {
        Destroy(gameObject);
    }
}
