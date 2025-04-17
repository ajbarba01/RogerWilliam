using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salami : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damagePerSecond = 10f;
    public float damageInterval = 1f;

    private HashSet<GameObject> objectsInZone = new HashSet<GameObject>();

    private void Start()
    {
        StartCoroutine(DamageOverTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInZone.Contains(other.gameObject))
        {
            objectsInZone.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInZone.Contains(other.gameObject))
        {
            objectsInZone.Remove(other.gameObject);
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);

            foreach (GameObject obj in objectsInZone)
            {
                if (obj != null)
                {
                    // Replace this with your own damageable component
                    var health = obj.GetComponent<Health>();
                    if (health != null)
                    {
                        health.TakeDamage(damagePerSecond * damageInterval);
                    }
                }
            }
        }
    }
}
