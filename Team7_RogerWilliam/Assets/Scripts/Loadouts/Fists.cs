using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Fists : Weapon {

      // public Animator animator;
      [SerializeField] public Transform attackPt;
      [SerializeField] public float attackRange = 0.5f;
      [SerializeField] public int attackDamage = 40;
      [SerializeField] public LayerMask enemyLayers;
      [SerializeField] public LayerMask playerLayers;
      [SerializeField] private AudioSource punchSFX;
      [SerializeField] private GameObject punchVFX;

      private float knockBackForce = 10f;

      private void Update() {
            if(targetTag == "Player")
            {
                  Vector3 direction = (target.position - transform.position).normalized;
                  Vector3 targetPosition = transform.position + direction * 1.5f;
                  attackPt.position = targetPosition;
            } else
            {
                  Vector3 targetPosition = transform.position + Util.TowardsMouse(transform.position) * 1f;
                  attackPt.position = targetPosition;
            }
            
      }

      public override void OnAttack() {
            if (punchSFX != null && !punchSFX.isPlaying){
                  punchSFX.Play();
            }
            if(targetTag == "Enemy")
            {
                  mover.FaceTowardsMouse(transform.position);
                  anim.PlayOnce("Player_Punch");
            }
            if(targetTag == "Enemy")
            {
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
                              Vector2 knockback = (Vector2)(player.transform.position - transform.position);
                              knockback.Normalize();
                              mover.ApplyKnockback(knockback, knockBackForce);
                        }
                  }
            }
            
            
      }
}