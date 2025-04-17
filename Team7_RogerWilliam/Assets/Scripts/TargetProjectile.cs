using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetProjectile : Projectile
{
    [SerializeField] private float distanceToHit = 0.5f;

    // private AnimationCurve speedCurve;
    private Vector3 target;
    private float maxSpeed = 0f;

    public void SetTarget(Vector3 newTarget) {
        target = newTarget;
    }

    public void SetMaxSpeed(float newMaxSpeed) {
        maxSpeed = newMaxSpeed;
    }

    public override void BeforeMove() {
        Vector2 towardsTarget = (Vector2)(target - transform.position);
        SetDirection(towardsTarget);

        if (towardsTarget.magnitude < distanceToHit) {
            OnHit();
        }

        // float currentSpeed = speedCurve.Evaluate();
        // SetSpeed();
    }

    // public void SetSpeedCurve(AnimationCurve newCurve) {
    //     speedCurve = newCurve;
    // }
}