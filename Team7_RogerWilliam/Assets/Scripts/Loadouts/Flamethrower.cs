using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
    [SerializeField] private float dps = 30f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private GameObject flamethrower;

    [SerializeField] private float attackTime = 3f;
    [SerializeField] private float reloadTime = 5f;
    [SerializeField] private float slow = 0.7f;


    [SerializeField] private LayerMask playerLayers;

    private float reloadSpeed;
    private float fuelSpeed;
    private float capacity = 1f;

    private bool attacking = false;
    private bool canAttack = true;

    private Collider2D[] enemyHits = new Collider2D[20];
    private Collider2D[] playerHits = new Collider2D[20];

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

        flamethrower.SetActive(false);
    }

    private void Update() {
        if(targetTag == "Enemy")
        {
            if (Input.GetMouseButtonUp(0)) {
            attacking = false;
            canAttack = true;
            }
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
        flamethrower.SetActive(true);
        if(targetTag == "Enemy")
        {
            mover.SetSlow(slow);
        }
    
        StartCoroutine(DamageTickLoop());

        while (attacking) {
            capacity -= fuelSpeed * Time.deltaTime;
            if(targetTag == "Enemy")
            {
                Vector3 direction = Util.TowardsMouse(transform.position);
                mover.FaceTowardsMouse(transform.position);

                fireEffect.transform.position = transform.position + direction * 2f;
                fireEffect.transform.rotation = Util.QuaternionOfVector3(direction, -45f);

                flamethrower.transform.rotation = Util.QuaternionOfVector3(direction, 0f);
            } else
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Vector3 direction = (player.transform.position - transform.position).normalized;
                // mover.FaceTowards(player.transform.position);
                fireEffect.transform.position = transform.position + direction * 2f;
                fireEffect.transform.rotation = Util.QuaternionOfVector3(direction, -45f);

                flamethrower.transform.rotation = Util.QuaternionOfVector3(direction, 0f);
            }
            

            yield return null;
        }
        if(targetTag == "Enemy")
        {
            mover.RemoveSlow();
        }
        fireEffect.SetActive(false);
        flamethrower.SetActive(false);
    }

    void DealDamageInZone(float amount)
    {
        int hitCount = DetectEnemies();
        for (int i = 0; i < hitCount; i++)
        {
            if(targetTag == "Enemy")
            {
                Collider2D enemyCollider = enemyHits[i];
                Health enemyHealth = enemyCollider.GetComponent<Health>();
                enemyHealth.TakeDamage(amount);

                if (i == 0) {
                    onEnemyHit.Invoke(enemyHealth);
                }
            } else
            {
                Collider2D playerCollider = playerHits[i];
                if(playerCollider.CompareTag("Player"))
                {
                    Player.health.TakeDamage(amount);
                }
            }
            
        }
    }

    private IEnumerator DamageTickLoop() {
        while (attacking) {
            if(targetTag == "Enemy")
            {
                DealDamageInZone(dps * Globals.TickRate);
            } else
            {
                DealDamageInZone((dps/3.0f) * Globals.TickRate);
            }
            

            yield return new WaitForSeconds(Globals.deltaTick);
        }
    }

    private int DetectEnemies()
    {
        int hitCount;
        if(targetTag == "Enemy")
        {
            hitCount = Physics2D.OverlapCircleNonAlloc(fireEffect.transform.position, 1f, enemyHits, enemyLayers);
        } else
        {
            hitCount = Physics2D.OverlapCircleNonAlloc(fireEffect.transform.position, 1f, playerHits, playerLayers);
        }
        

        return hitCount;
    }

    public override float HUDFill() {
        return 1 - capacity;
    }

    public override void OnInterrupt() {
        attacking = false;
        canAttack = true;
    }
}
