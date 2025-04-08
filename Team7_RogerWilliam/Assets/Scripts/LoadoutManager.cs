using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    public static LoadoutManager Instance;

    private List<Weapon> unlockedWeapons;
    private List<Ability> unlockedAbilities;
    private List<Passive> unlockedPassives;
    
    [SerializeField] public LoadoutOption currentWeapon;
    [SerializeField] public Ability currentAbility;
    [SerializeField] public Passive currentPassive;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }

        else {
            Destroy(gameObject);
        }
    }

    public void UnlockWeapon(Weapon newWeapon) {
        unlockedWeapons.Add(newWeapon);
    }

    public void UnlockAbility(Ability newAbility) {
        unlockedAbilities.Add(newAbility);
    }

    public void UnlockPassive(Passive newPassive) {
        unlockedPassives.Add(newPassive);
    }

    private void Reset() {
        currentWeapon = null;
        currentAbility = null;
        currentPassive = null;

        unlockedWeapons = new List<Weapon>();
        unlockedAbilities = new List<Ability>();
        unlockedPassives = new List<Passive>();
    }
}
