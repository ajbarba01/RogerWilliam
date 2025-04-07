using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AnimationManager anim;

    public static Player instance;
    public static Health health;

    private enum Facing {
        Front,
        Back,
        Right,
        Left
    }
    
    private Facing direction = Facing.Front;

    private Vector2 movement;
    public bool isKnockbackActive = false;  

    private void Awake() {
        instance = this;
        movement = new Vector2(0, 0);
    }

    private void Start() {
        health = GameHandler.playerHealth;
        anim.ChangeState("Player_Idle_Front");
    }

    void Update()
    {
        if (!isKnockbackActive)  
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();

            UpdateDirection();

            // Update Animation
            if (movement.x != 0 || movement.y != 0) {
                switch (direction) {
                case Facing.Front:
                    anim.ChangeState("Player_Walk_Front");
                    break;

                case Facing.Back:
                    anim.ChangeState("Player_Walk_Back");
                    break;

                default:
                    anim.ChangeState("Player_Walk_Side");
                    break;
                }
            }
            else {
                switch (direction) {
                case Facing.Front:
                    anim.ChangeState("Player_Idle_Front");
                    break;

                case Facing.Back:
                    anim.ChangeState("Player_Idle_Back");
                    break;

                default:
                    anim.ChangeState("Player_Idle_Side");
                    break;
                }
            }
            
        }
    }

    void UpdateDirection() {
        if (movement.y > 0) {
            direction = Facing.Back;
        }

        else if (movement.y < 0) {
            direction = Facing.Front;
        }

        else if (movement.x > 0) {
            direction = Facing.Right;
            Vector3 newScale = transform.localScale;
            newScale.x = 1.0f;
            transform.localScale = newScale;
        } 
        else if (movement.x < 0) {
            direction = Facing.Left;
            Vector3 newScale = transform.localScale;
            newScale.x = -1.0f;
            transform.localScale = newScale;
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
