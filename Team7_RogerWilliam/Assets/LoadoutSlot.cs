using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TooltipWindow))]
public class LoadoutSlot : MonoBehaviour
{
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private Image icon;
    private string defaultTooltipText = "Empty";
    [SerializeField] private TooltipWindow tooltipWindow;
    private LoadoutOption current;

    private void Start() {
        UpdateTooltip();
    }

    public void SetOption(LoadoutOption option) {
        current = option;

        UpdateIcon();
        UpdateTooltip();
    }

    public LoadoutOption GetOption () {
        return current;
    }

    public void RemoveOption() {
        SetOption(null);
    }

    private void UpdateIcon() {
        if (current == null) {
            icon.sprite = defaultIcon;
        }

        else {
            icon.sprite = current.GetIcon();
        }
    }

    private void UpdateTooltip() {
        if (tooltipWindow == null) {
            Debug.Log(gameObject.name);
        }
        if (current == null) {
            tooltipWindow.SetText(defaultTooltipText);
        }

        else {
            tooltipWindow.SetText(current.GetTooltip());
        }
    }
}
