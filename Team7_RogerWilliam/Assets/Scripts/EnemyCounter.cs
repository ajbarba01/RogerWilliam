using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    private GameObject[] enemies;
    public int goal;
    private int start_enemies;
    private int curr_enemies;
    private int kill_count; 
    private int remaining_kills;
    public TMP_Text remainingKills_text;

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
        remaining_kills = goal - kill_count;

        if (goal <= kill_count) {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if (remaining_kills >= 0) {
            remainingKills_text.text = remaining_kills.ToString();
        }

    }
}
