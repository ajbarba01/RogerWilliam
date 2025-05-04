using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamiSpin : Ability
{
    [SerializeField] private float spinRange = 2f;        // Range of the spin attack
    [SerializeField] private float spinDuration = 1f;     // Duration of the spin attack
    [SerializeField] private float spinDamage = 5f;       // Damage dealt per hit
    [SerializeField] private float spinCooldown = 3f;     // Cooldown between spins
    [SerializeField] private float spinSpeed = 300f;      // Speed of the spin (degrees per second)

    [SerializeField] private LayerMask enemyLayers;      // Layers to detect enemies on
    [SerializeField] private LayerMask playerLayers;

    private float currentSpinTime = 0f;

    protected override void OnActivate()
    {
        // Activate the spin attack
        StartCoroutine(SpinAttack());
    }

    private IEnumerator SpinAttack()
    {
        if(targetTag == "Enemy")
        {
            anim.PlayOnce("Player_SpinAttack");  // Play the spin animation
        }
        
        float elapsedTime = 0f;

        // While the spin duration is not finished
        while (elapsedTime < spinDuration)
        {
            // Rotate the character (you can also rotate the character's weapon/attack hitbox)
            transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
            if(targetTag == "Enemy")
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, spinRange, enemyLayers);
                foreach (var hit in hits)
                {
                    // Only apply damage if it's an enemy with health
                    var health = hit.GetComponent<Health>();
                    if (health != null)
                    {
                        health.TakeDamage(spinDamage * Globals.deltaTick);
                    }
                }
            } else
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, spinRange, playerLayers);
                foreach (var hit in hits)
                {
                    // Only apply damage if it's an enemy with health
                    Player.health.TakeDamage(spinDamage);
                }

            }

            // Check for enemies in the spin's range and apply damage
            

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Cooldown before the next spin
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        // Prevent reactivation during cooldown
        onCooldown = true;
        currentSpinTime = 0f;

        while (currentSpinTime < spinCooldown)
        {
            currentSpinTime += Time.deltaTime;
            yield return null;
        }

        RefreshCooldown();
    }

    // Visual or functional feedback (optional)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spinRange); // Display the spin range in the editor
    }
}
