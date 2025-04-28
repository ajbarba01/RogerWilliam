using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPassive : Passive
{
    [SerializeField] private Roll roll;
    [SerializeField] private float speedMultiplier = 1.5f;

    private void Start()
    {
        if (roll != null)
        {
            roll.rollSpeed *= speedMultiplier;
        }
    }
}
