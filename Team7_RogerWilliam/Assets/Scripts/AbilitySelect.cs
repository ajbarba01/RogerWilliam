using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelect : Menu
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject[] weaponSlots;

    private bool weaponOpen;

    void Start()
    {
        accessKey = KeyCode.N;

        weaponOpen = false;
    }

    protected override void OnOpen() {
        if (HUD != null) {
            HUD.SetActive(false);
        }
    }

    protected override void OnClose() {
        if (HUD != null) {
            HUD.SetActive(true);
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
}
