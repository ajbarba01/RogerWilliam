using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentMover))]
// [RequireComponent(typeof(TopDownDirection))]
public class Player : MonoBehaviour
{
    public static Player Instance;
    public static Health health;

    [SerializeField] private AnimationManager anim;
    [SerializeField] private Rigidbody2D rb;
    // private TopDownDirection direction;
    private AgentMover mover;
    
    private Vector2 movement;

    private bool movementPaused = false;
    public bool moving = false;

    private void Awake() {
        Instance = this;
        movement = new Vector2(0, 0);
        // direction = GetComponent<TopDownDirection>();
        mover = GetComponent<AgentMover>();
    }

    private void Start() {
        health = GameHandler.playerHealth;
        anim.ChangeState("Player_Idle");
    }

    void Update()
    {
        if (Pause.isPaused) {
            return;
        }
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        if (movement == Vector2.zero) {
            movementPaused = false;
        }

        if (!movementPaused) {
            mover.SetDirection(movement);

            if (movement != Vector2.zero) {
                moving = true;
            }
            else {
                moving = false;
            }
        }
        else {
            mover.SetMovement(Vector2.zero);
        }
    }

    void FixedUpdate()
    {
        if (mover.GetMovement() != Vector2.zero) {
            anim.ChangeState("Player_Walk");
        }
        else {
            anim.ChangeState("Player_Idle");
        }
    }

    public void PauseMovement() {
        movementPaused = true;
        moving = false;
    }


    public void ApplyFreeze(float delay) {
        mover.Freeze();
        StartCoroutine(UnFreezeAfterDelay(delay));
    }

    private IEnumerator UnFreezeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mover.Unfreeze();
    }

    public static Vector3 GetPosition() {
        return Instance.transform.position;
    }
}
