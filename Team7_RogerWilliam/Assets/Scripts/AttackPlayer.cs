using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    protected GameObject gameHandler;
    protected GameObject player;

    private GameObject floatingText;
    [SerializeField] private float minMoveSpeed = 1f;
    [SerializeField] private float maxMoveSpeed = 4f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float rangedAttackRange = 10f; // Ranged attack range
    [SerializeField] private RangedAttack rangedAttackScript; // Reference to RangedAttack script


    
    private float distance;
    private float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.Log("ERROR HERE");
        }
        gameHandler = GameObject.FindWithTag("GameController");
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        
        if (distance > rangedAttackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        } 
        else if (distance <= rangedAttackRange && distance > attackRange) 
        {
            rangedAttackScript.ShootProjectile(); // Trigger the ranged attack (shoot projectile)
        }
        else // Melee attack
        {
            currEnemyAttack(); // Melee attack
        }
        
        // ORIGINAL CODE BEFORE CHANGE _______

        // if (distance > attackRange) {
        //     transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        // } else 
        // {
        //     Debug.Log("MadeItToAttack");
        //     currEnemyAttack();
        // }
    }

    protected virtual void currEnemyAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = attackCooldown;
            if (gameHandler != null)
            {
                gameHandler.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }
}
