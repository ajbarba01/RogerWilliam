using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 10f;
    public float selfDestructTime = 2.0f;
    public GameObject hitEffectAnim;

    private Vector2 targetDirection;

    void Start()
    {
        // Optionally, you can put other code for initialization here
    }

    // This method is called to initialize the projectile and set the target direction
    public void Initialize(Vector2 targetPosition)
    {
        targetDirection = (targetPosition - (Vector2)transform.position).normalized; // Calculate direction toward the player
        StartCoroutine(SelfDestruct());
    }

    void Update()
    {
        // Move the projectile in the direction of the target
        transform.position += (Vector3)targetDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.health.TakeDamage(damage); // Deal damage to the player
            Hit();

        }

        else if (collision.CompareTag("Wall"))
        {
            Hit();
        }
    }

    // Self-destructs after a set amount of time
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructTime);
        Destroy(gameObject);
    }

    void Hit() {
        if (hitEffectAnim != null)
            {
                GameObject animEffect = Instantiate(hitEffectAnim, transform.position, Quaternion.identity);
                Destroy(animEffect, 0.5f);
            }
        Destroy(gameObject);  // Destroy the projectile after it hits
    }
}
