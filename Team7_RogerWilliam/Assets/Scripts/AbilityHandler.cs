using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    private Ability[] abilities;

    [SerializeField] private Ability[] startingAbilities; 
    [SerializeField] private int abilityCapacity = 4;

    [SerializeField] private Ability melee;

    void Start() {
        abilities = new Ability[abilityCapacity];

        for (int i = 0; i < abilityCapacity; i++) {
            abilities[i] = null;
        }

        abilities[0] = melee;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ActivateAbility(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ActivateAbility(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ActivateAbility(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ActivateAbility(4);
        }
    }

    void ActivateAbility(int abNumber) {
        if (abilities[abNumber] != null) {
            abilities[abNumber].Activate();
        }
    }

    void SetAbility(Ability ab, int abNumber) {
        abilities[abNumber] = ab;
    }
}