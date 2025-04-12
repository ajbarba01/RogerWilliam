using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
    private Animator anim;
    private string currentState;

    private bool playing;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeState(string newState) {
        if (playing) return;
        if (newState == currentState) return;

        currentState = newState;
        anim.Play(newState);
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
