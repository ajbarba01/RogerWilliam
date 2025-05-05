using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quickfoot : Passive
{
    [SerializeField] private float speedMod;
    // private float ogSpeed;

    private AgentMover mover;

    private void Start()
    {
        mover = Player.Instance.GetComponent<AgentMover>();
        
        mover.SetMovespeed(mover.GetMoveSpeed() + speedMod);
    }

    private void OnDestroy() {
        mover.SetMovespeed(mover.GetMoveSpeed() - speedMod);
    }
}
