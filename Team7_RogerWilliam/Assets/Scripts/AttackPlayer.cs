using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    protected GameObject gameHandler;
    protected GameObject player;

    public float moveSpeed = 4f;
    private float distance;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    private float attackTimer;
    public float attackDamage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.Log("ERROR HERE");
        }
        gameHandler = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        if (distance > attackRange) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        } else 
        {
            currEnemyAttack();
        }
    }

    protected virtual void currEnemyAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = attackCooldown;
            if (gameHandler != null)
            {
                Debug.Log("Hit by Enemy");
                // gameHandler.GetComponent<Health>().takeDamage(attackDamage);
            }
        }
    }
}
