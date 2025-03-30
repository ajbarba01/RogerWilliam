using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackCooldown = 2f;

    private Transform player;
    private float attackTimer;

    void Start()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint is not assigned in the RangedAttack script.");
        }

        if (projectilePrefab == null)
        {
        Debug.LogError("Projectile Prefab is not assigned in the RangedAttack script.");
        }

        player = GameObject.FindWithTag("Player").transform;
        attackTimer = 0f;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (player != null && attackTimer <= 0)
        {
            ShootProjectile();
            attackTimer = attackCooldown;
        }
    }

    public void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Debug.Log("FirePoint and Projectile Prefab are assigned properly.");
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<EnemyProjectile>().Initialize(player.position);
        }
        else
        {
            if (projectilePrefab == null)
            {
                Debug.LogError("Projectile Prefab is null!");
            }

            if (firePoint == null)
            {
                Debug.LogError("FirePoint is null!");
            }
        }
        
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
          
            projectile.GetComponent<EnemyProjectile>().Initialize(player.position);
        }
        else
        {
            Debug.LogError("Projectile prefab or firePoint is missing on " + gameObject.name);
        }
    }
}