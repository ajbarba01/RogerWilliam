using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    private float durationProgress;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Run(Sprite sprite) {
        spriteRenderer.sprite = sprite;
        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime() {
        durationProgress = duration;
        while (durationProgress > 0f) {
            float alpha = Mathf.Lerp(0f, 1f, durationProgress / duration);
            SetAlpha(alpha);
            durationProgress -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void SetAlpha(float a) {
        Color original = spriteRenderer.color;
        original.a = a;
        spriteRenderer.color = original;
    }
}
