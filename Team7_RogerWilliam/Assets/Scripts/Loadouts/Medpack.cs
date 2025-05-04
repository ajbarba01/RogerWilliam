using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medpack : Ability
{

    [SerializeField] private float healAmount;
    [SerializeField] private float initialVelocity, flyDuration, spawnOffset;

    [SerializeField] private GameObject healthPickupPrefab;

    protected override void OnActivate() {
        GameObject pickup = Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        HealthPickup healthPickup = pickup.GetComponent<HealthPickup>();
        healthPickup.SetHeal(healAmount);
        StartCoroutine(EnableAfterLand(pickup));
    }

    private IEnumerator EnableAfterLand(GameObject pickup) {
        Collider2D collider = pickup.GetComponent<Collider2D>();
        collider.enabled = false;

        Rigidbody2D rb = pickup.GetComponent<Rigidbody2D>();

        Vector2 randDir = Random.insideUnitCircle.normalized;

        pickup.transform.position = transform.position + (Vector3)(randDir * spawnOffset);

        float flyProgress = 0f;
        while (flyProgress < flyDuration) {
            float vel = initialVelocity * Mathf.Lerp(1, 0, flyProgress / flyDuration);
            rb.velocity = randDir * vel;
            
            flyProgress += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;

        collider.enabled = true;
    }
}
