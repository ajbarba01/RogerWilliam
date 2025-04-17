using UnityEngine;

public class SalamiSpinner : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float aoeRange = 3f;
    public float aoeDamage = 20f;
    public float aoeCooldown = 2f;
    public float maxHealth = 100f;

    private float currentHealth;
    private float aoeTimer = 0f;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        aoeTimer -= Time.deltaTime;

        MoveTowardPlayer();

        if (Vector2.Distance(transform.position, player.position) <= aoeRange && aoeTimer <= 0f)
        {
            DoAOEDamage();
            aoeTimer = aoeCooldown;
        }
    }

    void MoveTowardPlayer()
    {
        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    void DoAOEDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, aoeRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Debug.Log("Player takes " + aoeDamage + " AoE damage!");
                // hit.GetComponent<PlayerHealth>()?.TakeDamage(aoeDamage);
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Enemy took " + damageAmount + " damage. Current HP: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoeRange);
    }
}