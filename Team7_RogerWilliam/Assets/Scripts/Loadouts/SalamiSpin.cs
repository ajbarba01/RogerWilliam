using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamiSpin : Ability
{
    [SerializeField] private float splashArea;
    [SerializeField] private float DPS;
    [SerializeField] public LayerMask enemyLayers;
    [SerializeField] public LayerMask playerLayers;

    [SerializeField] private GameObject VFX;
    private float VFXLength = 0.25f;

    private void Awake() {
        VFX.SetActive(false);
    }

    protected override void OnActivate() {
        anim.PlayOnce("Player_Punch");
        StartCoroutine(AbilityDuration());
    }

    private IEnumerator AbilityDuration() {
        VFX.SetActive(true);

        while (channeling) {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, splashArea, enemyLayers);
            foreach (var enemy in hits) {
                AgentMover hitEnemy = enemy.GetComponent<AgentMover>();
                Health enemyHealth = enemy.GetComponent<Health>();
                
                // damage
                onEnemyHit.Invoke(enemyHealth);
                enemyHealth.TakeDamage(DPS * Globals.TickRate);
            }
            yield return new WaitForSeconds(Globals.TickRate);
        }

        VFX.SetActive(false);
    }
}
