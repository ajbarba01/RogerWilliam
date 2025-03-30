using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPt;

    [SerializeField] private float speed = 10f;
    [SerializeField] private string damageTag;
    [SerializeField] private float damage = 0f;

    public void Attack(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject instance = Instantiate(projectile, attackPt.position, Quaternion.Euler(0, 0, angle + 90));

        Projectile script = instance.GetComponent<Projectile>();

        script.SetDamage(damage);
        script.SetDirection(direction);
        script.SetDamageTag(damageTag);
        script.SetSpeed(speed);
    }
}
