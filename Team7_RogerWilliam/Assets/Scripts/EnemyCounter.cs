using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private GameObject[] enemies;
    private int curr_enemies;
    private int start_enemies;
    public int goal;
    private int kill_count; 

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        start_enemies = enemies.Length;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        curr_enemies = enemies.Length;
        kill_count = start_enemies - curr_enemies;

        if (goal <= kill_count) {
            GetComponent<BoxCollider2D>().enabled = false;
            //Debug.Log("Removing boulder collider");
        }

    }
}
