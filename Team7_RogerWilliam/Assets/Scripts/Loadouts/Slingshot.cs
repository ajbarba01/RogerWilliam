using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : Weapon
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPt;
    [SerializeField] private float maxCharge = 2f;
    [SerializeField] private float minVelocity, maxVelocity;
    [SerializeField] private float minDamage, maxDamage;
    [SerializeField] private float knockBackForce;
    [SerializeField] private GameObject art;
    [SerializeField] private float slow = 0.7f;

    private float charge = 0f;
    private bool charging;

    private void Start() {
        art.SetActive(false);
        charging = false;
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            Release();
        }
    }

    private IEnumerator Charging() {
        art.SetActive(true);
        mover.SetSlow(slow);

        charging = true;
        charge = 0f;
        while (charging) {
            charge += Time.deltaTime;
            transform.rotation = Util.QuaternionTowardsMouse(transform.position);
            mover.FaceTowardsMouse(transform.position);
            yield return null;
        }

        mover.RemoveSlow();
        art.SetActive(false);
    }

    public override void Attack() {
        if (charging) return;
        OnAttack();
    }

    public override void OnAttack() {
        StartCoroutine(Charging());
    }

    public override void OnInterrupt() {
        Release();
    }

    private void Release() {
        if (!charging) return;

        charging = false;
        
        if (charge > maxCharge) charge = maxCharge;

        float mod = (charge / maxCharge);
        float velocity = Mathf.Lerp(minVelocity, maxVelocity, mod);
        float damage = Mathf.Lerp(minDamage, maxDamage, mod);

        GameObject rock = Instantiate(projectile, attackPt.position, transform.rotation);
        Projectile proj = rock.GetComponent<Projectile>();
        proj.SetDamage(damage);
        proj.SetDamageTag("Enemy");
        proj.SetSpeed(velocity);
        proj.SetDirection((Vector2)(Util.TowardsMouse(transform.position)));
        proj.onTagHit.AddListener(EnemyHit);

        StartCoroutine(Cooldown());
    }

    public override float HUDFill() {
        if (charging) return charge / maxCharge;

        else return base.HUDFill();
    }

    public void EnemyHit(GameObject projectile, GameObject enemy) {
        onEnemyHit.Invoke(enemy.GetComponent<Health>());
        AgentMover enemyMover = enemy.GetComponent<AgentMover>();
        if (enemyMover == null) return;
        Projectile proj = projectile.GetComponent<Projectile>();
        Vector3 direction = (Vector3)(proj.GetDirection());
        enemyMover.ApplyKnockback(direction, knockBackForce * proj.GetSpeed() / maxVelocity);
    }
}
