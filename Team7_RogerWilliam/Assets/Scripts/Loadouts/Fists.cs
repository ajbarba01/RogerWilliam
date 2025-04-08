using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Fists : Weapon {

      // public Animator animator;
      [SerializeField] public Transform attackPt;
      [SerializeField] public float attackRange = 0.5f;
      [SerializeField] public float attackRate = 2f;
      [SerializeField] public int attackDamage = 40;
      [SerializeField] public LayerMask enemyLayers;

      private float nextAttackTime = 0f;

      public GameObject lastEnemyHit;

      public AudioSource punchSFX;

      public override void Attack() {
            if (Time.time >= nextAttackTime) {
                  if (punchSFX != null && punchSFX.isPlaying == false){
                        punchSFX.Play();
                  }
                                    
                  Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

                  foreach (Collider2D enemy in hitEnemies) {
                        Health enemyHealth = enemy.GetComponent<Health>();
                        onEnemyHit.Invoke(enemyHealth);
                        enemyHealth.TakeDamage(attackDamage);
                  }

                  nextAttackTime = Time.time + 1f / attackRate;
            }
      }
}