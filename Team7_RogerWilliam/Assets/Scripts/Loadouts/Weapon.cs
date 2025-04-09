using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : Loadout {
    public UnityEvent<Health> onEnemyHit;
    protected float cooldown;
    public abstract void OnAttack();

    protected float cooldownProgress;
    protected bool onCooldown = false;

    public float GetCooldown() {
        return cooldownProgress / cooldown;
    }

    public bool OnCooldown() {
        return onCooldown;
    }

    public void Attack() {
        OnAttack();
        StartCoroutine(Cooldown());
    }

    protected IEnumerator Cooldown() {
        onCooldown = true;
        while (cooldownProgress < cooldown) {
            cooldownProgress += Time.deltaTime;
            yield return null;
        }
        onCooldown = false;
        cooldownProgress = 0f;
    }
}