using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactions : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Pulse pulse;
    private bool isActive;

    private bool enabled;

    private static Interactions instance;

    private void Awake() {
        if (instance == null) {
            instance = this;

            text = GetComponentInChildren<TextMeshProUGUI>(true);
            pulse = GetComponent<Pulse>();
            isActive = false;
            enabled = false;
            Hide();
        }
        else {
            Destroy(gameObject);
        }
    }

    private void _Show(string newText) {
        SetText(newText);
        if (!isActive) {
            text.gameObject.SetActive(true);
            isActive = true;
            pulse.Reset();
        }
    }

    private void _Hide() {
        if (isActive) {
            text.gameObject.SetActive(false);
            isActive = false;
        }
    }

    public void SetText(string newText) {
        text.text = newText;
    }

    public static void Show(string newText) {
        instance._Show(newText);
    }

    public static void Hide() {
        instance._Hide();
    }

    public static void SetEnabled(bool newEnabled) {
        Debug.Log(newEnabled);
        instance.enabled = newEnabled;
        if (!newEnabled) {
            Hide();
        }
        else {
            instance.text.gameObject.SetActive(true);
            instance.pulse.Reset();
        }
    }
}
