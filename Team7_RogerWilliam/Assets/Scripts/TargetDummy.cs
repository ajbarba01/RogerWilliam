using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetDummy : MonoBehaviour
{
    private Health health;

    [SerializeField] private TextMeshPro text;
    [SerializeField] private float timeWindow = 5f;
    [SerializeField] private float smoothingSpeed = 0.8f;

    private float dps = 0f;
    private float smoothDps = 0f;
    private Queue<(float time, float damage)> damageQueue = new Queue<(float, float)>();
    
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        health.tookDamage.AddListener(TookDamage);
        health.onDeath.AddListener(onDeath);

        SetDPSText(0f);
    }

    private void FixedUpdate() {
        float currentTime = Time.time;

        while (damageQueue.Count > 0 && currentTime - damageQueue.Peek().time > timeWindow)
        {
            damageQueue.Dequeue();
        }

        float totalRecentDamage = 0f;
        foreach (var entry in damageQueue)
        {
            totalRecentDamage += entry.damage;
        }

        dps = totalRecentDamage / timeWindow;

        smoothDps = Mathf.Lerp(smoothDps, dps, smoothingSpeed * Time.deltaTime);

        SetDPSText(smoothDps);
    }

    // Update is called once per frame
    private void onDeath()
    {
        health.ResetHealth();
    }

    private void TookDamage(float amount) {
        damageQueue.Enqueue((Time.time, amount));
    }

    private void SetDPSText(float displayDPS) {
        if (displayDPS < 1) {
            text.gameObject.SetActive(false);
        }

        else {
            text.gameObject.SetActive(true);
            text.text = "DPS: " + ((int)displayDPS);
        }

    }
}
