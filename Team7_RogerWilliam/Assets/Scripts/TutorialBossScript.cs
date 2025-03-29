using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBossScript : MonoBehaviour
{
    protected GameObject gameHandler;
    protected GameObject player;

    protected Rigidbody2D rb2D;
    [SerializeField] private float moveSpeed = 2.5f;

    [SerializeField] private float attackRange = 4f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackDamage = 20f;

    [SerializeField] public float knockBack = 20f;
    private float distance;
    private float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.Log("ERROR HERE");
        }
        rb2D = GetComponent<Rigidbody2D>();
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
            Debug.Log("MadeItToAttackBoss");
            BossAttack();
        }
    }

    protected virtual void BossAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = attackCooldown;
            if (gameHandler != null)
            {
                gameHandler.GetComponent<Health>().TakeDamage(attackDamage);
            }
            player.gameObject.GetComponent<Player_EndKnockBack>().EndKnockBack();
            Rigidbody2D pushRB = player.gameObject.GetComponent<Rigidbody2D>();
            Vector2 moveDirectionPush = rb2D.transform.position - player.transform.position;
            pushRB.AddForce(moveDirectionPush.normalized * knockBack * - 1f, ForceMode2D.Impulse);
            Debug.Log("KnkockBack happends");
        }
    }
}
