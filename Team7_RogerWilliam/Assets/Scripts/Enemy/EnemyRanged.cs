using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyChase))]
public class EnemyRanged : MonoBehaviour
{

    [SerializeField] private float attackCooldown = 1f;
    // [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float scareDist = 3f;

    private RangedAttack rangedAttackScript;
    private EnemyChase enemyChase;
    private float attackChannel = 0f;

    private enum enemyState {
        attacking,
        running,
        chasing,
        idle
    }

    private enemyState currentState;

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
        if (currentState == enemyState.attacking) {
            Attacking();
        }
        else if (currentState == enemyState.running) {
            Running();
        }
        else if (currentState == enemyState.chasing) {
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
            currentState = enemyState.idle;
        }

        else if (enemyChase.Distance() <= scareDist) {
            // RUNNING
            currentState = enemyState.running;
            enemyChase.SetActive(true);
            enemyChase.SetTowards(false);
        }

        else {
            attackChannel += Time.deltaTime;
            if (attackChannel >= attackCooldown) {
                // IDLE
                currentState = enemyState.idle;
                rangedAttackScript.ShootProjectile();
            }
        }
    }

    void Running() {
        if (!enemyChase.HasLOS() || enemyChase.Distance() > scareDist) {
            // IDLE
            currentState = enemyState.idle;
            enemyChase.SetActive(false);
            enemyChase.SetTowards(true);
        }
    }

    void Chasing() {
        if (!enemyChase.HasLOS()) {
            // IDLE
            currentState = enemyState.idle;
            enemyChase.SetActive(false);
        }

        else if (enemyChase.GetInDistance()) {
            // ATTACKING
            currentState = enemyState.attacking;
            attackChannel = 0f;
            enemyChase.SetActive(false);
        }
    }

    void Idle() {
        if (enemyChase.HasLOS()) {
            if (enemyChase.Distance() <= scareDist) {
                // RUNNING
                currentState = enemyState.running;
                enemyChase.SetActive(true);
                enemyChase.SetTowards(false);
            }
            else if (enemyChase.GetInDistance()) {
                // ATTACKING
                currentState = enemyState.attacking;
                attackChannel = 0f;
            }
            else {
                // CHASING
                currentState = enemyState.chasing;
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
            currentState = enemyState.idle;
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
