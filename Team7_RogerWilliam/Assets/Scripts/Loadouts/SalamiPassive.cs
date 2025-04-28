using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamiPassive : MonoBehaviour
{
     [SerializeField] private AgentMover mover;

    private void Start()
    {
        if (mover == null)
        {
            Debug.LogError("Mover not assigned!");
            return;
        }

        float currentSpeed = mover.GetMovement().magnitude;
        mover.SetMovespeed(currentSpeed * 2f);
    }
}
