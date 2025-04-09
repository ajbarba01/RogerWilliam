using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : Ability
{
    public float damage = 10f; // Amount of damage dealt per second
    public float zoneWidth = 3f; // Width of the damage zone
    public float zoneLength = 5f; // Length of the damage zone
    public LayerMask enemyLayer; // Define what is considered an enemy
    private float attackDuration = 5f; // Duration the zone stays active
    public GameObject fireEffect; // Assign the fire effect sprite in the Inspector
    private bool isAttacking = false;

    private Collider2D[] enemyHits = new Collider2D[20];

    private void Awake() {
        cooldown = 15f;
    }

    void Start()
    {
        if (fireEffect != null)
        {
            fireEffect.SetActive(false); // Ensure the effect is hidden initially
        }
    }

    public override void OnActivate() {
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
            fireEffect.SetActive(true);
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
        int hitCount = DetectEnemies();
        for (int i = 0; i < hitCount; i++)
        {
            Collider2D enemyCollider = enemyHits[i];
            Health enemyHealth = enemyCollider.GetComponent<Health>();
            enemyHealth.TakeDamage(damage * Time.deltaTime);
            onEnemyHit.Invoke(enemyHealth);
        }

        // Collider[] enemies = Physics.OverlapBox(transform.position + transform.forward * (zoneLength / 2),
        //                                         new Vector3(zoneWidth / 2, 1f, zoneLength / 2),
        //                                         transform.rotation, enemyLayer);

        // foreach (Collider2D enemy in enemies)
        // {
        //     Health enemyHealth = enemy.GetComponent<Health>();
        //     if (enemyHealth != null)
        //     {
        //         Debug.Log("ENEMY DAMAGE");
        //         enemyHealth.TakeDamage(damage * Time.deltaTime);
        //         onEnemyHit.Invoke(enemyHealth);
        //     }
        // }
    }

    private int DetectEnemies()
    {
        Vector2 center = (Vector2)(transform.position + transform.forward * (zoneLength / 2));
        Vector2 halfExtents = new Vector3(zoneWidth / 2, zoneLength / 2);

        int hitCount = Physics2D.OverlapBoxNonAlloc(
        center,
        halfExtents,
        0,
        enemyHits,
        enemyLayer
        );

        return hitCount;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward * (zoneLength / 2), transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(zoneWidth, 2f, zoneLength));
    }
    
}
