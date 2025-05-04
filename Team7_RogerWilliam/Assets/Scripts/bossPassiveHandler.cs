using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossPassiveHandler : MonoBehaviour
{
     private Passive currentPassive;
     [SerializeField] private Transform playerTarget;
    // [SerializeField] private LastHitEnemy lastHit;

    void Awake()
    {
        LoadoutManager.Instance.passiveUpdated.AddListener(SetPassive);
    }

    void SetPassive(LoadoutOption passive) {
        if (passive == null || passive.GetPrefab() == null) {
            return;
        }

        GameObject passivePrefab = passive.GetPrefab();

        RemovePassive();

        GameObject newPassive = Instantiate(passivePrefab, transform);
        
        currentPassive = newPassive.GetComponent<Passive>();
        currentPassive.setTargetTag("Player");
        currentPassive.SetTarget(playerTarget);
        // currentPassive.onEnemyHit.AddListener(lastHit.EnemyHit);
    }

    public Passive GetPassive()
    {
        return currentPassive;
    }

    void RemovePassive() {
        if (currentPassive != null) {
            // currentPassive.onEnemyHit.RemoveListener(lastHit.EnemyHit);
            Destroy(currentPassive);
        }

    }
}
