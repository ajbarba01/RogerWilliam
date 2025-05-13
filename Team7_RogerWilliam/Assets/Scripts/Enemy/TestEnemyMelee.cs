using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeleeAnimation))]
[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyChase))]
public class TestEnemyMelee : MonoBehaviour
{

    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 2f;

    private MeleeAnimation melee;

    private Animator anim;
    private EnemyChase enemyChase;
    private float attackChannel = 0f;
    private bool attacking = false;

    private float attackTimer;

    private void Awake() {
        melee = GetComponent<MeleeAnimation>();
        enemyChase = GetComponent<EnemyChase>();
        anim = gameObject.GetComponent<Animator>();        
    }

    private void Start() {
        enemyChase.SetDistance(attackRange);
        GetComponent<Health>().onDamage.AddListener(Damaged);
        melee.SetRadius(enemyChase.preferredDistance);
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
                melee.StopAttack();
                attacking = false;
                enemyChase.SetActive(true);
            }
        }
        else {
            attacking = false;
            attackChannel = 0f;
            melee.StopAttack();
        }
        
    }

    void ChannelAttack() {
        melee.StartAttack(enemyChase.preferredDistance, attackCooldown);

        attackChannel += Time.deltaTime;
        if (attackChannel >= attackCooldown) {
            Attack();
            // GetComponent<EnemyAttackInvoker>().StateAttack();
            attackChannel = 0;
            attacking = false;
            enemyChase.SetActive(false);
        }
    }

    void Attack() {
        melee.FinishAttack();
        if (enemyChase.GetInDistance()) {
            // anim.SetBool("Attack", true);
            Player.health.TakeDamage(attackDamage);
            // anim.SetBool("Attack", false);
        }
    }

    void Damaged() {
        if (attacking) {
            melee.StopAttack();
            attacking = false;
            attackChannel = 0f;
        }
    }
}
