using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactions : MonoBehaviour
{
    private static TextMeshProUGUI text;
    private static Pulse pulse;
    private static bool isActive;

    private void Start() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        pulse = GetComponent<Pulse>();
        isActive = true;
        Hide();
    }

    public static void Show() {
        if (!isActive) {
            text.gameObject.SetActive(true);
            isActive = true;
            pulse.Reset();
        }
    }

    public static void Hide() {
        if (isActive) {
            text.gameObject.SetActive(false);
            isActive = false;
        }
    }

    public static void SetText(string newText) {
        text.text = newText;
    }
}
