using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Text tooltipText;
    [SerializeField] private RectTransform BG;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float padding = 4f;
    [SerializeField] private GameObject window;

    private static Tooltip instance; 

    private void Awake() {
        instance = this;
        HideTooltip();
    }

    private void Update() {
        transform.position = Input.mousePosition + offset;
    }

    private void _ShowTooltip(string tooltipString) {
        tooltipText.text = tooltipString;
        window.SetActive(true);

        Vector2 BGSize = new Vector2(tooltipText.preferredWidth + padding * 2f, tooltipText.preferredHeight + padding * 2f);
        BG.sizeDelta = BGSize;
    }

    private void _HideTooltip() {
        window.SetActive(false);
    }

    public static void ShowTooltip(string tooltipString) {
        instance._ShowTooltip(tooltipString);
    }

    public static void HideTooltip() {
        instance._HideTooltip();
    }
}
