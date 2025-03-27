using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastHitEnemy : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.gameObject.SetActive(false);
    }

    public void EnemyHit(Health enemy) {
        healthBar.SetHealth(enemy);
        enemy.onDeath.AddListener(EnemyDied);
        healthBar.gameObject.SetActive(true);
    }

    public void EnemyDied() {
        healthBar.gameObject.SetActive(false);
    }
}
