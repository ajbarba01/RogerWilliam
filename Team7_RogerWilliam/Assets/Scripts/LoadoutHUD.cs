using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LoadoutHUD : MonoBehaviour
{
    [SerializeField] private LoadoutSlot weapon, ability, passive;
    [SerializeField] private Image abilityCooldown, weaponCooldown;
    [SerializeField] private GameObject abilityChannel;
    [SerializeField] private Image channelBar;
    [SerializeField] private TextMeshProUGUI channelText;

    private Ability currentAbility;
    private Weapon currentWeapon;
    
    void Start()
    {
        // Ewww I hate the way I did this but I have no idea how else to call this function after all starts have been called.
        // Could use unity event but i dont want to deal with that rn
        Invoke("Refresh", 0.1f);

        abilityChannel.SetActive(false);
    }

    private void Update() {
        if (currentAbility != null && currentAbility.OnCooldown()) {
            abilityCooldown.fillAmount = 1 - currentAbility.GetCooldown();
        }
        else {
            abilityCooldown.fillAmount = 0f;
        }

        if (currentWeapon != null) {
            weaponCooldown.fillAmount = currentWeapon.HUDFill();
        }
        else {
            weaponCooldown.fillAmount = 0f;
        }

        if (currentAbility != null && currentAbility.IsChanneling()) {
            abilityChannel.SetActive(true);
            channelBar.fillAmount = 1 - currentAbility.GetDuration();
            channelText.text = LoadoutManager.Instance.currentAbility.GetTooltip();
        }   
        else {
            abilityChannel.SetActive(false);
        }

        // if (Input.GetKeyDown(KeyCode.Return) && currentAbility != null) {
        //     currentAbility.RefreshCooldown();
        // }

        // if (Input.GetKeyDown(KeyCode.M) && currentAbility != null) {
        //     currentAbility.StopChannel();
        // }
    }

    public void Refresh() {
        weapon.SetOption(LoadoutManager.Instance.currentWeapon);
        ability.SetOption(LoadoutManager.Instance.currentAbility);
        passive.SetOption(LoadoutManager.Instance.currentPassive);

        GetLoadout();
    }

    private void GetLoadout() {
        currentAbility = Player.Instance.GetComponentInChildren<AbilityHandler>().GetAbility();
        currentWeapon = Player.Instance.GetComponentInChildren<WeaponHandler>().GetWeapon();
    }
}
