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
    
    void Awake()
    {
        // Debug.Log("LOADOUT HUD LISTENING");

        abilityChannel.SetActive(false);

        LoadoutManager.Instance.postWeaponUpdated.AddListener(UpdateWeapon);
        LoadoutManager.Instance.postAbilityUpdated.AddListener(UpdateAbility);
        LoadoutManager.Instance.postPassiveUpdated.AddListener(UpdatePassive);
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

    public void UpdateWeapon(LoadoutOption option) {
        weapon.SetOption(option);
        currentWeapon = Player.Instance.GetComponentInChildren<WeaponHandler>().GetWeapon();
    }
    
    public void UpdateAbility(LoadoutOption option) {
        ability.SetOption(option);
        currentAbility = Player.Instance.GetComponentInChildren<AbilityHandler>().GetAbility();
    }

    public void UpdatePassive(LoadoutOption option) {
        passive.SetOption(option);
    }
}
