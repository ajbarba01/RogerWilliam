using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastHitEnemy : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerAttackMelee attack;

    private GameObject lastHit;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemyHit = attack.lastEnemyHit;

        // need to update bar
        if (lastHit != enemyHit) {
            lastHit = enemyHit;
            healthBar.SetHealth(lastHit.GetComponent<Health>());
            healthBar.gameObject.SetActive(true);
        }
        else if (lastHit == null) {
            healthBar.gameObject.SetActive(false);
        }
    }
}
