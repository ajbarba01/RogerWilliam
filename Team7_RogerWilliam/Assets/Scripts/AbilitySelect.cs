using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadoutSelect : Menu
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private PopoutSelector weaponSlots, abilitySlots, passiveSlots;

    [SerializeField] private LoadoutSlot weapon, ability, passive;
    [SerializeField] private TextMeshProUGUI nickname;
    private string currentNickname = "";

    private bool weaponOpen;

    // private WeaponHandler weaponHandler;
    // private AbilityHandler abilityHandler;
    // private PassiveHandler passiveHandler;

    private void Awake() {
        Debug.Log("ABILITY SELECT LISTENING");

        accessKey = KeyCode.N;
        weaponOpen = false;

        weaponSlots.newOption.AddListener(WeaponSelect);
        abilitySlots.newOption.AddListener(AbilitySelect);
        passiveSlots.newOption.AddListener(PassiveSelect);

        LoadoutManager.Instance.weaponUpdated.AddListener(UpdateWeapon);
        LoadoutManager.Instance.abilityUpdated.AddListener(UpdateAbility);
        LoadoutManager.Instance.passiveUpdated.AddListener(UpdatePassive);
    }

    private void Start() {
        // weaponHandler = Player.Instance.GetComponentInChildren<WeaponHandler>();
        // abilityHandler = Player.Instance.GetComponentInChildren<AbilityHandler>();
        // passiveHandler = Player.Instance.GetComponentInChildren<PassiveHandler>();
    }

    public void WeaponSelect(LoadoutOption option) {
        LoadoutManager.Instance.SetWeapon(option);
    }

    public void AbilitySelect(LoadoutOption option) {
        LoadoutManager.Instance.SetAbility(option);
    }

    public void PassiveSelect(LoadoutOption option) {
        LoadoutManager.Instance.SetPassive(option);
    }

    protected override void OnOpen() {
        UpdateNickname();

        weaponSlots.SetOptions(LoadoutManager.Instance.unlockedWeapons);
        abilitySlots.SetOptions(LoadoutManager.Instance.unlockedAbilities);
        passiveSlots.SetOptions(LoadoutManager.Instance.unlockedPassives);

        weaponSlots.SetActive(LoadoutManager.Instance.currentWeapon);
        abilitySlots.SetActive(LoadoutManager.Instance.currentAbility);
        passiveSlots.SetActive(LoadoutManager.Instance.currentPassive);

        if (HUD != null) {
            HUD.SetActive(false);
        }
    }

    protected override void OnClose() {
        if (HUD != null) {
            HUD.SetActive(true);
            // HUD.GetComponent<LoadoutHUD>().Refresh();
        }
    }

    private void Refresh() {
        weapon.SetOption(LoadoutManager.Instance.currentWeapon);
        ability.SetOption(LoadoutManager.Instance.currentAbility);
        passive.SetOption(LoadoutManager.Instance.currentPassive);
    }

    public void UpdateWeapon(LoadoutOption option) {
        Debug.Log("WEAPON UPDATE");
        weapon.SetOption(option);
        weaponSlots.SetOptions(LoadoutManager.Instance.unlockedWeapons);
        weaponSlots.SetActive(option);
    }

    public void UpdateAbility(LoadoutOption option) {
        ability.SetOption(option);
        abilitySlots.SetOptions(LoadoutManager.Instance.unlockedAbilities);
        abilitySlots.SetActive(option);
    }

    public void UpdatePassive(LoadoutOption option) {
        passive.SetOption(option);
        passiveSlots.SetOptions(LoadoutManager.Instance.unlockedPassives);
        passiveSlots.SetActive(option);
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
