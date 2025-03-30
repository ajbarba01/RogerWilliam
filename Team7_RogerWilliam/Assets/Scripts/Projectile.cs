using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifespan = 3f;

    private float speed = 10f;
    private string damageTag;
    private Vector2 direction;
    private float damage = 0f;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void SetDamage(float newDamage) {
        damage = newDamage;
    }

    public void SetDamageTag(string tag) {
        damageTag = tag;
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGERED");
        if (other.CompareTag(damageTag))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}