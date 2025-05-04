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
    [SerializeField] public LayerMask playerLayers;


    protected override void OnActivate() {
        Vector3 dist;
        if(targetTag == "Enemy")
        {
            mover.FaceTowardsMouse(transform.position);
            dist = Util.DistToMouse(transform.position);
            if (dist.magnitude > attackRange) {
                dist = dist.normalized * attackRange;
            }
            anim.PlayOnce("Player_Punch");
        } else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 direction = (player.transform.position - transform.position).normalized;
            // Bossmover.FaceTowards(direction);
            dist = player.transform.position - transform.position;
            if (dist.magnitude > attackRange) {
                dist = dist.normalized * attackRange;
            }
        }
        Vector3 target = transform.position + dist;

        Vector3 initialPos = transform.position + dist.normalized * 0.5f;
        Quaternion rotation;
        if(targetTag == "Enemy")
        {
            rotation = Util.QuaternionTowardsMouse(transform.position);
        } else {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            rotation = Util.QuaternionOfVector3(directionToPlayer, 0f);
        }
        GameObject proj = Instantiate(molotovProjectile, initialPos, rotation);
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
            if(targetTag == "Enemy")
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(fireObject.transform.position, radius, enemyLayers);
                foreach (var hit in hits) {
                    hit.GetComponent<Health>()?.TakeDamage(dps * Globals.deltaTick);
                }
                fireProgress += Globals.deltaTick;
            } else
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(fireObject.transform.position, radius, playerLayers);
                foreach (var hit in hits) {
                    Player.health.TakeDamage(dps/2 * Globals.deltaTick);
                    // hit.GetComponent<Health>()?.TakeDamage(dps * Globals.deltaTick);
                }
                fireProgress += Globals.deltaTick;
            }
            
            yield return new WaitForSeconds(Globals.TickRate);
        }

        Destroy(fireObject);
    }
}
