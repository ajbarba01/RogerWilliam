using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class AgentMover : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    // Between 0 and 1, with 0 being a rock
    [SerializeField] private float knockbackResistance; 
    
    
    private AnimationManager anim;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 movement;

    public int facing { get; private set; } = 1;

    private bool frozen = false;
    private bool trueFrozen = false;

    public UnityEvent onMove;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationManager>();
    }
    
    public void SetDirection(Vector2 newDirection) {
        direction = newDirection;
        direction.Normalize();
        movement = direction * moveSpeed;
    }

    public void SetMovement(Vector2 newMovement) {
        movement = newMovement;
    }

    public Vector2 GetMovement() {
        return movement;
    }

    public Vector2 GetDirection() {
        return direction;
    }

    public void Freeze() {
        movement = Vector2.zero;
        direction = Vector2.zero;
        frozen = true;
        trueFrozen = true;
    }

    public void Unfreeze() {
        frozen = false;
        trueFrozen = false;
    }

    public void SetMovespeed(float newSpeed) {
        moveSpeed = newSpeed;
    }

    public void ApplyKnockback(Vector2 knockbackDirection, float knockbackStrength) {
        StopAllCoroutines();
        StartCoroutine(_ApplyKnockback(knockbackDirection, knockbackStrength));
    }

    // private IEnumerator _ApplyKnockback(Vector2 knockbackDirection, float knockbackStrength) {
    //     Freeze();
    //     rb.drag = 10f;
    //     rb.velocity = knockbackDirection * knockbackStrength;
    //     yield return new WaitForSeconds(0.2f);
    //     rb.drag = 0f;
    //     Unfreeze();
    // }

    private IEnumerator _ApplyKnockback(Vector2 knockbackDirection, float knockbackStrength) {
        Freeze();

        float initialDrag = rb.drag;
        rb.drag = 10f;

        float duration = 0.3f;
        duration *= (1 - knockbackResistance);

        float durationProgress = duration;

        knockbackStrength *= (1 - knockbackResistance);

        while (durationProgress > 0f) {
            durationProgress -= Time.deltaTime;
            rb.velocity = (durationProgress / duration) * (knockbackDirection * knockbackStrength);
            yield return null;
        }

        rb.drag = initialDrag;
        Unfreeze();
    }

    private void FixedUpdate() {
        if (frozen) return;

        if (movement != Vector2.zero) {
            onMove.Invoke();
        }

        if (movement.x > 0) {
            facing = 1;
        }

        else if (movement.x < 0) {
            facing = -1;
        }
        
        SetFacing(facing);

        rb.velocity = movement;
    }

    public void SetFacing(int newFacing) {
        facing = newFacing;

        if (anim == null) return;
        anim.SetFacing(facing);
    }

    public void FaceTowardsMouse(Vector3 fromPos) {
        SetFacing(Util.MouseFacing(fromPos));
    }
}
