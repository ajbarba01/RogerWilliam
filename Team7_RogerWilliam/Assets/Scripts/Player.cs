using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;

    public static Player instance;
    public static Health health;

    private Vector2 movement;
    public bool isKnockbackActive = false;  

    private void Awake() {
        instance = this;
        movement = new Vector2(0, 0);
    }

    private void Start() {
        health = GameHandler.playerHealth;
    }

    void Update()
    {
        if (!isKnockbackActive)  
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();
        }
    }

    void FixedUpdate()
    {
        if (!isKnockbackActive)  
        {
            rb.velocity = movement * moveSpeed;
        }
    }

    public void ApplyKnockback(Vector2 knockbackDirection, float knockbackStrength)
    {
        isKnockbackActive = true; 
        rb.velocity = knockbackDirection * knockbackStrength;  
        StartCoroutine(StopKnockbackAfterDelay(0.2f));  
    }

    private IEnumerator StopKnockbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isKnockbackActive = false; 
    }

    public static Vector3 GetPosition() {
        return instance.transform.position;
    }
}
