using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string tooltipText;
    [SerializeField] private float requiredHover = 1f;
    [SerializeField] private Button window;

    private float hoverTime = 0f;
    private bool hover = false;

    private void OnDisable() {
        Hide();
    }

    void Update() {
        if (hover) {
            hoverTime += Time.deltaTime;
            if (hoverTime >= requiredHover) {
                Show();
                hover = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData e) {
        hover = true;
    }

    public void OnPointerExit(PointerEventData e) {
        Hide();
    }

    public void SetText(string newText) {
        tooltipText = newText;
    }
    
    private void Show() {
        Tooltip.ShowTooltip(tooltipText);
    }

    private void Hide() {
        Tooltip.HideTooltip();
        hover = false;
        hoverTime = 0f;
    }
}
