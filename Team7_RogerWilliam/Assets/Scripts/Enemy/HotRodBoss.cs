using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotRodBoss : MonoBehaviour
{
    public float damage = 10f; // Amount of damage dealt per second
    public float zoneWidth = 3f; // Width of the damage zone
    public float zoneLength = 5f; // Length of the damage zone
    public LayerMask enemyLayer; // Define what is considered an enemy
    public float attackDuration = 10f; // Duration the zone stays active
    public GameObject fireEffect; // Assign the fire effect sprite in the Inspector
    private bool isAttacking = false;

    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float attackRange = 1f;

    [SerializeField] private float fireBreathCD = 10f;
    private float fireBreathTimer = 0f;
    // public Animator anim;
    private HotRodMovement enemyChase;
    private float attackChannel = 0f;
    private bool attacking = false;

    private float attackTimer;

    private void Awake() {
        enemyChase = GetComponent<HotRodMovement>();
        // anim = gameObject.GetComponent<Animator>();
    }

    private void Start() {
        enemyChase.SetDistance(attackRange);
    }

    void Update()
    {
        if (!enemyChase.HasLOS())
        {
            attacking = false;
            attackChannel = 0f;
            fireBreathTimer = 0f; // optional: reset when out of sight
            return;
        }

        // Update timers
        attackChannel += Time.deltaTime;
        fireBreathTimer += Time.deltaTime;

        bool inRange = enemyChase.GetInDistance();

        if (inRange)
        {
            enemyChase.SetActive(false); // Stop chasing to attack

            // Handle regular attack
            if (!attacking && attackChannel >= attackCooldown)
            {
                Attack();
                attackChannel = 0f;
            }

            // Handle fire breath
            if (!isAttacking && fireBreathTimer >= fireBreathCD)
            {
                StartCoroutine(ActivateDamageZone());
                fireBreathTimer = 0f;
            }
        }
        else
        {
            enemyChase.SetActive(true); // Resume chasing
            attacking = false;
        }
    }

    private IEnumerator FireBreathLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            if(!isAttacking)
            {
                StartCoroutine(ActivateDamageZone());
            }
        }
    }
    void ChannelAttack() {
        attackChannel += Time.deltaTime;
        if (attackChannel >= attackCooldown) {
            Attack();
            attackChannel = 0;
            attacking = false;
            enemyChase.SetActive(false);
        }
    }

    void Attack() {
        Debug.Log("Attacking");
        if (enemyChase.GetInDistance()) {
            // anim.SetBool("Attack", true);
            Player.health.TakeDamage(attackDamage);
            StartCoroutine(applyBurn(Player.health, 3f, 1f, 4f));
            // anim.SetBool("Attack", false);
        }
    }

    IEnumerator applyBurn(Health playerHealth, float duration, float interval, float damage)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            yield return new WaitForSeconds(interval);
            playerHealth.TakeDamage(damage);
            elapsed += interval;
        }
    }


     private System.Collections.IEnumerator ActivateDamageZone()
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
