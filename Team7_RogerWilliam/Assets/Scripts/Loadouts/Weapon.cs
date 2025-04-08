using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : Loadout {
    public UnityEvent<Health> onEnemyHit;
    public abstract void Attack();
}