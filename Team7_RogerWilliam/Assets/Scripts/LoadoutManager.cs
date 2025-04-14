using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadoutManager : MonoBehaviour
{
    public static LoadoutManager Instance;
    
    public HashSet<LoadoutOption> unlockedWeapons = new HashSet<LoadoutOption>();
    public HashSet<LoadoutOption> unlockedAbilities = new HashSet<LoadoutOption>();
    public HashSet<LoadoutOption> unlockedPassives = new HashSet<LoadoutOption>();
    
    [SerializeField] public LoadoutOption currentWeapon = null;
    [SerializeField] public LoadoutOption currentAbility = null;
    [SerializeField] public LoadoutOption currentPassive = null;

    public UnityEvent<LoadoutOption> postWeaponUpdated, postAbilityUpdated, postPassiveUpdated;
    public UnityEvent<LoadoutOption> weaponUpdated, abilityUpdated, passiveUpdated;

    public bool shouldRelay = false;

    private void Awake() {
        DontDestroyOnLoad(this);

        if (Instance == null) {
            Instance = this;
            shouldRelay = false;
            Debug.Log("LOADOUT MANAGER CREATED");
        }

        else {
            Instance.shouldRelay = true;
        }
    }

    private void Start() {
        if (Instance.shouldRelay) {
            Instance.Relay();
            Debug.Log("RELAYING");
            Instance.shouldRelay = false;
            Destroy(gameObject);
        }
    }

    public void UnlockWeapon(LoadoutOption newWeapon) {
        unlockedWeapons.Add(newWeapon);
    }

    public void UnlockAbility(LoadoutOption newAbility) {
        unlockedAbilities.Add(newAbility);
    }

    public void UnlockPassive(LoadoutOption newPassive) {
        unlockedPassives.Add(newPassive);
    }

    public void SetWeapon(LoadoutOption weapon) {
        weaponUpdated.Invoke(weapon);
        postWeaponUpdated.Invoke(weapon);
        currentWeapon = weapon;
    }

    public void SetAbility(LoadoutOption ability) {
        abilityUpdated.Invoke(ability);
        postAbilityUpdated.Invoke(ability);
        currentAbility = ability;
    }
    
    public void SetPassive(LoadoutOption passive) {
        passiveUpdated.Invoke(passive);
        postPassiveUpdated.Invoke(passive);
        currentPassive = passive;
    }

    public void Relay() {
        weaponUpdated.Invoke(currentWeapon);
        postWeaponUpdated.Invoke(currentWeapon);

        abilityUpdated.Invoke(currentAbility);
        postAbilityUpdated.Invoke(currentAbility);

        passiveUpdated.Invoke(currentPassive);
        postPassiveUpdated.Invoke(currentPassive);
    }

    private void Reset() {
        currentWeapon = null;
        currentAbility = null;
        currentPassive = null;

        unlockedWeapons = new HashSet<LoadoutOption>();
        unlockedAbilities = new HashSet<LoadoutOption>();
        unlockedPassives = new HashSet<LoadoutOption>();
    }
}
