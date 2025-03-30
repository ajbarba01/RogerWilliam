using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

    private GameObject player;
    
    private bool attacking = true;

    private float attackChannel = 0f;
    private float attackDuration = 1.5f;

    [SerializeField] private float attackRange = 10f;
    [SerializeField] private RangedAttack rangeAttack;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rangeAttack = GetComponent<RangedAttack>();
    }

    void Update()
    {
        if (attacking) {
            attackChannel += Time.deltaTime;
            if (attackChannel >= attackDuration) {
                // rangeAttack.Attack(new Vector2(1, 1));
                // attacking = false;
                attackChannel = 0f;
            }
        }

        else {
            float distance = 20;
            if (distance <= attackRange) {
                attacking = true;
            }
            else {
                // CHASE
            }
        }
    }
}
