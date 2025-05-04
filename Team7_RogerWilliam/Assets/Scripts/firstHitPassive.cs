using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstHitPassive : Passive
{


    void Start()
    {
        Player.Instance.GetComponentInChildren<WeaponHandler>().GetWeapon().onEnemyHit.AddListener(onEnemyHit);
    }

    void OnDestroy()
    {
        Player.Instance.GetComponentInChildren<WeaponHandler>().GetWeapon().onEnemyHit.RemoveListener(onEnemyHit);
    }
    void onEnemyHit(Health enemyHealth)
    {
        // Debug.Log("On hit");
        if (Mathf.Approximately(enemyHealth.GetHealth(), enemyHealth.GetMaxHealth()))
        {
            // Debug.Log("Made it to damage");
            enemyHealth.TakeDamage(5);
        }
    }
}
