using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube_boss : MonoBehaviour
{
    protected GameObject gameHandler;
    protected GameObject player;
    public GameObject MusicProjectile;
    public Transform firePoint;
    protected Transform player_transform;

    protected Rigidbody2D rb2D;
    [SerializeField] private float moveSpeed = 2.5f;

    [SerializeField] private float Range1 = 7f;
    [SerializeField] private float attack1Range = 4f;
    [SerializeField] private float attack1Cooldown = 1f;
    [SerializeField] private float attack1Damage = 20f;

    [SerializeField] private float Range2 = 15f;
    [SerializeField] private float attack2Range = 11f;
    [SerializeField] private float attack2Cooldown = 2f;
    //[SerializeField] private float attack2Damage = 20f;
    
    private float distance;
    private float attack1Timer;
    private float attack2Timer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_transform = player.transform;
        if (player == null)
        {
            Debug.Log("ERROR: Player not found");
        }
        rb2D = GetComponent<Rigidbody2D>();
        gameHandler = GameHandler.instance.gameObject;

        GetComponent<Health>().onDeath.AddListener(OnDeath);
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        
        if (distance <= Range2 && distance > attack2Range) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        } 
        else if (distance <= attack2Range && distance > Range1) {
            BossAttack2();
        }
        else if (distance <= Range1 && distance > attack1Range) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else {
            BossAttack1();
        }
    }

    protected void BossAttack1() {
        attack1Timer -= Time.deltaTime;
        if (attack1Timer <= 0)
        {
            attack1Timer = attack1Cooldown;

            if (gameHandler != null)
            {
                gameHandler.GetComponent<Health>().TakeDamage(attack1Damage);
            }

        }
    }

    protected void BossAttack2() {
        attack2Timer -= Time.deltaTime;
        if (attack2Timer <= 0)
        {
            attack2Timer = attack2Cooldown;

            if (gameHandler != null)
            {
                GameObject projectile = Instantiate(MusicProjectile, firePoint.position, Quaternion.identity);

                //aim at player
                Vector2 direction = (player_transform.position - firePoint.position).normalized;
                
                //sending the projectile
                Rigidbody2D proj_rb = projectile.GetComponent<Rigidbody2D>();
                if (proj_rb != null)
                {
                    float projectileSpeed = 6f;
                    proj_rb.velocity = direction * projectileSpeed;
                }
            }

        }
    }

    public void OnDeath() {
        GameHandler.instance.ChangeScene("HomeBase");
    }

}
