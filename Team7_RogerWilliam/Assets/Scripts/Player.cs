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

    private void Awake() {
        instance = this;
        movement = new Vector2(0, 0);
    }

    private void Start() {
        health = GameHandler.playerHealth;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }
}

