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
    private float abilityTimer;
    private float abilityCD;
    private LoadoutManager bossLoadout;
    private bossWeaponHandler BossWeaponHandler;
    private bossAbilityHandler BossAbilityHandler;
    private bossPassiveHandler BossPassiveHandler;
    private Weapon bossWeapon;
    private Ability bossAbility;
    private Passive bossPassive;
    public AgentMover Bossmover;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        bossLoadout = GetComponent<LoadoutManager>();
        BossWeaponHandler = GetComponentInChildren<bossWeaponHandler>();
        BossAbilityHandler = GetComponentInChildren<bossAbilityHandler>();
        BossPassiveHandler = GetComponentInChildren<bossPassiveHandler>();
        bossPassive = BossPassiveHandler.GetPassive();
        bossAbility = BossAbilityHandler.GetAbility();
        attackTimer = attackCooldown;
        bossWeapon = BossWeaponHandler.GetWeapon();
        string abilityName = bossAbility.name;
        Bossmover = GetComponent<AgentMover>();
        string weaponName = bossWeapon.name;
        Debug.Log(abilityName);
        Debug.Log(weaponName);
        if(weaponName == "Fists(Clone)" || weaponName == "Guitar(Clone)")
        {
            attackRange = 1f;
        }
        if(weaponName == "Flamethrower(Clone)")
        {
            attackTimer = 10f;
            attackRange = 5f;
        }
        if(weaponName == "Slingshot(Clone)")
        {
            attackRange = 10f;
            // attackTimer = 3f;
        }
        if(weaponName == "SalamiWhip(Clone)")
        {
            attackRange = 1f;
            attackTimer = 2f;
        }
        if(abilityName == "MolotovCocktail(Clone)")
        {
            abilityTimer = 5f;
            abilityCD = 5f;

        }
        if(abilityName == "ConfuseAbility(Clone)")
        {
            abilityTimer = 10f;
            abilityCD = 10f;
        }
        if(abilityName == "SalamiSpin(Clone)")
        {
            abilityTimer = 0f;
            abilityCD = 2f;
        }
        if(abilityName == "GroundPound(Clone)")
        {
            abilityTimer = 10f;
            abilityCD = 10f;
        }
        if(abilityName == "Medpack(Clone)")
        {
            abilityTimer = 12f;
            abilityCD = 12f;
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
        if(abilityTimer <= 0)
        {
            Debug.Log("AbilityActivated");
            bossAbility.Activate();
            abilityTimer = abilityCD;
        }
        attackTimer -= Time.deltaTime;
        abilityTimer -= Time.deltaTime;
    }
}
