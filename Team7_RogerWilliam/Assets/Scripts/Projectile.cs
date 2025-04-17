using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject hitFX;
    [SerializeField] private float lifespan = 3f;

    private float speed = 0f;
    private string damageTag = "";
    private Vector2 direction;
    private float damage = 0f;

    public UnityEvent<GameObject, GameObject> onTagHit;
    public UnityEvent<GameObject> onHit;

    public virtual void BeforeMove() { }

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    private void Update()
    {
        BeforeMove();
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
        if (damageTag != "" && other.CompareTag(damageTag))
        {
            onTagHit.Invoke(gameObject, other.gameObject);
            other.GetComponent<Health>().TakeDamage(damage);
            OnHit();
        }
        else if (other.CompareTag("Wall"))
        {
            OnHit();
        }
    }

    protected void OnHit() {
        if (hitFX != null) {
            Instantiate(hitFX, transform.position, Quaternion.identity);
        }

        onHit.Invoke(gameObject);
        Destroy(gameObject);
    }
}