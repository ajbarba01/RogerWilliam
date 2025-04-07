using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownDirection : MonoBehaviour
{
    private Rigidbody2D rb;
    private string direction = "Front";
    private string facing = "Front";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        UpdateDirection();
    }

    void UpdateDirection() {
        Vector2 movement = rb.velocity;
        
        if (movement.y > 0) {
            direction = "Back";
            facing = "Back";
        }

        else if (movement.y < 0) {
            direction = "Front";
            facing = "Front";
        }

        else if (movement.x > 0) {
            direction = "Right";
            facing = "Side";
            Vector3 newScale = transform.localScale;
            newScale.x = 1.0f;
            transform.localScale = newScale;
        } 
        else if (movement.x < 0) {
            direction = "Left";
            facing = "Side";
            Vector3 newScale = transform.localScale;
            newScale.x = -1.0f;
            transform.localScale = newScale;
        }
    }

    public string GetDirection() {
        return direction;
    }

    public string GetFacing() {
        return facing;
    }
}
