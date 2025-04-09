using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    private Ability currentAbility;
    [SerializeField] private LastHitEnemy lastHit;

    void Start()
    {
        SetAbility(LoadoutManager.Instance.currentAbility);
    }

    void Update()
    {
        if (currentAbility != null && !currentAbility.OnCooldown() && Input.GetKeyDown(KeyCode.Space)){
            currentAbility.Activate();
        }
    }

    void SetAbility(LoadoutOption ability) {
        if (ability == null || ability.GetPrefab() == null) {
            return;
        }

        GameObject abilityPrefab = ability.GetPrefab();

        RemoveAbility();

        GameObject newAbility = Instantiate(abilityPrefab, transform);
        
        currentAbility = newAbility.GetComponent<Ability>();
        currentAbility.onEnemyHit.AddListener(lastHit.EnemyHit);
    }

    public Ability GetAbility() {
        return currentAbility;
    }

    void RemoveAbility() {
        if (currentAbility != null) {
            currentAbility.onEnemyHit.RemoveListener(lastHit.EnemyHit);
            Destroy(currentAbility);
        }

    }
}
