using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TopDownDirection))]
[RequireComponent(typeof(Animator))]

public class HotRodMovement : MonoBehaviour
{
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 2.5f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float preferredDistance = 2f;
    [SerializeField] private float detectionRadius = 8f;
    [SerializeField] private Animator anim;

    private float distance = 100f;
    public int facing { get; private set; } = 1;
    public UnityEvent onMove;

    private Vector2 movement;
    private Rigidbody2D rb;
    private bool active = true;
    private bool towards = true;
    private bool hasLOS = false;

    void Awake()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(0, 0);
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (!active) {
            return;
        }
        
        if (hasLOS && !GetInDistance() && Distance() <= detectionRadius) {
            Vector3 direction = Player.GetPosition() - transform.position;
            movement = new Vector2(direction.x, direction.y);
            movement.Normalize();
        }
        else {
            movement = new Vector2(0, 0);
        }

        bool isWalking = movement.sqrMagnitude > 0f;       
        anim.SetBool("Walk", isWalking);

        if (isWalking)
        {
            onMove.Invoke();
            anim.SetFloat("MoveX", movement.x);  
            anim.SetFloat("MoveY", movement.y);
        }                   

    }

    void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, Player.GetPosition());

        int direction = -1;
        if (towards) direction = 1;
        rb.velocity = movement * moveSpeed * direction;

        int layerMask = ~LayerMask.GetMask("Enemy");
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Player.GetPosition() - transform.position, Distance(), layerMask);
        if (ray.collider != null) {
            hasLOS = ray.collider.CompareTag("Player");
        }
    }

    public bool HasLOS() {
        return hasLOS;
    }

    public void SetActive(bool isActive) {
        active = isActive;

        if (!active) {
            movement = new Vector2(0, 0);
        }
    }

    public void SetDistance(float dist) {
        preferredDistance = dist;
    }

    public float Distance() {
        return distance;
    }

    public bool GetInDistance() {
        return distance <= preferredDistance;
    }

    public void SetTowards(bool newTowards) {
        towards = newTowards;
    }
}
