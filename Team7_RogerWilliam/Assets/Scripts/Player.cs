using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;

    public static Player instance;

    private Vector2 movement;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }

        else {
            Destroy(gameObject);
        }
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

