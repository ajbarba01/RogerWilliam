using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamiSpin : MonoBehaviour
{
     [SerializeField] private float radius = 3f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject aoeEffectPrefab;
    [SerializeField] public LayerMask enemyLayers;

    protected override void OnActivate() {
        mover.FaceTowardsMouse(transform.position);

        Vector3 aoePosition = transform.position; // Could change to mouse position if needed
        Instantiate(aoeEffectPrefab, aoePosition, Quaternion.identity);

        anim.PlayOnce("Player_Punch");

        Collider2D[] hits = Physics2D.OverlapCircleAll(aoePosition, radius, enemyLayers);
        foreach (var hit in hits) {
            hit.GetComponent<Health>()?.TakeDamage(damage);
        }
    }

    // Optional: visualize radius in editor
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
