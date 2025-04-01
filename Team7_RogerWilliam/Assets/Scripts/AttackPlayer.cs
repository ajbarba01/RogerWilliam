using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{

    Animator anim;
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

    private bool hasLOS;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
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
        
        if (distance > rangedAttackRange && hasLOS)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("WalkFront", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        } 
        else if (distance <= rangedAttackRange && distance > attackRange) 
        {
            anim.SetBool("Walk", false);
            anim.SetBool("WalkFront", false);
            // rangedAttackScript.ShootProjectile(); // Trigger the ranged attack (shoot projectile)
        }
        else // Melee attack
        {
            anim.SetBool("Walk", false);
            anim.SetBool("WalkFront", false);
            anim.SetBool("Attack", true);
            currEnemyAttack(); // Melee attack
            anim.SetBool("Attack", false);
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
                anim.SetTrigger("Attack");
                gameHandler.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }
    
    private void FixedUpdate() {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Player.GetPosition() - transform.position);
        if (ray.collider != null) {
            hasLOS = ray.collider.CompareTag("Player");
        }

        if (hasLOS) {
            Debug.DrawRay(transform.position, Player.GetPosition() - transform.position, Color.green);
        }
        else {
            Debug.DrawRay(transform.position, Player.GetPosition() - transform.position, Color.red);
        }
    }
}
