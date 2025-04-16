using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
    [SerializeField] private float dps = 30f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private GameObject fireEffect;

    [SerializeField] private float attackTime = 3f;
    [SerializeField] private float reloadTime = 5f;

    private float reloadSpeed;
    private float fuelSpeed;
    private float capacity = 1f;

    private bool attacking = false;
    private bool canAttack = true;

    private Collider2D[] enemyHits = new Collider2D[20];

    private void Awake() {
        reloadSpeed = 1 / reloadTime;
        fuelSpeed = 1 / attackTime;
    }

    void Start()
    {
        if (fireEffect != null)
        {
            fireEffect.SetActive(false);
        }
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            attacking = false;
            canAttack = true;
        }

        if (capacity <= 0) {
            capacity = 0f;
            attacking = false;
        }

        if (!attacking && capacity != 1) {
            capacity += reloadSpeed * Time.deltaTime;
            if (capacity > 1) capacity = 1f;
        }
    }

    public override void Attack() {
        if (canAttack) {
            StartCoroutine(StartAttack());
        }
    }

    private IEnumerator StartAttack() {
        canAttack = false;
        attacking = true;
        fireEffect.SetActive(true);

        while (attacking) {
            capacity -= fuelSpeed * Time.deltaTime;
            Vector3 direction = Util.TowardsMouse(transform.position);
            mover.FaceTowardsMouse(transform.position);

            fireEffect.transform.position = transform.position + direction * 2f;
            fireEffect.transform.rotation = Util.QuaternionOfVector3(direction, -45f);

            DealDamageInZone();
            yield return null;
        }

        fireEffect.SetActive(false);
    }

    void DealDamageInZone()
    {
        int hitCount = DetectEnemies();
        for (int i = 0; i < hitCount; i++)
        {
            Collider2D enemyCollider = enemyHits[i];
            Health enemyHealth = enemyCollider.GetComponent<Health>();
            enemyHealth.TakeDamage(dps * Time.deltaTime);
            onEnemyHit.Invoke(enemyHealth);
        }
    }

    private int DetectEnemies()
    {
        int hitCount = Physics2D.OverlapCircleNonAlloc(fireEffect.transform.position, 1f, enemyHits, enemyLayers);

        return hitCount;
    }

    public override float HUDFill() {
        return capacity;
    }
}
