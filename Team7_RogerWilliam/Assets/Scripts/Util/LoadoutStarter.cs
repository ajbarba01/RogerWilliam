using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutStarter : MonoBehaviour
{
    private static bool started = false;

    [SerializeField] private LoadoutOption[] weapons, abilities, passives;

    [SerializeField] private LoadoutOption weapon, ability, passive;

    void Start()
    {
        if (started) return;
        
        Debug.Log("LOADOUT STARTER");

        foreach (LoadoutOption weapon in weapons) {
            if (weapon == null) continue;
            LoadoutManager.Instance.UnlockWeapon(weapon);
        }
        
        foreach (LoadoutOption ability in abilities) {
            if (ability == null) continue;
            LoadoutManager.Instance.UnlockAbility(ability);
        }
        
        foreach (LoadoutOption passive in passives) {
            if (passive == null) continue;
            LoadoutManager.Instance.UnlockPassive(passive);
        }

        if (weapon != null) {
            LoadoutManager.Instance.SetWeapon(weapon);
        }
        
        if (ability != null) {
            LoadoutManager.Instance.SetAbility(ability);
        }

        if (passive != null) {
            LoadoutManager.Instance.SetPassive(passive);
        }

        started = true;
    }
}
