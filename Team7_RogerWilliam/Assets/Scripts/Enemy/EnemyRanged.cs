using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{

    [SerializeField] private float attackCooldown = 1f;
    // [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float scareDist = 3f;

    private RangedAttack rangedAttackScript;
    private EnemyChase enemyChase;
    private float attackChannel = 0f;

    private bool attacking = false;
    private bool running = false;
    private bool chasing = false;

    private float attackTimer;

    private void Awake() {
        rangedAttackScript = GetComponent<RangedAttack>();
        enemyChase = GetComponent<EnemyChase>();
    }

    private void Start() {
        enemyChase.SetDistance(attackRange);
        enemyChase.SetActive(false);
    }

    void Update()
    {
        if (attacking) {
            Attacking();
        }
        else if (running) {
            Running();
        }
        else if (chasing) {
            Chasing();
        }
        else {
            Idle();
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

    void Attacking() {
        if (!enemyChase.HasLOS()) {
            // IDLE
            attacking = false;
        }

        else if (enemyChase.Distance() <= scareDist) {
            // RUNNING
            attacking = false;
            running = true;
            enemyChase.SetActive(true);
            enemyChase.SetTowards(false);
        }

        else {
            attackChannel += Time.deltaTime;
            if (attackChannel >= attackCooldown) {
                // IDLE
                attacking = false;
                rangedAttackScript.ShootProjectile();
            }
        }
    }

    void Running() {
        if (!enemyChase.HasLOS() || enemyChase.Distance() > scareDist) {
            // IDLE
            running = false;
            enemyChase.SetActive(false);
            enemyChase.SetTowards(true);
        }
    }

    void Chasing() {
        if (!enemyChase.HasLOS()) {
            // IDLE
            chasing = false;
            enemyChase.SetActive(false);
        }

        else if (enemyChase.GetInDistance()) {
            // ATTACKING
            chasing = false;
            attacking = true;
            attackChannel = 0f;
            enemyChase.SetActive(false);
        }
    }

    void Idle() {
        if (enemyChase.HasLOS()) {
            if (enemyChase.Distance() <= scareDist) {
                // RUNNING
                running = true;
                enemyChase.SetActive(true);
                enemyChase.SetTowards(false);
            }
            else if (enemyChase.GetInDistance()) {
                // ATTACKING
                attacking = true;
                attackChannel = 0f;
            }
            else {
                // CHASING
                chasing = true;
                enemyChase.SetActive(true);
                enemyChase.SetTowards(true);
            }
        }
    }

    void ChannelAttack() {
        attackChannel += Time.deltaTime;
        if (attackChannel >= attackCooldown) {
            rangedAttackScript.ShootProjectile(); // Trigger the ranged attack (shoot projectile)
            attackChannel = 0;
            attacking = false;
        }
    }

    // protected virtual void currEnemyAttack()
    // {
    //     attackTimer -= Time.deltaTime;
    //     if (attackTimer <= 0)
    //     {
    //         attackTimer = attackCooldown;
    //         if (gameHandler != null)
    //         {
    //             Player.health.TakeDamage(attackDamage);
    //         }
    //     }
    // }
}
