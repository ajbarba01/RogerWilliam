using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    private static Sprite defaultIcon;

    [SerializeField] private Image icon;

    public void SetIcon(Sprite newIcon) {
        icon.sprite = newIcon;
    }

    public void Remove() {
        icon.sprite = defaultIcon;
    }
}
