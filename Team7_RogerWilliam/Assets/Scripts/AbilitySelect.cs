using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilitySelect : Menu
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject[] weaponSlots;

    [SerializeField] private LoadoutSlot weapon, ability, passive;
    [SerializeField] private TextMeshProUGUI nickname;
    private string currentNickname = "";

    private bool weaponOpen;

    private WeaponHandler weaponHandler;
    private AbilityHandler abilityHandler;
    private PassiveHandler passiveHandler;

    private void Start() {
        accessKey = KeyCode.N;
        weaponOpen = false;

        weaponHandler = Player.Instance.GetComponentInChildren<WeaponHandler>();
        abilityHandler = Player.Instance.GetComponentInChildren<AbilityHandler>();
        passiveHandler = Player.Instance.GetComponentInChildren<PassiveHandler>();
    }

    protected override void OnOpen() {
        Refresh();
        UpdateNickname();

        if (HUD != null) {
            HUD.SetActive(false);
        }
    }

    protected override void OnClose() {
        if (HUD != null) {
            HUD.SetActive(true);
            HUD.GetComponent<LoadoutHUD>().Refresh();
        }
    }

    public void WeaponSelect() {
        weaponOpen = !weaponOpen;

        Reveal(weaponSlots, weaponOpen);
    }

    private void Reveal(GameObject[] slots, bool reveal) {
        foreach (GameObject slot in slots) {
            slot.SetActive(reveal);
        }
    }

    private void Refresh() {
        weapon.SetOption(LoadoutManager.Instance.currentWeapon);
        ability.SetOption(LoadoutManager.Instance.currentAbility);
        passive.SetOption(LoadoutManager.Instance.currentPassive);
    }

    private void UpdateNickname() {
        currentNickname = "\"";
        if (ability.GetOption() != null) {
            currentNickname += ability.GetOption().GetNickname() + " ";
        }

        if (weapon.GetOption() != null) {
            currentNickname += weapon.GetOption().GetNickname() + " ";
        }

        if (passive.GetOption() != null) {
            currentNickname += passive.GetOption().GetNickname();
        }

        nickname.text = currentNickname.Trim() + "\"";

    }
}
