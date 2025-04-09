using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Warning : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject window;

    [SerializeField] private float warningTime;
    [SerializeField] private float fadeTime;

    private float progress = 0f;

    private bool warning = false;
    private bool fading = false;

    private static Warning Instance; 

    private void Awake() {
        Instance = this;
        _HideWarning();
    }

    private void Update() {
        if (warning) {
            progress += Time.deltaTime;
            if (progress >= warningTime) {
                warning = false;
                fading = true;
                progress = 0f;
            }
        }
        else if (fading) {
            progress += Time.deltaTime;
            if (progress >= fadeTime) {
                fading = false;
                progress = 0f;
                _HideWarning();
            }
            else {
                ChangeAlpha(1 - (progress / fadeTime));
            }
        }
    }

    private void _ShowWarning(string warningString) {
        progress = 0f;
        fading = false;
        warning = true;
        ChangeAlpha(1f);

        warningText.text = warningString;
        window.SetActive(true);
    }

    private void _HideWarning() {
        window.SetActive(false);
    }

    public static void ShowWarning(string warningString) {
        Instance._ShowWarning(warningString);
    }

    void ChangeAlpha(float a) {
        Color color = warningText.color;
        color.a = a;
        warningText.color = color;
    }
}
