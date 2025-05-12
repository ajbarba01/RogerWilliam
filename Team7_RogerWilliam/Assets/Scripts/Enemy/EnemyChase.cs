using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentMover))]
public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 2.5f;
    [SerializeField] private float moveSpeed;
    public float preferredDistance = 2f;
    [SerializeField] private float detectionRadius = 8f;

    private AgentMover mover;

    public Animator anim;

    private float distance = 100f;

    private Vector2 movement;
    private bool active = true;
    private bool towards = true;
    private bool hasLOS = false;

    public bool isConfused = false;

    void Awake()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        movement = new Vector2(0, 0);
        anim = gameObject.GetComponent<Animator>();
        mover = GetComponent<AgentMover>();
    }
    
    private void Start() {
        mover.SetMovespeed(moveSpeed);
    }

    void Update()
    {
        if (!active) {
            return;
        }
        if (!isConfused) {
            if (hasLOS && !GetInDistance() && Distance() <= detectionRadius) {
            Vector3 direction = Player.GetPosition() - transform.position;

            int directionTowards = -1;
            if (towards) directionTowards = 1;

            movement = new Vector2(direction.x, direction.y) * directionTowards;
            movement.Normalize();
            
            // anim.SetBool("Walk", true);
            }
            else {
                movement = new Vector2(0, 0);
                // anim.SetBool("Walk", false);
            }
        }
        else {
            if (hasLOS && !GetInDistance() && Distance() <= detectionRadius) {
            // walk in the opposite direction if confused
            Vector3 direction = transform.position - Player.GetPosition();

            movement = new Vector2(direction.x, direction.y).normalized;

            }
        }

        mover.SetMovement(movement * moveSpeed);
    }

    void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, Player.GetPosition());
        int layerMask = ~LayerMask.GetMask("Enemy", "Projectile", "Hazard");
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Player.GetPosition() - transform.position, Distance(), layerMask);
        if (ray.collider != null) {
            hasLOS = ray.collider.CompareTag("Player");
        }

    }

    public bool HasLOS() {
        return hasLOS;
    }

    public void SetActive(bool isActive) {
        bool wasActive = active;
        active = isActive;

        if (active = wasActive) return;
        
        if (active) {
            mover.Unfreeze();
        }

        else {
            mover.Freeze();
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

    public void Confuse(float duration) {
        isConfused = true;
        StartCoroutine(ConfuseDuration(duration));
    }

    private IEnumerator ConfuseDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isConfused = false;
    }


}
