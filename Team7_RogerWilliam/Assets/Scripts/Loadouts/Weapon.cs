using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : Loadout {
    public UnityEvent<Health> onEnemyHit;
    [SerializeField] protected float cooldown;
    public abstract void OnAttack();

    protected float cooldownProgress;
    protected bool onCooldown = false;

    protected AnimationManager anim;
    protected AgentMover mover;

    public void Initialize(AnimationManager animManage, AgentMover agentMover) {
        anim = animManage;
        mover = agentMover;
    }

    public float GetCooldown() {
        return cooldownProgress / cooldown;
    }

    public virtual float HUDFill() {
        if (onCooldown) return 1 - GetCooldown();

        return 0;
    }

    public bool OnCooldown() {
        return onCooldown;
    }

    public virtual void Attack() {
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