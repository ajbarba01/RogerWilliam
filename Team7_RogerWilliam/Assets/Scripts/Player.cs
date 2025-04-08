using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopDownDirection))]
public class Player : MonoBehaviour
{
    public static Player instance;
    public static Health health;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AnimationManager anim;
    private TopDownDirection direction;
    
    private Vector2 movement;
    public bool isKnockbackActive = false;  

    private void Awake() {
        instance = this;
        movement = new Vector2(0, 0);
        direction = GetComponent<TopDownDirection>();
    }

    private void Start() {
        health = GameHandler.playerHealth;
        anim.ChangeState("Player_Idle_Front");
    }

    void Update()
    {
        if (Pause.isPaused) {
            return;
        }
        
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

            if (rb.velocity.sqrMagnitude != 0) {
                anim.ChangeState("Player_Walk_" + direction.GetFacing());
            }
            else {
                anim.ChangeState("Player_Idle_" + direction.GetFacing());
            }
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
