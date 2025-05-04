using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : Ability
{
    [SerializeField] private float splashArea;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float attackDamage;
    [SerializeField] public LayerMask enemyLayers;
    [SerializeField] public LayerMask playerLayers;

    [SerializeField] private GameObject VFX;
    private float VFXLength = 0.25f;

    private void Awake() {
        VFX.SetActive(false);
    }

    protected override void OnActivate() {

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, splashArea, enemyLayers);
        foreach (var enemy in hits) {
            AgentMover hitEnemy = enemy.GetComponent<AgentMover>();
            Health enemyHealth = enemy.GetComponent<Health>();
            
            // damage
            onEnemyHit.Invoke(enemyHealth);
            enemyHealth.TakeDamage(attackDamage);

            // knockback
            Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
            hitEnemy.ApplyKnockback(knockbackDirection, knockbackStrength);
        }
        anim.PlayOnce("Player_Punch");

        StartCoroutine(ShowVFX());
    }

    private IEnumerator ShowVFX() {
        VFX.SetActive(true);
        yield return new WaitForSeconds(VFXLength);
        VFX.SetActive(false);

    }
}
