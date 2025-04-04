using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float attackRange = 2f;

    public Animator anim;
    private EnemyChase enemyChase;
    private float attackChannel = 0f;
    private bool attacking = false;

    private float attackTimer;

    private void Awake() {
        enemyChase = GetComponent<EnemyChase>();
        // anim = gameObject.GetComponent<Animator>();
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
            // anim.SetBool("Attack", true);
            Player.health.TakeDamage(attackDamage);
            StartCoroutine(applyBurn(Player.health, 5f, 1f, 2f));
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
}