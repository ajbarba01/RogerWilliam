using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform fireBase;
    public Transform firePoint;
    public GameObject projectilePrefab;
    private Transform player;
    private float scaleX;
    private Vector2 PlayerVect;
    private Renderer rend;
    public float projectileSpeed = 10f;
       private float attackChannel = 0f;
           [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRange = 10f;

    public Animator anim;
    private EnemyChase enemyChase;
    private bool attacking = false;

    private float attackTimer;

    private void Awake() {
        enemyChase = GetComponent<EnemyChase>();
        // anim = gameObject.GetComponent<Animator>();
    }

    private void Start() {
        enemyChase.SetDistance(attackRange);
        rb = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerVect = player.transform.position;
        rend = GetComponentInChildren<Renderer>();
    }

    void Update()
    {
        if (enemyChase.HasLOS()) {
            if (attacking) {
            ChannelAttack();
            }

            else if (enemyChase.GetInDistance()) {
                enemyChase.SetActive(false);
                attacking = true;
            }

            else {
                attacking = false;
                enemyChase.SetActive(true);
            }
        }
        else {
            attacking = false;
        }
    }


    void ChannelAttack() {
        attackChannel += Time.deltaTime;
        if (attackChannel >= attackCooldown) {
            Attack();
            attackChannel = 0;
            attacking = false;
            enemyChase.SetActive(false);
        }
    }

    void Attack() {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);

    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            Health currHealth = this.GetComponent<Health>();
            currHealth.TakeDamage(2);
        }
    }

}
