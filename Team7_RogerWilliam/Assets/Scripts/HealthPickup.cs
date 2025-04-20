using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickupable
{
    [SerializeField] private float healAmount = 10f;
    //public Transform backToStart; //uncomment this line for "auto-death," to zap the Player back to start

    protected override void Pickup() {
        Player.health.Heal(healAmount);
    }
}
