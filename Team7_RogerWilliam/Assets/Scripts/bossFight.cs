using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossFight : MonoBehaviour
{

    protected GameObject gameHandler;
    protected GameObject player;

    protected Rigidbody2D rb2D;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown = 2f;
    // [SerializeField] private float attackDamage;
    private float distance;
    private float attackTimer;

    private LoadoutManager bossLoadout;
    private bossWeaponHandler BossWeaponHandler;
    private Weapon bossWeapon;
    public AgentMover mover;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        bossLoadout = GetComponent<LoadoutManager>();
        BossWeaponHandler = GetComponentInChildren<bossWeaponHandler>();
        attackTimer = attackCooldown;
        bossWeapon = BossWeaponHandler.GetWeapon();
        mover = GetComponent<AgentMover>();
        string weaponName = bossWeapon.name;
        Debug.Log(weaponName);
        if(weaponName == "Fists(Clone)" || weaponName == "Guitar(Clone)")
        {
            attackRange = 1f;
        }
        if(weaponName == "Flamethrower(Clone)")
        {
            attackTimer = 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        
        if (distance > attackRange) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }   
        else 
        {
            // Vector3 attackTarget = transform.position + direction.normalized * attackRange;
            // bossWeapon.attackPt.position = attackTarget;
            if(attackTimer <= 0)
            {
                bossWeapon.Attack();
                attackTimer = attackCooldown;
            }
            
        }
        attackTimer -= Time.deltaTime;
    }
}
