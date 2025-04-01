using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackCooldown = 2.5f;

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
    }

    public void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
          
            projectile.GetComponent<EnemyProjectile>().Initialize(Player.GetPosition());
        }
        else
        {
            Debug.LogError("Projectile prefab or firePoint is missing on " + gameObject.name);
        }
    }
}