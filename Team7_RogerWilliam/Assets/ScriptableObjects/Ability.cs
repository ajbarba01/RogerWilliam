using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public abstract class Ability : LoadoutOption
{
    public UnityEvent<Health> onEnemyHit;
    public abstract void Activate();
}