using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadoutOption", menuName = "LoadoutOption")]
public class LoadoutOption : ScriptableObject
{
    [SerializeField] private string tooltipText;
    [SerializeField] private string nickname;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject prefab;

    public string GetTooltip() {
        return tooltipText;
    }

    public string GetNickname() {
        return nickname;
    }
    
    public Sprite GetIcon() {
        return icon;
    }

    public GameObject GetPrefab() {
        return prefab;
    }
}
