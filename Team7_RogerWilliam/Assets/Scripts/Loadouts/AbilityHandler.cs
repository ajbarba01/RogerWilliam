using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityHandler : MonoBehaviour
{
    private Ability currentAbility;
    [SerializeField] private LastHitEnemy lastHit;

    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private AnimationManager playerAnim;
    [SerializeField] private AgentMover mover;

    private bool paused;

    // public UnityEvent startChannel, stopChannel;

    void Start()
    {
        SetAbility(LoadoutManager.Instance.currentAbility);
    }

    void Update()
    {
        if (paused) return;

        if (currentAbility != null && Input.GetMouseButtonDown(1)) {
            if (!currentAbility.OnCooldown()){
                currentAbility.Activate();
            }
            else {
                Warning.ShowWarning("Ability on Cooldown");
            }
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
        currentAbility.Initialize(playerAnim, mover);
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

    public void InterruptAbility() {
        if (currentAbility != null) {
            currentAbility.StopChannel();
        }
    }

    public void SetPause(bool pause) {
        paused = pause;
    }
}
