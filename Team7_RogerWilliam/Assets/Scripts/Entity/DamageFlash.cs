using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Material flashMat;
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Health health;

    private Material flashInstance;
    private float cooldown = 0.15f;
    private bool onCooldown = false;

    private float duration = 0.2f;
    private bool flashing = false;

    private void Awake() {
        health.onDamage.AddListener(Flash);
    }

    void Flash() {
        if (flashing || onCooldown) return;
        StartCoroutine(_Flash());
    }

    private IEnumerator _Flash() {
        flashing = true;
        
        Material originalMat = spriteRenderer.material;
        flashInstance = new Material(flashMat);
        flashInstance.SetColor("_FlashColor", flashColor);

        spriteRenderer.material = flashInstance;

        SetFlashAmount(1f);

        float durationProgress = duration;

        while (durationProgress > 0f) {
            durationProgress -= Time.deltaTime;

            float flashAmount = Mathf.Lerp(0f, 1f, (durationProgress / duration));
            SetFlashAmount(flashAmount);
            yield return null;
        }

        spriteRenderer.material = originalMat;

        flashing = false;

        StartCoroutine(Cooldown());
    }

    void SetFlashAmount(float amount) {
        flashInstance.SetFloat("_FlashAmount", amount);
    }

    private IEnumerator Cooldown() {
        onCooldown = true;

        yield return new WaitForSeconds(cooldown);

        onCooldown = false;
    }
}
