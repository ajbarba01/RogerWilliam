using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveHandler : MonoBehaviour
{
    private Passive currentPassive;
    // [SerializeField] private LastHitEnemy lastHit;

    void Start()
    {
        SetPassive(LoadoutManager.Instance.currentPassive);
    }

    void SetPassive(LoadoutOption passive) {
        if (passive == null || passive.GetPrefab() == null) {
            return;
        }

        GameObject passivePrefab = passive.GetPrefab();

        RemovePassive();

        GameObject newPassive = Instantiate(passivePrefab, transform);
        
        currentPassive = newPassive.GetComponent<Passive>();
        // currentPassive.onEnemyHit.AddListener(lastHit.EnemyHit);
    }

    void RemovePassive() {
        if (currentPassive != null) {
            // currentPassive.onEnemyHit.RemoveListener(lastHit.EnemyHit);
            Destroy(currentPassive);
        }

    }
}
