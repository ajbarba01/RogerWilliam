using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public abstract class Ability : Loadout
{
    public UnityEvent<Health> onEnemyHit;

    [SerializeField] protected float cooldown;
    protected float cooldownProgress;
    protected bool onCooldown = false;

    [SerializeField] protected float duration = 0f;
    protected float durationProgress;

    protected bool channeling { get; private set; }

    protected abstract void OnActivate();
    protected virtual void OnInterrupt() { }

    protected AnimationManager anim;
    protected AgentMover mover;

    public void Initialize(AnimationManager animManage, AgentMover agentMover) {
        anim = animManage;
        mover = agentMover;
    }

    public void Activate() {
        if (onCooldown || channeling) return;
        StartCoroutine(Cooldown());
        StartCoroutine(Channel());
        OnActivate();
    }

    protected IEnumerator Cooldown() {
        onCooldown = true;
        while (cooldownProgress < cooldown) {
            cooldownProgress += Time.deltaTime;

            // Refresh Logic (just ends enumerator)
            if (!onCooldown) {
                yield break;
            }

            yield return null;
        }
        RefreshCooldown();
    }

    protected IEnumerator Channel() {
        channeling = true;
        while (durationProgress < duration) {
            if (!channeling) {
                Interrupt();
                yield break;
            }

            durationProgress += Time.deltaTime;
            yield return null;
        }
        StopChannel();
    }

    public bool IsChanneling() {
        return channeling;
    }

    public float GetDuration() {
        if (duration == 0f) return 0f;

        return durationProgress / duration;
    }

    public float GetCooldown() {
        return cooldownProgress / cooldown;
    }

    public bool OnCooldown() {
        return onCooldown;
    }

    public void StopChannel() {
        channeling = false;
        durationProgress = 0f;
    }

    public void RefreshCooldown() {
        onCooldown = false;
        cooldownProgress = 0f;
    }

    private void Interrupt() {
        Warning.ShowWarning("Ability Interrupted");
        OnInterrupt();
    }
}