using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    private Health health;
    
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        health.onDeath.AddListener(onDeath);
    }

    // Update is called once per frame
    private void onDeath()
    {
        health.ResetHealth();
    }
}
