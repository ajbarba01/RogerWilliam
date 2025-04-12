using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifespan = 3f;

    private float speed = 0f;
    private string damageTag;
    private Vector2 direction;
    private float damage = 0f;

    public UnityEvent<GameObject, GameObject> onTagHit;

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
        // transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    public Vector2 GetDirection() {
        return direction;
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

    public float GetSpeed() {
        return speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(damageTag))
        {
            onTagHit.Invoke(gameObject, other.gameObject);
            other.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}