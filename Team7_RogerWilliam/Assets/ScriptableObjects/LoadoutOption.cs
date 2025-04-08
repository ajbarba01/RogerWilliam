using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutOption : ScriptableObject
{
    [SerializeField] private string tooltipText;
    [SerializeField] private string nickname;
    [SerializeField] private Sprite icon;

    public string GetTooltip() {
        return tooltipText;
    }

    public string GetNickname() {
        return nickname;
    }
    
    public Sprite GetIcon() {
        return icon;
    }
}
