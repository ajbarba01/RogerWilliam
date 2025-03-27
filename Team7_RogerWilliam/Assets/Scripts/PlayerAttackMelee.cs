using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : Ability {

      // public Animator animator;
      [SerializeField] public Transform attackPt;
      [SerializeField] public float attackRange = 0.5f;
      [SerializeField] public float attackRate = 2f;
      [SerializeField] public int attackDamage = 40;
      [SerializeField] public LayerMask enemyLayers;

      private float nextAttackTime = 0f;

      public GameObject lastEnemyHit;

      void Start(){
           //animator = gameObject.GetComponentInChildren<Animator>();
      }

      void Update(){
            if (Input.GetAxis("Attack") > 0){
                  Activate();
            }
      }

      public override void Activate() {
            if (Time.time >= nextAttackTime) {
                  Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);

                  if (hitEnemies.Length != 0) {
                        lastEnemyHit = hitEnemies[0].gameObject;
                  }

                  foreach(Collider2D enemy in hitEnemies){
                        enemy.GetComponent<Health>().TakeDamage(attackDamage);
                  }

                  nextAttackTime = Time.time + 1f / attackRate;
            }
            //animator.SetTrigger ("Melee");
      }

      //NOTE: to help see the attack sphere in editor:
      void OnDrawGizmosSelected(){
           if (attackPt == null) {return;}
            Gizmos.DrawWireSphere(attackPt.position, attackRange);
      }
}