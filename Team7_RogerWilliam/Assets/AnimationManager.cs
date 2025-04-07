using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
    private Animator anim;
    private string currentState;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeState(string newState) {
        if (newState == currentState) return;

        currentState = newState;
        anim.Play(newState);
    }
}
