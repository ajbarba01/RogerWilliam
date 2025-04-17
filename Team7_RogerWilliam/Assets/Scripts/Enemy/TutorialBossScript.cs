using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBossScript : MonoBehaviour
{
    protected GameObject gameHandler;
    protected GameObject player;

    protected Rigidbody2D rb2D;
    [SerializeField] private float moveSpeed = 2.5f;

    [SerializeField] private float attackRange = 4f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackDamage = 20f;

    [SerializeField] public float knockBack = 10f;
    private float distance;
    private float attackTimer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.Log("ERROR: Player not found");
        }
        rb2D = GetComponent<Rigidbody2D>();
        gameHandler = GameObject.FindWithTag("GameController");
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        
        if (distance > attackRange) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        } else 
        {
            BossAttack();
        }
    }

    protected virtual void BossAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = attackCooldown;

            if (gameHandler != null)
            {
                gameHandler.GetComponent<Health>().TakeDamage(attackDamage);
            }

            Vector2 knockbackDirection = player.transform.position - transform.position;
            knockbackDirection.Normalize();
            player.GetComponent<AgentMover>().ApplyKnockback(knockbackDirection, knockBack);

            Debug.Log("Knockback applied");
        }
    }
}
