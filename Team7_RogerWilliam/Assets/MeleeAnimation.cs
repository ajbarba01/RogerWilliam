using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimation : MonoBehaviour
{
    [SerializeField] private GameObject artPrefab;
    [SerializeField] private GameObject VFXPrefab;
    [SerializeField] private Color loadingColor, attackColor;
    [SerializeField] private float finishDuration = 0.1f;

    [SerializeField] private float maxAlpha = 0.5f;

    private float radius = 0f;

    private GameObject art;

    private bool finished = false;

    private float attackProgress = 0f;

    private SpriteRenderer renderer;
    
    private bool channeling;

    private void Awake() {
        art = Instantiate(artPrefab, transform.position, Quaternion.identity, transform);
        renderer = art.GetComponent<SpriteRenderer>();
        art.SetActive(false);
    }

    public void StartAttack(float attackRange, float attackDuration) {
        if (!channeling) {
            StartCoroutine(ChannelAttack(attackDuration));
        }
    }

    public void StopAttack() {
        channeling = false;
    }

    public void SetRadius(float radius) {
        this.radius = radius;
        SetSize(radius);
    }

    private void SetSize(float size) {
        art.transform.localScale = new Vector3(size * 2, size * 2, 1);
    }

    private IEnumerator ChannelAttack(float attackDuration) {
        finished = false;
        art.SetActive(true);
        channeling = true;
        attackProgress = 0f;
        while (channeling) {
            attackProgress += Time.deltaTime;
            ChangeAlpha(renderer, Mathf.Lerp(0, maxAlpha, attackProgress / attackDuration), loadingColor);
            SetSize(Mathf.Lerp(0, radius, attackProgress / attackDuration));
            yield return null;
        }
        
        channeling = false;
        art.SetActive(false);
    }

    public void FinishAttack() {
        channeling = false;
        GameObject VFX = Instantiate(VFXPrefab, transform.position, Quaternion.identity);
        VFX.transform.localScale = new Vector3(radius * 2, radius * 2, 1);
        ChangeAlpha(VFX.GetComponent<SpriteRenderer>(), maxAlpha, attackColor);
        Destroy(VFX, finishDuration);
    }

    private void ChangeAlpha(SpriteRenderer render, float alpha, Color color) {
        Color newColor = color;
        newColor.a = alpha;
        render.color = newColor;
    }
}
