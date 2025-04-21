using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamiBoss : MonoBehaviour
{
   public Transform target;
    public Transform sausageWhip; // The transform representing the sausage
    public float whipRange = 3f;
    public float whipCooldown = 2f;
    public float whipSpeed = 10f;
    public int damage = 10;

    private float lastWhipTime = -Mathf.Infinity;
    private Vector3 originalWhipPosition;

    void Start()
    {
        if (sausageWhip != null)
            originalWhipPosition = sausageWhip.localPosition;
    }

    void Update()
    {
        if (target == null) return;

        // Face the player
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // keep rotation flat
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        // Try to whip
        if (Time.time >= lastWhipTime + whipCooldown)
        {
            StartCoroutine(WhipSausage());
            lastWhipTime = Time.time;
        }
    }

    System.Collections.IEnumerator WhipSausage()
    {
        // Extend whip
        Vector3 targetPosition = sausageWhip.localPosition + sausageWhip.forward * whipRange;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * whipSpeed;
            sausageWhip.localPosition = Vector3.Lerp(originalWhipPosition, targetPosition, t);
            yield return null;
        }

        // Deal damage if player is hit
        if (Vector3.Distance(sausageWhip.position, target.position) < 1.5f)
        {
            Debug.Log("Whipped enemy with sausage!");
            // If player has health script:
            // target.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

        // Retract whip
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * whipSpeed;
            sausageWhip.localPosition = Vector3.Lerp(targetPosition, originalWhipPosition, t);
            yield return null;
        }
    }
}
