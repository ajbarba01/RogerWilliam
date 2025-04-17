using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WhiteHurt : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Sprite sprite;
    private float flashProgress = 0f;
    private float flashDuration = 0.5f;

    private void Start() {
        if (health == null) return;

        health.onDamage.AddListener(FlashWhite);
    }

    void FlashWhite()
    {
        flashProgress += Time.deltaTime;
        if (flashProgress > flashDuration) {
            flashProgress = 0f;

        }
    }

    void SetColor() {
        
    }
}
