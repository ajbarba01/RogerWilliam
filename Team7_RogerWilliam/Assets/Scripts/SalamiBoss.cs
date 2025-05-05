using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamiBoss : MonoBehaviour
{
    protected GameObject gameHandler;
    public Transform player;
    public GameObject sausagePrefab;
    public Transform firePoint;
    public float throwCooldown = 2f;
    public float sausageSpeed = 10f;
    private float lastThrowTime = -Mathf.Infinity;

    public Animator anim;

    private string walkState = "giantsalami_walk";
    private string attackState = "giantsalami_hit";
    private string idleState = "giantsalami_idle";

    private AnimationManager animMgr;

    private void Update()
    {
        if (player == null || sausagePrefab == null || firePoint == null)
            return;

        // Vector3 scale = transform.localScale;
        // scale.x = player.position.x < transform.position.x ? -1 : 1;
        // transform.localScale = scale;

        if (Time.time >= lastThrowTime + throwCooldown)
        {
            ThrowSausage();
            lastThrowTime = Time.time;
        }

        gameHandler = GameObject.FindWithTag("GameController");
        anim = gameObject.GetComponent<Animator>();

        animMgr = GetComponentInChildren<AnimationManager>();

        if (animMgr != null) {
                // Debug.Log("HERE!");
                animMgr.ChangeState(walkState);
        } 

    }

    private void ThrowSausage()
    {
        GameObject sausage = Instantiate(sausagePrefab, firePoint.position, Quaternion.identity);
        if (animMgr != null) {
            animMgr.PlayOnce(attackState);
        }


        Rigidbody2D rb = sausage.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.velocity = direction * sausageSpeed;
        }
    }
}
