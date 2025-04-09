using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadoutHUD : MonoBehaviour
{
    [SerializeField] private LoadoutSlot weapon, ability, passive;
    [SerializeField] private Image abilityCooldown;

    private Ability currentAbility;
    private float currentCooldown;
    
    void Start()
    {
        Refresh();
    }

    private void Update() {
        if (currentAbility != null && currentAbility.OnCooldown()) {
            abilityCooldown.fillAmount = 1 - currentAbility.GetCooldown();
        }
        else {
            abilityCooldown.fillAmount = 0f;
        }
    }

    public void Refresh() {
        weapon.SetOption(LoadoutManager.Instance.currentWeapon);
        ability.SetOption(LoadoutManager.Instance.currentAbility);
        passive.SetOption(LoadoutManager.Instance.currentPassive);

        GetAbility();
    }

    private void GetAbility() {
        currentAbility = Player.Instance.GetComponentInChildren<AbilityHandler>().GetAbility();
    }
}
