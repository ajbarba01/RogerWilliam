using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{

    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 2f;

    private EnemyChase enemyChase;
    private float attackChannel = 0f;
    private bool attacking = false;

    private float attackTimer;

    private void Awake() {
        enemyChase = GetComponent<EnemyChase>();
    }

    private void Start() {
        enemyChase.SetDistance(attackRange);
    }

    void Update()
    {
        if (enemyChase.HasLOS()) {
            if (attacking) {
            ChannelAttack();
            }

            else if (enemyChase.GetInDistance()) {
                enemyChase.SetActive(false);
                attacking = true;
            }

            else {
                attacking = false;
                enemyChase.SetActive(true);
            }
        }
        else {
            attacking = false;
            attackChannel = 0f;
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
        if (enemyChase.GetInDistance()) {
            Player.health.TakeDamage(attackDamage);
        }
    }
}
