using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{

    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float scareDist = 3f;

    private RangedAttack rangedAttackScript;
    private EnemyChase enemyChase;
    private float attackChannel = 0f;
    private bool attacking = false;

    private float attackTimer;

    private void Awake() {
        rangedAttackScript = GetComponent<RangedAttack>();
        enemyChase = GetComponent<EnemyChase>();
    }

    private void Start() {
        enemyChase.SetDistance(attackRange);
    }

    void Update()
    {
        if (attacking) {
            enemyChase.SetActive(false);
            TryAttack();
        }

        else if (enemyChase.GetInDistance()) {
            attacking = true;
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

    void TryAttack() {
        attackChannel += Time.deltaTime;
        if (attackChannel >= attackCooldown) {
            rangedAttackScript.ShootProjectile(); // Trigger the ranged attack (shoot projectile)
            attackChannel = 0;
            attacking = false;
            enemyChase.SetActive(true);
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
