using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pulse : MonoBehaviour
{
    private float pulseProgress;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float frequency = 2f;

    void Start() {
        frequency = 1 / frequency;
    }

    // Update is called once per frame
    void Update()
    {
        pulseProgress += Time.deltaTime;
        float scale = SineWave(pulseProgress);
        ChangeAlpha(scale);
    }
    
    void ChangeAlpha(float a) {
        Color color = text.color;
        color.a = a;
        text.color = color;
    }

    float SineWave(float time)
    {
        return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * frequency * time);
    }

}
