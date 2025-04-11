using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Fists : Weapon {

      // public Animator animator;
      [SerializeField] public Transform attackPt;
      [SerializeField] public float attackRange = 0.5f;
      [SerializeField] public int attackDamage = 40;
      [SerializeField] public LayerMask enemyLayers;
      [SerializeField] private AudioSource punchSFX;
      [SerializeField] private GameObject punchVFX;

      private float knockBackForce = 15f;


      private void Awake() {
            cooldown = 0.5f;
      }

      private void Update() {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;

            Vector3 direction = (mouseWorld - transform.position).normalized;
            Vector3 targetPosition = transform.position + direction * 1f;

            attackPt.position = targetPosition;
      }

      void OnDrawGizmos()
      {
            if (attackPt != null)
            {
                  Gizmos.color = Color.red;
                  Gizmos.DrawWireSphere(attackPt.position, 1f);
            }
      }

      public override void OnAttack() {
            if (punchSFX != null && punchSFX.isPlaying == false){
                  punchSFX.Play();
            }
            
            Debug.Log(punchVFX == null);
            Destroy(Instantiate(punchVFX, attackPt.position, Quaternion.identity), 0.2f);
                              
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