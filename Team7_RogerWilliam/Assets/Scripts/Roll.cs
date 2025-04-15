using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Roll : MonoBehaviour
{
    [SerializeField] private float rollSpeed, rollDuration, rollCooldown;

    [SerializeField] private Image cooldownBar;

    private AgentMover mover;
    private AnimationManager anim;

    private float rollProgress, cooldownProgress = 0f;
    private bool rolling, onCooldown = false;

    public UnityEvent startRoll, endRoll;

    private void Awake() {
        mover = GetComponent<AgentMover>();
        anim = GetComponentInChildren<AnimationManager>();
        cooldownBar.fillAmount = 0f;
    }

    void Update()
    {
        bool moving = Player.Instance.moving;
        if (!rolling && !onCooldown && moving && Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(ActivateRoll());
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator ActivateRoll() {
        startRoll.Invoke();

        rolling = true;

        anim.PlayOnce("Player_Roll", rollDuration);

        Vector3 direction = mover.GetDirection();

        while (rollProgress < rollDuration) {
            rollProgress += Time.deltaTime;
            float speedMod = Mathf.Lerp(1f, 0f, rollProgress / rollDuration);
            mover.SetMovement(direction * (rollSpeed * speedMod));
            yield return null;
        }
        
        rollProgress = 0f;
        rolling = false;
        endRoll.Invoke();
    }

    private IEnumerator Cooldown() {
        onCooldown = true;

        while (cooldownProgress < rollCooldown) {
            cooldownProgress += Time.deltaTime;
            cooldownBar.fillAmount = 1 - cooldownProgress / rollCooldown;
            yield return null;
        }

        cooldownProgress = 0f;
        cooldownBar.fillAmount = 0f;
        onCooldown = false;
    }
}
