using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : LoadoutOption {
    public UnityEvent<Health> onEnemyHit;
    public virtual void Attack() {}
}