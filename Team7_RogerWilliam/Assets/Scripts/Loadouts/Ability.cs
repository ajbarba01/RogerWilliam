using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public abstract class Ability : Loadout
{
    public UnityEvent<Health> onEnemyHit;
    protected float cooldown;
    public abstract void OnActivate();
    protected float cooldownProgress;
    protected bool onCooldown;

    public float GetCooldown() {
        return cooldownProgress / cooldown;
    }

    public bool OnCooldown() {
        return onCooldown;
    }

    public void Activate() {
        OnActivate();
        StartCoroutine(Cooldown());
    }

    protected IEnumerator Cooldown() {
        onCooldown = true;
        while (cooldownProgress < cooldown) {
            cooldownProgress += Time.deltaTime;
            yield return null;
        }
        onCooldown = false;
    }
}