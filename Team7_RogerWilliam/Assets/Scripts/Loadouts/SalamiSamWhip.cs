using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamiSamWhip : Weapon
{
    private int numAttacks = 3;
    private int currentAttack = 0;

    [SerializeField] public Transform attackPt;
    [SerializeField] public float attackRange = 0.5f;
    [SerializeField] public int attackDamage = 40;
    [SerializeField] public LayerMask enemyLayers;
    [SerializeField] public LayerMask playerLayers;
    [SerializeField] private float knockBackForce = 10f;

    [SerializeField] private GameObject[] swingVFX;

    private void Update() {
        if(targetTag == "Player")
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 targetPosition = transform.position + direction * 1.5f;
            attackPt.position = targetPosition;
        } else
        {
            Vector3 targetPosition = transform.position + Util.TowardsMouse(transform.position) * 1.5f;
            attackPt.position = targetPosition;
        }
        
    }

    public override void OnAttack() {
        if(targetTag == "Enemy")
        {
            mover.FaceTowardsMouse(transform.position);
            anim.PlayOnce("Player_Punch");
        }
        if(targetTag == "Enemy")
        {
            Quaternion rot = Util.QuaternionOfVector3(attackPt.localPosition, -90f);
            GameObject punch = Instantiate(GetVFX(), attackPt.position, rot);
            float animLength = punch.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - 0.05f;
            Destroy(punch, animLength);
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies) {
                // Damage
                Health enemyHealth = enemy.GetComponent<Health>();
                onEnemyHit.Invoke(enemyHealth);
                enemyHealth.TakeDamage(attackDamage);

                // Knockback
                AgentMover mover = enemy.GetComponent<AgentMover>();
                if (mover != null) {
                    Vector2 knockback = GetKnockbackDirection(enemy.transform);
                    knockback.Normalize();
                    mover.ApplyKnockback(knockback, knockBackForce);
                }
            }
        } else
        {
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPt.position, attackRange, playerLayers);
            foreach (Collider2D player in hitPlayers)
            {
                if(player.CompareTag("Player"))
                {
                    Player.health.TakeDamage(attackDamage);
                    AgentMover mover = player.GetComponent<AgentMover>();
                }
                
                if(mover != null)
                {
                    Vector2 knockback = GetKnockbackDirection(player.transform);
                    knockback.Normalize();
                    mover.ApplyKnockback(knockback, knockBackForce);
                }
            }
        }
        
        currentAttack = (currentAttack + 1) % numAttacks;
    }

    private Vector2 GetKnockbackDirection(Transform enemy) {
        Vector3 away = enemy.position - transform.position;
        if (currentAttack == 0) {
            return (Vector2)Vector3.Cross(away, Vector3.forward);
        }

        else if (currentAttack == 1) {
            return (Vector2)Vector3.Cross(away, Vector3.back);
        }

        else {
            return (Vector2)away;
        }
    }

    private GameObject GetVFX() {
        return swingVFX[currentAttack];
    }
    
}
