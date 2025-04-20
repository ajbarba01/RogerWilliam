using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
    private Animator anim;
    private string currentState, priorityState;

    private bool playing, priority;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playing = false;
        priority = false;
    }

    public void ChangeState(string newState) {
        if (playing) return;
        if (newState == currentState) return;
        
        currentState = newState;
        if (!priority) {
            PlayState(newState);
        }
    }

    private void PlayState(string state) {
        anim.Play(state);
    }

    public void SetPriorityState(string newPriorityState) {
        Debug.Log(newPriorityState);
        if (playing || priority) return;

        priority = true;
        priorityState = newPriorityState;
        anim.Play(newPriorityState);
    }

    public void RemovePriorityState() {
        if (!priority) return;

        priority = false;
        PlayState(currentState);
    }

    public void PlayOnce(string newState, float duration=-1f) {
        StartCoroutine(_PlayOnce(newState, duration));
    }

    public IEnumerator _PlayOnce(string newState, float duration) {
        playing = true;
        anim.Play(newState);

        yield return null;

        float animLength = anim.GetCurrentAnimatorStateInfo(0).length - 0.15f;
        float timeToWait = duration;
        if (duration == -1f) {
            timeToWait = animLength;
        }
        else {
            anim.speed = animLength / duration;
        }

        yield return new WaitForSeconds(timeToWait);
        playing = false;
        anim.Play(currentState);
        anim.speed = 1f;
    }

    public void SetFacing(int facing) {
        if (!playing) {
            Vector3 newScale = transform.localScale;
            newScale.x = facing;
            transform.localScale = newScale;
        }
    }
}
