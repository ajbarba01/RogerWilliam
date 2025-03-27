using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] private Ability abilityOne, abilityTwo;
    [SerializeField] private LastHitEnemy lastHit;

    void Start() {
        SetAbility(abilityOne, 1);
        SetAbility(abilityTwo, 2);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) && abilityOne != null) {
            abilityOne.Activate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && abilityTwo != null) {
            abilityTwo.Activate();
        }
    }

    void SetAbility(Ability ab, int abNumber) {
        if (abNumber == 1) {
            abilityOne = ab;
            abilityOne.onEnemyHit.AddListener(lastHit.EnemyHit);
        }
        else {
            abilityTwo = ab;
            abilityTwo.onEnemyHit.AddListener(lastHit.EnemyHit);
        }
    }

    void RemoveAbility(int abNumber) {
        if (abNumber == 1) {
            abilityOne = abilityTwo;
            abilityOne.onEnemyHit.RemoveListener(lastHit.EnemyHit);
            abilityTwo = null;
        }

        else {
            abilityTwo = null;
            abilityTwo.onEnemyHit.RemoveListener(lastHit.EnemyHit);
        }
    }
}