using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovCocktail : Ability
{
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject molotovProjectile;
    [SerializeField] private GameObject fire;
    [SerializeField] private float fireDuration = 2f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float dps = 3f;
    [SerializeField] private float radius = 1.5f;
    [SerializeField] public LayerMask enemyLayers;

    private float damagePerTick;

    private void Awake() {
        damagePerTick = dps * Globals.TickRate;
    }

    protected override void OnActivate() {
        mover.FaceTowardsMouse(transform.position);

        Vector3 dist = Util.DistToMouse(transform.position);
        if (dist.magnitude > attackRange) {
            dist = dist.normalized * attackRange;
        }

        Vector3 target = transform.position + dist;

        Vector3 initialPos = transform.position + dist.normalized * 0.5f;
        GameObject proj = Instantiate(molotovProjectile, initialPos, Util.QuaternionTowardsMouse(transform.position));
        TargetProjectile targetProj = proj.GetComponent<TargetProjectile>();
        targetProj.SetTarget(target);
        targetProj.SetSpeed(speed);

        targetProj.onHit.AddListener(CocktailHit);
    }

    private void CocktailHit(GameObject cocktail) {
        GameObject fireObject = Instantiate(fire, cocktail.transform.position, Quaternion.identity);
        StartCoroutine(DamageTickLoop(fireObject));
    }

    private IEnumerator DamageTickLoop(GameObject fireObject) {
        float fireProgress = 0f;

        while (fireProgress < fireDuration) {
            Collider2D[] hits = Physics2D.OverlapCircleAll(fireObject.transform.position, radius, enemyLayers);
            foreach (var hit in hits) {
                hit.GetComponent<Health>()?.TakeDamage(damagePerTick);
            }

            fireProgress += Globals.TickRate;
            yield return new WaitForSeconds(Globals.TickRate);
        }

        Destroy(fireObject);
    }
}
