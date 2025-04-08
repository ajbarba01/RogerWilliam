using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutHUD : MonoBehaviour
{
    [SerializeField] private LoadoutSlot weapon, ability, passive;
    
    void Start()
    {
        Refresh();
    }

    public void Refresh() {
        weapon.SetOption(LoadoutManager.Instance.currentWeapon);
        ability.SetOption(LoadoutManager.Instance.currentAbility);
        passive.SetOption(LoadoutManager.Instance.currentPassive);
    }
}
