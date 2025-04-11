using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentMover))]
[RequireComponent(typeof(TopDownDirection))]
public class Player : MonoBehaviour
{
    public static Player Instance;
    public static Health health;

    [SerializeField] private AnimationManager anim;
    [SerializeField] private Rigidbody2D rb;
    private TopDownDirection direction;
    private AgentMover mover;
    
    private Vector2 movement;

    private void Awake() {
        Instance = this;
        movement = new Vector2(0, 0);
        direction = GetComponent<TopDownDirection>();
        mover = GetComponent<AgentMover>();
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
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mover.SetDirection(movement);
    }

    void FixedUpdate()
    {
        if (mover.GetMovement() != Vector2.zero) {
            anim.ChangeState("Player_Walk_" + direction.GetFacing());
        }
        else {
            anim.ChangeState("Player_Idle_" + direction.GetFacing());
        }
    }

    // public void ApplyKnockback(Vector2 knockbackDirection, float knockbackStrength)
    // {
    //     isKnockbackActive = true;
    //     rb.velocity = knockbackDirection * knockbackStrength;  
    //     StartCoroutine(StopKnockbackAfterDelay(0.2f));  
    // }

    public void ApplyFreeze(float delay) {
        mover.Freeze();
        StartCoroutine(UnFreezeAfterDelay(delay));
    }

    private IEnumerator UnFreezeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mover.Unfreeze();
    }

    // private IEnumerator StopKnockbackAfterDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     isKnockbackActive = false; 
    //     if (mover.GetMovement() != Vector2.zero) {
    //         anim.ChangeState("Player_Walk_" + direction.GetFacing());
    //     }
    //     else {
    //         anim.ChangeState("Player_Idle_" + direction.GetFacing());
    //     }
    // }

    public static Vector3 GetPosition() {
        return Instance.transform.position;
    }
}
