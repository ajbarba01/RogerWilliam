using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : Ability
{
    public float damage = 10f; // Amount of damage dealt per second
    public float zoneWidth = 3f; // Width of the damage zone
    public float zoneLength = 5f; // Length of the damage zone
    // public LayerMask enemyLayer; // Define what is considered an enemy
    // public KeyCode attackKey;// Primary attack button
    public float attackDuration = 10f; // Duration the zone stays active
    public GameObject fireEffect; // Assign the fire effect sprite in the Inspector
    private bool isAttacking = false;

    void Start()
    {
        if (fireEffect != null)
        {
            fireEffect.SetActive(false); // Ensure the effect is hidden initially
        }
    }

    public override void Activate() {
        if (isAttacking) {
            return;
        }

        StartCoroutine(ActivateDamageZone());
    }

    private IEnumerator ActivateDamageZone()
    {
        isAttacking = true;
        if (fireEffect != null)
        {
            Debug.Log("Lebron Lebron Lebron");
            fireEffect.SetActive(true); // Show fire effect
        }

        float timer = 0f;
        while (timer < attackDuration)
        {
            DealDamageInZone();
            timer += Time.deltaTime;
            yield return null;
        }

        if (fireEffect != null)
        {
            fireEffect.SetActive(false); // Hide fire effect
        }
        isAttacking = false;
    }

    void DealDamageInZone()
    {
        Collider[] enemies = Physics.OverlapBox(transform.position + transform.forward * (zoneLength / 2),
                                                new Vector3(zoneWidth / 2, 1f, zoneLength / 2),
                                                transform.rotation, enemyLayer);
        
        foreach (Collider enemy in enemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage * Time.deltaTime);
                onEnemyHit.Invoke(enemyHealth);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward * (zoneLength / 2), transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(zoneWidth, 2f, zoneLength));
    }
    
}
