using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    public static LoadoutManager Instance;
    
    private HashSet<LoadoutOption> unlockedWeapons, unlockedAbilities, unlockedPassives;
    
    [SerializeField] public LoadoutOption currentWeapon, currentAbility, currentPassive;

    private void Awake() {
        DontDestroyOnLoad(this);

        if (Instance == null) {
            Instance = this;
        }

        else {
            Destroy(gameObject);
        }

        Reset();
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

    private void Reset() {
        currentWeapon = null;
        currentAbility = null;
        currentPassive = null;

        unlockedWeapons = new HashSet<LoadoutOption>();
        unlockedAbilities = new HashSet<LoadoutOption>();
        unlockedPassives = new HashSet<LoadoutOption>();
    }
}
