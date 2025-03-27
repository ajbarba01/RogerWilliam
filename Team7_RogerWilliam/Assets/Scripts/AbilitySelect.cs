using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelect : Menu
{
    [SerializeField] private GameObject HUD;

    void Start()
    {
        accessKey = KeyCode.I;
    }

    protected override void OnOpen() {
        HUD.SetActive(false);
    }

    protected override void OnClose() {
        HUD.SetActive(true);
    }
}
