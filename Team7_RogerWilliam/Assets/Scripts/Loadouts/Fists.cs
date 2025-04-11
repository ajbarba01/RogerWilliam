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

      private float knockBackForce = 10f;


      private void Awake() {
            cooldown = 0.5f;
      }

      private void Update() {
            Vector3 targetPosition = transform.position + Util.TowardsMouse(transform.position) * 1f;

            attackPt.position = targetPosition;
      }

      public override void OnAttack() {
            if (punchSFX != null && punchSFX.isPlaying == false){
                  punchSFX.Play();
            }
            
            Quaternion rot = Util.QuaternionOfVector3(attackPt.localPosition, -90f);
            GameObject punch = Instantiate(punchVFX, attackPt.position, rot);
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
                        Vector2 knockback = (Vector2)(enemy.transform.position - transform.position);
                        knockback.Normalize();
                        mover.ApplyKnockback(knockback, knockBackForce);
                  }
            }
      }
}