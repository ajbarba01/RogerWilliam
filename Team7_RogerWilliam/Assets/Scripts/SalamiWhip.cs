using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamiWhip : MonoBehaviour
{
    public float whipRange = 5f;
    public float whipDamage = 25f;
    public float whipCooldown = 1f;
    public LayerMask enemyLayer;
    public Transform whipOrigin;

    private float lastWhipTime = -Mathf.Infinity;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastWhipTime + whipCooldown)
        {
            UseWhip();
            lastWhipTime = Time.time;
        }
    }

    void UseWhip()
    {
        // Raycast forward from whip origin
        RaycastHit[] hits = Physics.SphereCastAll(whipOrigin.position, 1f, whipOrigin.forward, whipRange, enemyLayer);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log("Whipped: " + hit.collider.name);

            var enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(whipDamage);
            }
        }

        // You can add animations/sound here
        // e.g., animator.SetTrigger("Whip");
    }

    void OnDrawGizmosSelected()
    {
        if (whipOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(whipOrigin.position, whipOrigin.forward * whipRange);
            Gizmos.DrawWireSphere(whipOrigin.position + whipOrigin.forward * whipRange, 1f);
        }
    }
}
