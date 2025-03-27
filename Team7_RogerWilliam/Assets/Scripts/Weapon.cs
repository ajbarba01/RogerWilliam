using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public UnityEvent<Health> onEnemyHit;
    public abstract void Attack();
}