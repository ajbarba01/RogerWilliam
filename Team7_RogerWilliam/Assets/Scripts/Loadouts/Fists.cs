using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Fists : Weapon {

      // public Animator animator;
      [SerializeField] public Transform attackPt;
      [SerializeField] public float attackRange = 0.5f;
      [SerializeField] public int attackDamage = 40;
      [SerializeField] public LayerMask enemyLayers;

      private float knockBackForce = 10f;

      public AudioSource punchSFX;

      private void Awake() {
            cooldown = 0.5f;
      }

      public override void OnAttack() {
            if (punchSFX != null && punchSFX.isPlaying == false){
                  punchSFX.Play();
            }
                              
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies) {

                  // Damage
                  Health enemyHealth = enemy.GetComponent<Health>();
                  onEnemyHit.Invoke(enemyHealth);
                  enemyHealth.TakeDamage(attackDamage);

                  // Knockback
                  AgentMover mover = enemy.GetComponent<AgentMover>();
                  if (mover != null) {
                        Vector2 knockback = (Vector2)(enemy.transform.position - transform.position);
                        knockback.Normalize();
                        mover.ApplyKnockback(knockback, knockBackForce);
                  }
            }
      }
}