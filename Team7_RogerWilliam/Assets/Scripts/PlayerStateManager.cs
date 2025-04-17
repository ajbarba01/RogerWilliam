using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private WeaponHandler weaponHandler;
    [SerializeField] private AbilityHandler abilityHandler;
    [SerializeField] private Roll roll;

    private void Awake() {
        weaponHandler.onAttack.AddListener(OnAttack);
        roll.startRoll.AddListener(StartRoll);
        roll.endRoll.AddListener(EndRoll);
    }
    
    private void OnAttack() {
        abilityHandler.InterruptAbility();
    }

    private void StartRoll() {
        weaponHandler.SetPause(true);
        abilityHandler.SetPause(true);
        abilityHandler.InterruptAbility();
        weaponHandler.Interrupt();
    }

    private void EndRoll() {
        weaponHandler.SetPause(false);
        abilityHandler.SetPause(false);
    }
}
