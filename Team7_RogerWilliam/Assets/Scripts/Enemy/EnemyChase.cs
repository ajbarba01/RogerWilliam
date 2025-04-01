using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 2.5f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float preferredDistance = 5f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private bool active = true;
    private bool inDistance = false;
    private bool towards = true;

    void Awake()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(0, 0);
    }

    void Update()
    {
        if (!active) {
            return;
        }

        bool los = HasLOS();
        if (los && Vector3.Distance(transform.position, Player.GetPosition()) > preferredDistance) {
            inDistance = false;
            Vector3 direction = Player.GetPosition() - transform.position;
            movement = new Vector2(direction.x, direction.y);
            movement.Normalize();
        }
        else {
            inDistance = true;
            movement = new Vector2(0, 0);
        }
    }

    void FixedUpdate()
    {
        int direction = -1;
        if (towards) direction = 1;
        rb.velocity = movement * moveSpeed * direction;
    }

    bool HasLOS() {
        return true;
    }

    public void SetActive(bool isActive) {
        active = isActive;
    }

    public void SetDistance(float dist) {
        preferredDistance = dist;
    }

    public bool GetInDistance() {
        return inDistance;
    }

    public void SetTowards(bool newTowards) {
        towards = newTowards;
    }
}
